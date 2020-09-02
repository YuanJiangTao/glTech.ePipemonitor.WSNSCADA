using PluginContract.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using System.Linq;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Repositories
{
    class SqlRepo
    {
        public IDapper Dapper { get; private set; }
        public SqlRepo(IDatabaseConfig databaseConfig)
        {
            Dapper = new DapperBase(databaseConfig);
        }

        public IEnumerable<MonitoringServerConfigModel> GetMonitoringServerConfigs()
        {
            var sql = "select * from MonitoringServerConfig where [State]=1 ";
            return Dapper.Query<MonitoringServerConfigModel>(sql);
        }
        public MonitoringServerConfigModel GetMonitoringServerConfigModelById(int monitorServerId)
        {
            var sql = $"select * from MonitoringServerConfig where [State]=1  AND MonitoringServerID={monitorServerId}";
            return Dapper.Query<MonitoringServerConfigModel>(sql).FirstOrDefault();
        }
        public IEnumerable<SubStationModel> GetSubStationModelsByMonitorServerId(int monitorServerId)
        {
            var sql = $"select * from SubStation where [IsUsed]=1 AND MonitoringServerID={monitorServerId}";
            return Dapper.Query<SubStationModel>(sql);
        }
        public IEnumerable<SubStationModel> GetSubStationModels()
        {
            var sql = "select * from SubStation where [IsUsed]=1";
            return Dapper.Query<SubStationModel>(sql);
        }

        public IEnumerable<AnalogPointModel> GetAnalogPointModels()
        {
            var sql = "select * from AnalogPoint where [IsUsed]=1";
            return Dapper.Query<AnalogPointModel>(sql);
        }

        public IEnumerable<FluxPointModel> GetFluxPointModels()
        {
            var sql = "select * from FluxPoint where [IsUsed]=1 ";
            return Dapper.Query<FluxPointModel>(sql);
        }
        public List<FluxRunModel> GetFluxRunModels(short[] fluxIds)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            var hour = DateTime.Now.Hour;
            var sql = $"select * from FluxRun where FluxId in ('{string.Join("','", fluxIds)}') " +
                $" and Year = '{year}' " +
                $" and Month = '{month}' " +
                $" and day = '{day}' " +
                $" and hour = '{hour}' " +
                $" and Flag = 1";

            Console.WriteLine("初始化FluxRun:" + sql);
            return Dapper.Query<FluxRunModel>(sql).ToList();
        }

        internal IEnumerable<CustomCommandModel> GetLastCustomCommand(DateTime now)
        {
            var sql = "select * from CustomCommand WHERE [State]=0";
            return Dapper.Query<CustomCommandModel>(sql);
        }

        internal bool Update(CustomCommandModel customCommandModel, string responseData, CustomCommandResponseState responseState)
        {
            customCommandModel.ResponseData = responseData;
            customCommandModel.State = (byte)responseState;
            customCommandModel.ResponseTime = DateTime.Now;
            var sql = $"Update CustomCommand set [State] ={customCommandModel.State} , ResponseData='{customCommandModel.ResponseData}', ResponseTime = '{customCommandModel.ResponseTime:yyyy-MM-dd HH:mm:ss:fff}'" +
                $" where CMDKey={customCommandModel.CMDKey} AND SubStationID={customCommandModel.SubStationID} AND ChannelNO={customCommandModel.ChannelNO} AND CreateTime='{customCommandModel.CreateTime}'";
            return Dapper.Execute(sql) > 0;
        }

        internal SubStationModel GetSubStationModel(int substationId)
        {
            var sql = $"select * from SubStation where [IsUsed]=1 AND SubstationId={substationId}";
            return Dapper.Query<SubStationModel>(sql).FirstOrDefault();
        }

        internal IEnumerable<AnalogPointModel> GetAnalogPointModelsBySubstationId(int substationId)
        {
            var sql = $"select * from AnalogPoint where IsUsed=1 AND SubStationID={substationId}";
            return Dapper.Query<AnalogPointModel>(sql);
        }
        public IEnumerable<FluxPointModel> GetFluxPointModelsBySubstationId(int substationId)
        {
            var sql = $"select * from FluxPoint where IsUsed=1 AND SubStationID={substationId}";
            return Dapper.Query<FluxPointModel>(sql);
        }
        public IEnumerable<FluxPointModel> GetFluxPointModelById(int fluxId)
        {
            var sql = $"select * from FluxPoint where IsUsed=1 AND FluxId={fluxId}";
            return Dapper.Query<FluxPointModel>(sql);
        }

    }
}
