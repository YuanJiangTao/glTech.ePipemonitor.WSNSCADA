using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class AnalogPointModel
    {
        public AnalogPointModel()
        {

        }
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string Location { get; set; }
        public string EquipCode { get; set; }
        public string UnitName { get; set; }


        internal void InitPointModel()
        {
            RealDataModel.PointID = PointID;
            RealDataModel.PointName = PointName;
            RealDataModel.PortNO = PortNO;
            RealDataModel.PointType = (int)PointType.Analog;
            RealDataModel.SubStationID = SubStationID;
            RealDataModel.Update(DateTime.Now, "初始化", PointState.Init);

            _analogRunModel.PointID = PointID;
            _analogRunModel.PointName = PointName;
            _analogRunModel.SubStationID = SubStationID;
            _analogRunModel.PortNO = PortNO;
            _analogRunModel.Location = Location;
        }

        public float ErrorRand { get; set; }
        public string SignalSystemName { get; set; }
        public int SignType { get; set; }
        public int RangeCode { get; set; }
        public int Precision { get; set; }
        public int MaxValue { get; set; }
        public int MidValue { get; set; }
        public int MinValue { get; set; }
        public float UpperLimitSwitchingOff { get; set; }
        public float UpperLimitWarning { get; set; }
        public float UpperLimitResume { get; set; }
        public float UpperLimitEarlyWarning { get; set; }
        public int UpperLimitSwitchingOffPort { get; set; }
        public int UpperLimitWarningPort { get; set; }
        public float LowerLimitSwitchingOff { get; set; }
        public float LowerLimitWarning { get; set; }
        public float LowerLimitResume { get; set; }
        public float LowerLimitEarlyWarning { get; set; }
        public int LowerLimitSwitchingOffPort { get; set; }
        public int LowerLimitWarningPort { get; set; }
        public int OverFlowPort { get; set; }
        public int LineBreakPort { get; set; }
        public int NegativeFleePort { get; set; }
        public bool IsUsed { get; set; }

        public RealDataModel RealDataModel { get; } = new RealDataModel();
        public List<AnalogRunModel> AnalogRunModels
        {
            get
            {
                return ModelHelper.CopyThenRemove(_analogRunModels);
            }
        }
        public List<AnalogAlarmModel> AnalogAlarmModels
        {
            get
            {
                return ModelHelper.CopyThenRemove(_analogAlarmModels);
            }
        }
        public List<Alarm_TodayModel> Alarm_TodayModels
        {
            get
            {
                return ModelHelper.CopyThenRemove(_alarmTodayModels);
            }
        }
        public List<AnalogStatisticModel> AnalogStatisticModels
        {
            get
            {
                return ModelHelper.CopyThenRemove(_analogStatisticModels);
            }
        }


        internal void Update(DateTime now, List<SensorRealDataInfo> sensorRealDataInfos)
        {
            _analogOffCount = 0;
            var sensorRealData = sensorRealDataInfos.FirstOrDefault(p => p.EquipCodes.Exists(q => q == EquipCode));
            PointState valueState = PointState.OK;
            HandleAnalogAlarm(sensorRealData.Value, ref valueState);
            RealDataModel.Update(now, sensorRealData.Value.ToString("f2"), valueState);
            _analogRunModel.Update(RealDataModel);
            _analogRunModels.Add(_analogRunModel);
            AnalogStatisticModel.UpdateAnalogStatistic(ref _analogStatisticModel, _analogStatisticModels, RealDataModel, this);
            AnalogAlarmModel.UpdateAnalogAlarm(ref _analogAlarmModel, _analogAlarmModels, RealDataModel,this, IsAlarmState);
            Alarm_TodayModel.UpdateAlarmToday(ref _alarmTodayModel, _alarmTodayModels, RealDataModel,this, IsAlarmState);
        }
        /// <summary>
        /// 根据状态值判断模拟量是否报警
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static bool IsAlarmState(PointState state)
        {
            var ret = false;
            switch (state)
            {
                case PointState.LowerLimitEarlyWarning: //注意：分站断线，模拟量不属于报警，但是显示时需要显示报警色
                case PointState.LowerLimitResume:
                case PointState.LowerLimitSwitchingOff:
                case PointState.LowerLimitWarning:
                case PointState.UpperLimitEarlyWarning:
                case PointState.UpperLimitResume:
                case PointState.UpperLimitSwitchingOff:
                case PointState.UpperLimitWarning:
                case PointState.OFF:
                    ret = true;
                    break;
            }
            return ret;
        }
        private void HandleAnalogAlarm(float value, ref PointState pointState)
        {
            var upperLimit = 9999;
            if (UpperLimitEarlyWarning != upperLimit && value >= UpperLimitEarlyWarning)
            {
                pointState = PointState.UpperLimitEarlyWarning;
            }
            if (UpperLimitResume != upperLimit && value >= UpperLimitResume)
            {
                pointState = PointState.UpperLimitResume;
            }
            if (UpperLimitWarning != upperLimit && value >= UpperLimitWarning)
            {
                pointState = PointState.UpperLimitWarning;
            }
            if (UpperLimitSwitchingOff != upperLimit && value >= UpperLimitSwitchingOff)
            {
                pointState = PointState.UpperLimitSwitchingOff;
            }
            var lowLimit = -9999;
            if (LowerLimitEarlyWarning != lowLimit && value <= LowerLimitEarlyWarning)
            {
                pointState = PointState.LowerLimitEarlyWarning;
            }
            if (LowerLimitResume != lowLimit && value <= LowerLimitResume)
            {
                pointState = PointState.LowerLimitResume;
            }
            if (LowerLimitWarning != lowLimit && value <= LowerLimitWarning)
            {
                pointState = PointState.LowerLimitWarning;
            }
            if (LowerLimitSwitchingOff != lowLimit && value <= LowerLimitSwitchingOff)
            {
                pointState = PointState.LowerLimitSwitchingOff;
            }
        }
        internal void Update(DateTime now)
        {
            _analogOffCount++;
            if (new[] { PointState.UnKnow, PointState.Init }.All(o => o != (PointState)RealDataModel.RealState) && _analogOffCount <= DasConfig.SensorTimeoutCount)
            {
                RealDataModel.Update(now);
                //处于屏蔽次数之内
                _analogRunModels.Add(_analogRunModel);
                AnalogStatisticModel.UpdateAnalogStatistic(ref _analogStatisticModel, _analogStatisticModels, RealDataModel, this);
                AnalogAlarmModel.UpdateAnalogAlarm(ref _analogAlarmModel, _analogAlarmModels, RealDataModel, this, IsAlarmState);
                Alarm_TodayModel.UpdateAlarmToday(ref _alarmTodayModel, _alarmTodayModels, RealDataModel, this, IsAlarmState);
            }
            else
            {
                var value = "模拟量断线";
                var valueState = PointState.OFF;
                //是真正的断线
                RealDataModel.Update(now, value, valueState);
                _analogRunModel.Update(RealDataModel);
                _analogRunModels.Add(_analogRunModel);
                AnalogStatisticModel.UpdateAnalogStatistic(ref _analogStatisticModel, _analogStatisticModels, RealDataModel, this);
                AnalogAlarmModel.UpdateAnalogAlarm(ref _analogAlarmModel, _analogAlarmModels, RealDataModel, this, IsAlarmState);
                Alarm_TodayModel.UpdateAlarmToday(ref _alarmTodayModel, _alarmTodayModels, RealDataModel, this, IsAlarmState);
            }
        }

        private readonly List<AnalogRunModel> _analogRunModels = new List<AnalogRunModel>();
        private readonly List<Alarm_TodayModel> _alarmTodayModels = new List<Alarm_TodayModel>();
        private readonly List<AnalogAlarmModel> _analogAlarmModels = new List<AnalogAlarmModel>();
        private readonly List<AnalogStatisticModel> _analogStatisticModels = new List<AnalogStatisticModel>();
        private readonly AnalogRunModel _analogRunModel = new AnalogRunModel();
        private int _analogOffCount = 0;
        private AnalogStatisticModel _analogStatisticModel;
        private Alarm_TodayModel _alarmTodayModel;
        private AnalogAlarmModel _analogAlarmModel;


    }
}
