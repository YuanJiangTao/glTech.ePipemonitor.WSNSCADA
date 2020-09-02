using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class SubStationModel
    {

        public List<AnalogPointModel> AnalogPointModels { get; } = new List<AnalogPointModel>();

        public List<FluxPointModel> FluxPointModels { get; } = new List<FluxPointModel>();

        private readonly List<SubStationRunModel> _subStationRunModels = new List<SubStationRunModel>();

        public List<SubStationRunModel> SubStationRunModels
        {
            get
            {
                return ModelHelper.CopyThenRemove(_subStationRunModels);
            }
        }
        public void InitPointModel(List<AnalogPointModel> analogPointModels, List<FluxPointModel> fluxPointModels,
            List<FluxRunModel> fluxRunModels)
        {
            lock (_lock)
            {
                AnalogPointModels.AddRange(analogPointModels);
                FluxPointModels.AddRange(fluxPointModels);

                AnalogPointModels.ForEach(p => p.InitPointModel());
                FluxPointModels.ForEach(p => p.InitPointModel(fluxRunModels?.FirstOrDefault(q => q.FluxID == p.FluxID)));
            }
            InitRealDataModel(RealDataModel);
        }
        public void InitPointModel(List<FluxPointModel> fluxPointModels,
            List<FluxRunModel> fluxRunModels)
        {
            if (fluxPointModels != null)
            {
                lock (_lock)
                {
                    fluxPointModels.ForEach(p => FluxPointModels.RemoveAll(q => q.FluxID == p.FluxID));
                    FluxPointModels.AddRange(fluxPointModels);
                    FluxPointModels.ForEach(p => p.InitPointModel(fluxRunModels?.FirstOrDefault(q => q.FluxID == p.FluxID)));
                }
            }
        }

        public RealDataModel RealDataModel { get; } = new RealDataModel();

        private void InitRealDataModel(RealDataModel realDataModel)
        {
            realDataModel.PointID = $"{SubStationID:D3}000";
            realDataModel.PointName = this.SubStationName;
            realDataModel.SubStationID = this.SubStationID;
            realDataModel.PointType = (int)this.PointType;
            realDataModel.PortNO = 0;

            realDataModel.Update(DateTime.Now, "初始化", PointState.Init, FeedState.Init);
        }
        private int _tcpOffCount = 0;
        public void UpdateNetOff(DateTime now)
        {
            var value = "网络中断";
            var state = PointState.OFF;
            _tcpOffCount++;
            if (_tcpOffCount < DasConfig.NetworkOffCount &&
                this.RealDataModel.RealState == (int)PointState.OK)
            {
                RealDataModel.Update(now);
                AnalogPointModels.ForEach(p => p.Update(now));
            }
            else
            {
                RealDataModel.Update(now, value, state, FeedState.OK);
                AnalogPointModels.ForEach(p => p.UpdateWhenSubstationOff(now));
            }

            SubStationRunModel.UpdateSubStationRun(ref _subStationRunModel, _subStationRunModels, RealDataModel, this);
        }

        public void UpdateAnalogOff(DateTime now)
        {
            RealDataModel.Update(now, "正常", PointState.OK);
            SubStationRunModel.UpdateSubStationRun(ref _subStationRunModel, _subStationRunModels, RealDataModel, this);
            AnalogPointModels.ForEach(p => p.Update(now));
        }


        public string PointID => SubStationID.ToString("{0:D3}000");

        public PointType PointType => PointType.SubStation;

        public int SubStationID { get; set; }
        public string SubStationName { get; set; }
        public string Location { get; set; }
        public string PortDefine { get; set; }
        public string EquipCode { get; set; }
        public int AnalogSwitchPortNum { get; set; }
        public int ControlPortNum { get; set; }
        public string Protocol { get; set; }
        public string Notes { get; set; }
        public int RMan { get; set; }
        public DateTime RDate { get; set; }
        public int LMan { get; set; }
        public DateTime LDate { get; set; }
        public bool IsRunLog { get; set; }
        public bool IsUsed { get; set; }
        public bool IsVoiceWarning { get; set; }
        public int MonitoringServerID { get; set; }
        public int ServicePortNO { get; set; }

        private int _bitErrorCount;
        internal void UpdateBitError(DateTime now)
        {
            var value = "通信误码";
            var state = PointState.BitError;
            _bitErrorCount++;
            if (_bitErrorCount >= 3)
            {
                // 连续大于三次才算通信误码.
                RealDataModel.Update(now, value, state);
            }
            else
            {
                // 只更新时间
                RealDataModel.Update(now);
            }
            SubStationRunModel.UpdateSubStationRun(ref _subStationRunModel, _subStationRunModels, RealDataModel, this);
        }


        private static object _lock = new object();
        private SubStationRunModel _subStationRunModel;
        internal void Update(DateTime now, SubStationData substationData)
        {
            RealDataModel.Update(now, "正常", PointState.OK);
            AnalogPointModels.ForEach(p => p.Update(now, substationData.SensorRealDataInfos));
            try
            {
                lock (_lock)
                {
                    FluxPointModels.ForEach(p => p.Update(now, AnalogPointModels.Select(p => p.RealDataModel).ToList()));
                }
                SubStationRunModel.UpdateSubStationRun(ref _subStationRunModel, _subStationRunModels, RealDataModel, this);
            }
            catch (Exception ex)
            {
                LogD.Error(ex.ToString());
            }
        }
        public bool DeleteFluxPoint(int fluxId)
        {
            lock (_lock)
            {
                return FluxPointModels.RemoveAll(p => p.FluxID == fluxId) == 1;
            }
        }
        public bool IsExist(int fluxId)
        {
            lock (_lock)
                return FluxPointModels.Exists(p => p.FluxID == fluxId);
        }

    }

    enum PointType : byte
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// 分站
        /// </summary>
        SubStation = 1,
        /// <summary>
        /// 模拟量
        /// </summary>
        Analog = 2,
        /// <summary>
        /// 开关量
        /// </summary>
        Switch = 3,
        /// <summary>
        /// 控制量
        /// </summary>
        Control = 4,
        /// <summary>
        /// 累计量
        /// </summary>
        Accumulation = 5,
        /// <summary>
        /// 逻辑量
        /// </summary>
        Logic = 6,
        /// <summary>
        /// DAS采集软件
        /// </summary>
        DASService = 7,
        /// <summary>
        /// 3G无线网卡
        /// </summary>
        PCMCIA = 8

    }
}
