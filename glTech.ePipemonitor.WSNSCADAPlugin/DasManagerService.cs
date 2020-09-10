using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation;
using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using PluginContract;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class DasManagerService
    {
        private readonly CustomCommandService _customCommandService;
        private readonly List<IDas> _dases = new List<IDas>();
        private readonly DataBulkService _dataBulkService;
        private PluginMonitor _dasMonitor;
        private PluginMonitor _databaseMonitor;
        public DasManagerService()
        {
            _dataBulkService = new DataBulkService();
            _customCommandService = new CustomCommandService();
        }

        public void Start(PluginMonitor databaseMonitor, PluginMonitor dasMonitor)
        {
            try
            {

#if DEBUG
                var hexString = "01 03 1E 00 00 00 00 00 00 00 00 42 C7 D0 13 00 00 41 D0 0A F1 00 00 41 EA 80 00 00 00 41 EE CF 38 6F 91";
                var bytes = ByteHelper.HexStringToByteArray(hexString);
                var substationData = new SubStationData(1, bytes.Skip(3).Take(0x1E).ToArray());


#endif
                _databaseMonitor = databaseMonitor;
                _dasMonitor = dasMonitor;
                _dases.AddRange(InitDas());
                _dases.ToList().ForEach(o => o.Start());
                _customCommandService.Start();
                _customCommandService.ConfigMonitoringServerEvent += CustomCommandService_ConfigMonitoringServerEvent;
                _customCommandService.ConfigSubstationEvent += CustomCommandService_ConfigSubstationEvent;
                _customCommandService.ConfigFluxEvent += CustomCommandService_ConfigFluxEvent;
                _dataBulkService.Start(_databaseMonitor);
                DasConfig.Repo.EndAlarmToday();
                DasConfig.Repo.EndAnalogAlarm();

            }
            catch (Exception ex)
            {
                LogD.Error(ex.ToString());
            }

        }

        private void CustomCommandService_ConfigFluxEvent(object sender, ConfigFluxEventArgs e)
        {
            var das = _dases.Find(o => o.MonitoringServerID == e.CommandModel.MonitoringServerID);
            if (das != null)
            {

                switch (e.CustomOperation)
                {
                    case CustomOperation.Delete:
                        {
                            das.DeleteFluxPoint(e.CommandModel.SubStationID, e.FluxId);
                        }
                        break;
                    case CustomOperation.Add:
                    case CustomOperation.Update:
                        {
                            var substationId = e.CommandModel.SubStationID;
                            var substationModel = das.GetSubStationModelBySubstationId(substationId);
                            if (substationModel != null)
                            {
                                if (int.TryParse(e.CommandModel.CMDData, out var fluxId))
                                {
                                    var fluxPointModels = DasConfig.Repo.GetFluxPointModelById(fluxId).ToList();
                                    var fluxRunModels = DasConfig.Repo.GetFluxRunModels(fluxPointModels.Select(p => p.FluxID).ToArray()).ToList();
                                    substationModel.InitPointModel(fluxPointModels, fluxRunModels);
                                }
                            }
                        }
                        break;
                }
            }
            e.CommandModel.Finish();
        }

        private void CustomCommandService_ConfigSubstationEvent(object sender, ConfigSubstationEventArgs e)
        {
            var das = _dases.Find(o => o.MonitoringServerID == e.CommandModel.MonitoringServerID);
            if (das != null)
            {
                switch (e.CustomOperation)
                {
                    case CustomOperation.Delete:
                        {
                            var result = das.DeleteSubstation(e.SubstationId);
                            e.CommandModel.Finish(result);
                        }
                        break;
                    case CustomOperation.Add:
                    case CustomOperation.Update:
                        {
                            var result = false;
                            var substationId = e.SubstationId;
                            var subStationModel = DasConfig.Repo.GetSubStationModel(substationId);
                            if (subStationModel == null)
                            {
                                if (das.HasSubstation(substationId))
                                    result = das.DeleteSubstation(substationId);
                            }
                            else
                            {
                                var analogPointModels = DasConfig.Repo.GetAnalogPointModelsBySubstationId(substationId).ToList();
                                var fluxPointModels = DasConfig.Repo.GetFluxPointModelsBySubstationId(substationId).ToList();
                                var fluxRunModels = DasConfig.Repo.GetFluxRunModels(fluxPointModels.Select(p => p.FluxID).ToArray()).ToList();
                                InitTreeStructure(subStationModel, analogPointModels, fluxPointModels, fluxRunModels);
                                result = das.ReloadSubstation(subStationModel);
                            }
                            e.CommandModel.Finish(result);
                        }
                        break;
                }
            }
        }
        private void InitTreeStructure(
           SubStationModel subStationModel,
           List<AnalogPointModel> analogPointModels,
           List<FluxPointModel> fluxPointModels, List<FluxRunModel> fluxRunModels)
        {
            var subAnalogPointModels =
                analogPointModels.Where(o => o.SubStationID == subStationModel.SubStationID).ToList();
            var subFluxPointModels =
                fluxPointModels.Where(o => o.SubStationID == subStationModel.SubStationID).ToList();

            subStationModel.InitPointModel(subAnalogPointModels,
                fluxPointModels, fluxRunModels);
        }
        private void CustomCommandService_ConfigMonitoringServerEvent(object sender, ConfigMonitoringServerEventArgs e)
        {
            switch (e.CustomOperation)
            {
                case CustomOperation.Update:
                    RemoveDas(e.MonitoringServerID);
                    AddDas(e.MonitoringServerID);
                    break;
                case CustomOperation.Add:
                    AddDas(e.MonitoringServerID);
                    break;
                case CustomOperation.Delete:
                    RemoveDas(e.MonitoringServerID);
                    break;
                default:
                    break;
            }
            e.CommandModel.Finish();
        }
        private void RemoveDas(int dasId)
        {
            var das = _dases.Find(o => o.MonitoringServerID == dasId);
            if (das != null)
            {
                das.DasUpdateRealData -= _dataBulkService.DasUpdateRealData;
                das.Stop();
                LogD.Info($"移除旧的 采集 {dasId}.");
                _dases.Remove(das);
            }
        }
        private void AddDas(int dasId)
        {
            if (!_dases.Exists(p => p.MonitoringServerID == dasId))
            {
                var monitorServerConfig = DasConfig.Repo.GetMonitoringServerConfigModelById(dasId);
                var substationModels = DasConfig.Repo.GetSubStationModelsByMonitorServerId(dasId).ToList();
                var analogPointModels = DasConfig.Repo.GetAnalogPointModels();
                var fluxPointModels = DasConfig.Repo.GetFluxPointModels();
                var fluxRunModels = DasConfig.Repo.GetFluxRunModels(fluxPointModels.Select(p => p.FluxID).ToArray());
                foreach (var substation in substationModels)
                {
                    var subAnalogPointModels = analogPointModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                    var subFluxPointModels = fluxPointModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                    var subFluxRunModels = fluxRunModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                    substation.InitPointModel(subAnalogPointModels, subFluxPointModels, subFluxRunModels);
                }
                var dasSubstationModels = substationModels.Where(p => p.MonitoringServerID == monitorServerConfig.MonitoringServerID).ToList();
                monitorServerConfig.InitSubStation(dasSubstationModels);
                var newDas = NewDas(monitorServerConfig, substationModels);
                if (newDas.IsGood)
                {
                    _dases.Add(newDas);
                    newDas.Start();
                }
                else
                {
                    LogD.Info($"采集服务器{dasId}初始化失败.");
                }
            }
        }
        public void Stop()
        {
            try
            {
                _dases.ForEach(o => o.Stop());
                _dataBulkService.Stop();
                _customCommandService.Stop();
                _customCommandService.ConfigMonitoringServerEvent -= CustomCommandService_ConfigMonitoringServerEvent;
                _customCommandService.ConfigSubstationEvent -= CustomCommandService_ConfigSubstationEvent;
                _customCommandService.ConfigFluxEvent -= CustomCommandService_ConfigFluxEvent;
            }
            catch (Exception ex)
            {
                LogD.Error(ex.ToString());
            }

        }
        private IEnumerable<IDas> InitDas()
        {

            LogD.Info("初始化数据库.");
            var monitorServerConfigModels = DasConfig.Repo.GetMonitoringServerConfigs().ToList();
            LogD.Info($"获取 Das 服务 {monitorServerConfigModels.Count}个.");

            var substationModels = DasConfig.Repo.GetSubStationModels().ToList();
            LogD.Info($"获取 分站 {substationModels.Count}个.");

            var analogPointModels = DasConfig.Repo.GetAnalogPointModels().ToList();
            LogD.Info($"获取 模拟量 {analogPointModels.Count}个.");

            var fluxPointModels = DasConfig.Repo.GetFluxPointModels().ToList();

            LogD.Info($"获取 抽采测点 {fluxPointModels.Count}个.");

            var fluxRunModels = DasConfig.Repo.GetFluxRunModels(fluxPointModels.Select(p => p.FluxID).ToArray());
            LogD.Info($"获取 抽采测点当前记录 {fluxPointModels.Count}个.");

            InitTreeStructure(monitorServerConfigModels, substationModels, analogPointModels, fluxPointModels, fluxRunModels);
            var dass = new List<IDas>();
            foreach (var item in monitorServerConfigModels)
            {
                var das = NewDas(item, item.SubStationModels);
                yield return das;
            }
        }
        private void InitTreeStructure(List<MonitoringServerConfigModel> dasModels, List<SubStationModel> substationModels,
            List<AnalogPointModel> analogPointModels, List<FluxPointModel> fluxPointModels,
            List<FluxRunModel> fluxRunModels)
        {
            foreach (var substation in substationModels)
            {
                var subAnalogPointModels = analogPointModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                var subFluxPointModels = fluxPointModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                var subFluxRunModels = fluxRunModels.Where(p => p.SubStationID == substation.SubStationID).ToList();
                substation.InitPointModel(subAnalogPointModels, subFluxPointModels, subFluxRunModels);
            }

            foreach (var das in dasModels)
            {
                var dasSubstationModels = substationModels.Where(p => p.MonitoringServerID == das.MonitoringServerID).ToList();
                das.InitSubStation(dasSubstationModels);
            }
        }

        private IDas NewDas(MonitoringServerConfigModel monitoringServerConfigModel, List<SubStationModel> subStationModels)
        {
            var das = new Das(monitoringServerConfigModel, subStationModels, CreateDasLogMonitor);
            LogD.Info($"初始化  采集服务器 {monitoringServerConfigModel.MonitoringServerID}.");
            das.DasUpdateRealData += _dataBulkService.DasUpdateRealData;
            return das;
        }
        private PluginMonitor CreateDasLogMonitor(string dasId, string[] columns)
        {
            return _dasMonitor;
        }
    }
}
