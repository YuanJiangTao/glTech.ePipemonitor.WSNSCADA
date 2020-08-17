using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class SubStationModel
    {

        public List<AnalogPointModel> AnalogPointModels { get; } = new List<AnalogPointModel>();

        public List<FluxPointModel> FluxPointModels { get; } = new List<FluxPointModel>();
        public void InitPointModel(List<AnalogPointModel> analogPointModels, List<FluxPointModel> fluxPointModels,
            List<FluxRunModel> fluxRunModels)
        {
            AnalogPointModels.AddRange(analogPointModels);
            FluxPointModels.AddRange(fluxPointModels);

            AnalogPointModels.ForEach(p => p.InitPointModel());
            FluxPointModels.ForEach(p => p.InitPointModel(fluxRunModels?.FirstOrDefault(q => q.FluxID == p.FluxID)));
            InitRealDataModel(RealDataModel);
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
                (this.RealDataModel.RealState == (int)PointState.AC
                    || this.RealDataModel.RealState == (int)PointState.DC))
            {
                RealDataModel.Update(now);
            }
            else
            {
                RealDataModel.Update(now, value, state, FeedState.OK);
            }
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
        }

        internal void Update(DateTime now, SubStationData substationData)
        {
            RealDataModel.Update(now, "正常", PointState.OK);
            AnalogPointModels.ForEach(p => p.Update(now, substationData.SensorRealDataInfos));
            try
            {
                FluxPointModels.ForEach(p => p.Update(now, AnalogPointModels.Select(p => p.RealDataModel).ToList()));
            }
            catch (Exception ex)
            {
                LogD.Error(ex.ToString());
            }
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
