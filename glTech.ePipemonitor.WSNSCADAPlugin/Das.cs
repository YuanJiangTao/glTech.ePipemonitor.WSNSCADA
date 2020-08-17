﻿using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using PluginContract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using PluginContract.Utils;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Threading;
using PluginContract.Extensions;
using PluginContract.Helper;
using System.Diagnostics;
using System.Linq;
using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol;
using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation;
using System.CodeDom;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class Das : IDas
    {
        private readonly ConcurrentDictionary<int, SubStationModel> _subStationDict =
           new ConcurrentDictionary<int, SubStationModel>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();


        public event EventHandler<SubStationUpdateRealDataEventArgs> DasUpdateRealData;
        public Comm Comm { get; }
        public bool IsGood { get; }

        public int MonitoringServerID { get; }
        private MonitoringServerConfigModel _serverConfig;
        public Das(MonitoringServerConfigModel monitoringServerConfigModel, IEnumerable<SubStationModel> subStationModels,
            Func<string, string[], PluginMonitor> createMonitor)
        {
            try
            {
                //XmlSerializerHelper<MonitoringServer> xmlSerializer = new PluginContract.Utils.XmlSerializerHelper<MonitoringServer>();
                MonitoringServerID = monitoringServerConfigModel.MonitoringServerID;
                _serverConfig = monitoringServerConfigModel;
                var serverConfig = XmlSerializerHelper.Serializer<MonitoringServer>(monitoringServerConfigModel.Configuration);
                if (serverConfig != null)
                {

                    Comm = new Comm(serverConfig.CommunicationMode.CommMode, $"COM{serverConfig.CommunicationConfig.COM}",
                        serverConfig.CommunicationConfig.BaudRate, serverConfig.CommunicationConfig.DataBits, (StopBits)serverConfig.CommunicationConfig.StopBits,
                         serverConfig.CommunicationConfig.GetParity());
                    if (Comm != null)
                        Comm.DataReceivedEvent += Comm_DataReceivedEvent;
                }
                Monitor = createMonitor(monitoringServerConfigModel.MonitoringServerID.ToString(), new string[] { "时间", "日志" });
                foreach (var substationModel in subStationModels)
                {
                    _subStationDict.TryAdd(substationModel.SubStationID, substationModel);
                }
                IsGood = true;
            }
            catch (Exception ex)
            {
                IsGood = false;
                LogD.Info($"初始化[{monitoringServerConfigModel.MonitoringServerID}]发生错误:{ex}");
            }

        }


        private void Comm_DataReceivedEvent(object sender, ChannelDataEventArgs e)
        {
            var now = DateTime.Now;
            var substationId = e.SendCommand.Id;

            if (!_subStationDict.ContainsKey(substationId))
            {
                return;
            }
            var substationModel = _subStationDict[substationId];
            var requestInfo = e.RequestInfo;
            if (requestInfo == null)
            {
                Log($"[{substationId}]号传感器断线");
                substationModel.UpdateNetOff(now);
            }
            else
            {
                Log($"[{substationId}]号传感器数据报文<--{requestInfo.DataString}");
                if (!requestInfo.CrcOk)
                {
                    Log($"[{substationId}]号传感器数据报文校验和错误，丢弃.");
                    return;
                }
                else
                {
                    switch (requestInfo.CmdKey)
                    {
                        case SubstationCmdkey.QueryRealData:
                            var substationData = new SubStationData(substationModel.SubStationID, requestInfo.Body);
                            if (substationData == null || !substationData.IsValid)
                            {
                                Log($"[{substationId}]号传感器数据报文无效，丢弃.");
                                return;
                            }
                            substationModel.Update(now, substationData);
                            break;
                        case SubstationCmdkey.ErrorResponse:
                            break;
                        default:
                            Log($"[{substationId}]号传感器未知报文，丢弃.");
                            break;
                    }

                }
            }
            FireDasUpdateRealData(substationModel);
        }

        private void Log(string content, bool isAddMonitor = true)
        {
            LogD.Info(content);
            if (isAddMonitor)
                Monitor.OnWatch(Monitor.Id, DateTime.Now.ToString(), content);
        }
        public PluginMonitor Monitor { get; }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                CultureInfoHelper.SetDateTimeFormat();
                var stopwatch = new Stopwatch();
                while (!_cts.IsCancellationRequested)
                {
                    // das 循环发送命令, 并接受报文.
                    try
                    {
                        if (Comm == null || !Comm.IsConnect)
                        {
                            Log($"{Comm}重新连接.");
                            Comm?.Start();
                            Thread.Sleep(3000);
                            continue;
                        }
                        stopwatch.Restart();
                        GatherData();
                        stopwatch.Stop();
                        Log($"{"=".Repeat(20)}一轮采集结束,耗时:{stopwatch.Elapsed.TotalSeconds:F2}秒{"=".Repeat(20)}");
                    }
                    catch (Exception e)
                    {
                        LogD.Error("轮询中出现错误:" + e);
                        // 循环不能断, 直至Stop;
                    }
                    finally
                    {
                        Thread.Sleep(50);
                    }
                }

            }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void GatherData()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            if (!_subStationDict.Any())
            {
                Log($"采集下无关联传感器.");
                SleepLifeCycle();
            }
            foreach (var subStationId in _subStationDict.Keys)
            {
                Log($"采集传感器{subStationId}");
                if (!_subStationDict.ContainsKey(subStationId))
                {
                    continue;
                }
                var now = DateTime.Now;
                var substationModel = _subStationDict[subStationId];
                if (!Comm.IsConnect)
                {
                    Log($"{Comm} 断开, {substationModel.SubStationID}断线");
                    substationModel.UpdateNetOff(now);
                    continue;
                }
                var command = SubstationProtocol.GetRealDataCommand(subStationId);
                Log($"读取[{subStationId}]号传感器实时数据:{command.DataString}");
                var result = Comm.Send(command);
                if (!result)
                {
                    Log($"读取[{subStationId}]号传感器实时数据失败");
                    continue;
                }
                if (!SpinWait.SpinUntil(() => command.IsReceiveResponse, DasConfig.RecvTimeout * 2))
                {
                    Log($"[{subStationId}]号传感器断线");
                    substationModel.UpdateNetOff(now);
                }
                SleepLifeCycle();
            }
        }

        private void FireDasUpdateRealData(SubStationModel subStationModel)
        {
            var realdataModels = new List<RealDataModel>();
            var analogRunModels = new List<AnalogRunModel>();
            var analogStatisticModels = new List<AnalogStatisticModel>();
            var alarmTodayModels = new List<Alarm_TodayModel>();
            var analogAlarmModels = new List<AnalogAlarmModel>();
            var fluxRealDataModels = new List<FluxRealDataModel>();
            var fluxRunModels = new List<FluxRunModel>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var message = new StringBuilder();
            realdataModels.Add(subStationModel.RealDataModel);
            realdataModels.AddRange(subStationModel.AnalogPointModels.Select(p => p.RealDataModel));
            if (realdataModels.Any())
            {
                message.Append($"RealDataModels:{realdataModels.Count}");
            }
            var arm = subStationModel.AnalogPointModels.SelectMany(p => p.AnalogRunModels).ToList();
            if (arm.Any())
            {
                message.Append($"AnalogRunModels:{arm.Count}");
            }
            analogRunModels.AddRange(arm);
            var asm = subStationModel.AnalogPointModels.SelectMany(p => p.AnalogStatisticModels).ToList();
            if (asm.Any())
            {
                message.Append($"AnalogStatisticModels:{asm.Count}");
            }
            analogStatisticModels.AddRange(asm);
            var atm = subStationModel.AnalogPointModels.SelectMany(p => p.Alarm_TodayModels).ToList();
            if (atm.Any())
            {
                message.Append($"Alarm_TodayModels:{atm.Count}");
            }
            alarmTodayModels.AddRange(atm);
            var aam = subStationModel.AnalogPointModels.SelectMany(p => p.AnalogAlarmModels).ToList();
            if (aam.Any())
            {
                message.Append($"AnalogAlarmModels:{aam.Count}");
            }
            analogAlarmModels.AddRange(aam);
            var frm = subStationModel.FluxPointModels.Select(p => p.FluxRealDataModel).ToList();
            if (frm.Any())
            {
                message.Append($"FluxRealDataModels:{frm.Count}");
            }
            fluxRealDataModels.AddRange(frm);
            var frunm = subStationModel.FluxPointModels.Select(p => p.FluxRunModel).ToList();
            if (frunm.Any())
            {
                message.Append($"FluxRunModels:{frunm.Count}");
            }
            fluxRunModels.AddRange(frunm);
            if (DasUpdateRealData != null)
            {
                DasUpdateRealData.Invoke(this, new SubStationUpdateRealDataEventArgs(
                this._serverConfig.MonitoringServerID,
                realdataModels,
                analogRunModels,
                analogStatisticModels,
                alarmTodayModels,
                analogAlarmModels,
                fluxRealDataModels,
                fluxRunModels
                ));
                //var receivers = DasUpdateRealData.GetInvocationList();
                //foreach (EventHandler<SubStationUpdateRealDataEventArgs> receive in receivers)
                //{
                    
                //}
            }
        }

        public bool DeleteSubstation(int substationId)
        {
            return _subStationDict.TryRemove(substationId, out _);
        }
        public bool HasSubstation(int substationId)
        {
            return _subStationDict.ContainsKey(substationId);
        }
        public bool ReloadSubstation(SubStationModel subStationModel)
        {
            if (_subStationDict.TryRemove(subStationModel.SubStationID, out _))
            {
                return _subStationDict.TryAdd(subStationModel.SubStationID, subStationModel);
            }
            return false;
        }


        private void SleepLifeCycle(Action action = null, Stopwatch stopwatch = null)
        {
            if (stopwatch == null)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }

            while (stopwatch.Elapsed.TotalSeconds < DasConfig.DasGatherInterval && !_cts.IsCancellationRequested)
            {
                action?.Invoke();
                Thread.Sleep(50);
            }
            stopwatch.Stop();
        }

        public void Stop()
        {
            try
            {
                _cts.Cancel();
            }
            catch (Exception ex)
            {
                LogD.Error(ex.ToString());
            }
        }
    }
}