using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class CustomCommandService
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public event EventHandler<ConfigSubstationEventArgs> ConfigSubstationEvent;
        public event EventHandler<ConfigMonitoringServerEventArgs> ConfigMonitoringServerEvent;

        public event EventHandler<ConfigFluxEventArgs> ConfigFluxEvent;

        public CustomCommandService()
        {

        }

        public void Start()
        {
            Task.Factory.StartNew(PollCustomCommand, _cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private void PollCustomCommand()
        {
            CultureInfoHelper.SetDateTimeFormat();
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    var customCommandList = DasConfig.Repo.GetLastCustomCommand(now);

                    if (customCommandList == null || !customCommandList.Any())
                    {
                        Thread.Sleep(1000);
                        continue; //没有命令
                    }
                    foreach (var item in customCommandList)
                    {
                        // 接触命令, 准备执行.
                        item.Accept();
                        Task.Factory.StartNew(() => HandleCustomCommand(item));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(10 * 1000);
                    // 保证线程持续进行
                }
                Thread.Sleep(1000);
            }
        }
        private void HandleCustomCommand(CustomCommandModel customCommand)
        {
            customCommand.ResponseTime = DateTime.Now;
            switch ((CMDKey)customCommand.CMDKey)
            {
                case CMDKey.ConfigureSubSation:
                    HandleCustomCommandConfigSubstation(customCommand);
                    break;
                case CMDKey.DeleteSubSation:
                    HandleCustomCommandDeleteSubstation(customCommand);
                    break;
                case CMDKey.MonitoringServerSetting:
                    HandleCustomCommandConfigServer(customCommand);
                    break;
                case CMDKey.ConfigureFlux:
                    HandleCustomCommandConfigFlux(customCommand);
                    break;
                case CMDKey.DeleteFlux:
                    HandleCustomCommandDeleteFlux(customCommand);
                    break;
                default:
                    throw new Exception($"[{customCommand.CMDKey}]暂时未实现!");
            }
        }
        private void HandleCustomCommandConfigFlux(CustomCommandModel customCommand)
        {
            LogD.Info($"CustomCommand: 收到编辑[{customCommand.CMDData:D3}]号抽采测点命令 ******");
            if (string.IsNullOrEmpty(customCommand.CMDData))
            {
                customCommand.Finish();
            }
            else
            {
                ConfigFluxEvent?.Invoke(this, new ConfigFluxEventArgs(customCommand, int.Parse(customCommand.CMDData), CustomOperation.Update));
            }
        }
        public void HandleCustomCommandDeleteFlux(CustomCommandModel customCommand)
        {
            LogD.Info($"CustomCommand: 收到删除[{customCommand.CMDData:D3}]号抽采测点命令 ******");
            ConfigFluxEvent?.Invoke(this, new ConfigFluxEventArgs(customCommand, int.Parse(customCommand.CMDData), CustomOperation.Delete));
        }

        private void HandleCustomCommandDeleteSubstation(CustomCommandModel customCommand)
        {
            LogD.Info($"CustomCommand: 收到删除[{customCommand.SubStationID:D3}]号分站[{customCommand.CMDData}]命令 ******");
            ConfigSubstationEvent?.Invoke(this, new ConfigSubstationEventArgs(customCommand, customCommand.SubStationID, CustomOperation.Delete));
        }
        private void HandleCustomCommandConfigSubstation(CustomCommandModel customCommand)
        {
            LogD.Info($"CustomCommand: 收到编辑[{customCommand.SubStationID:D3}]号分站[{customCommand.CMDData}]命令 ******");
            ConfigSubstationEvent?.Invoke(this, new ConfigSubstationEventArgs(customCommand, customCommand.SubStationID, CustomOperation.Update));
        }
        private void HandleCustomCommandConfigServer(CustomCommandModel customCommand)
        {
            LogD.Info($"CustomCommand: 收到编辑[{customCommand.MonitoringServerID:D3}]号采集服务器[{customCommand.CMDData}]命令 ******");
            CustomOperation operation;
            if (customCommand.CMDData == "add")
                operation = CustomOperation.Add;
            else if (customCommand.CMDData == "delete")
                operation = CustomOperation.Delete;
            else
                operation = CustomOperation.Update;
            ConfigMonitoringServerEvent?.Invoke(this, new ConfigMonitoringServerEventArgs(customCommand, customCommand.MonitoringServerID, operation));
        }
    }

    abstract class CommandEventArgs : EventArgs
    {
        public CustomCommandModel CommandModel { get; }

        public CommandEventArgs(CustomCommandModel commandModel)
        {
            CommandModel = commandModel;
        }
    }
    class ConfigSubstationEventArgs : CommandEventArgs
    {
        public int SubstationId { get; private set; }
        public CustomOperation CustomOperation { get; private set; }
        public ConfigSubstationEventArgs(CustomCommandModel model, int substationId, CustomOperation customOperation)
            : base(model)
        {
            SubstationId = substationId;
            CustomOperation = customOperation;
        }
    }
    class ConfigMonitoringServerEventArgs : CommandEventArgs
    {
        public int MonitoringServerID { get; private set; }
        public CustomOperation CustomOperation { get; private set; }
        public ConfigMonitoringServerEventArgs(CustomCommandModel model, int monitoringServerId, CustomOperation customOperation)
            : base(model)
        {
            MonitoringServerID = monitoringServerId;
            CustomOperation = customOperation;
        }
    }
    class ConfigFluxEventArgs : CommandEventArgs
    {
        public int FluxId { get; private set; }
        public CustomOperation CustomOperation { get; private set; }
        public ConfigFluxEventArgs(CustomCommandModel model, int fluxId, CustomOperation customOperation)
            : base(model)
        {
            FluxId = fluxId;
            CustomOperation = customOperation;
        }
    }
    enum CustomOperation : byte
    {
        Delete = 0x00,
        Add = 0x01,
        Update = 0x02
    }
}
