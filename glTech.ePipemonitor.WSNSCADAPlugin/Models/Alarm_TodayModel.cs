using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Markup.Localizer;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class Alarm_TodayModel
    {
        public string PointID { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public int PointType { get; set; }
        public string PointName { get; set; }
        public string Location { get; set; }
        public float AlarmValue { get; set; }
        public int AlarmState { get; set; }
        public string AlarmStateName { get; set; }
        public int State { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpanTime { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatmentTime { get; set; }
        public int Writer { get; set; }
        public int TableType { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "PointID", "StartTime" };
            }
        }

        public static IEnumerable<DataTable> List2Table(List<Alarm_TodayModel> alarmTodayModels)
        {
            var dt = BuildAlarmToday();
            foreach (var model in alarmTodayModels)
            {
                model.AddDataRow(dt);
            }
            dt.TableName = TableName();
            yield return dt;
        }
        public static string TableName()
        {
            return "Alarm_Today";
        }

        /// <summary>
        /// 建立数据内存表结构.
        /// </summary>
        /// <returns>返回datatable</returns>
        public static DataTable BuildAlarmToday()
        {
            var dt = new DataTable("Alarm_Today");
            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));
            dt.Columns.Add(new DataColumn("PointType", typeof(int)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmValue", typeof(float)));
            dt.Columns.Add(new DataColumn("AlarmState", typeof(int)));
            dt.Columns.Add(new DataColumn("AlarmStateName", typeof(string)));
            dt.Columns.Add(new DataColumn("State", typeof(int)));
            dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SpanTime", typeof(int)));
            dt.Columns.Add(new DataColumn("Treatment", typeof(string)));
            dt.Columns.Add(new DataColumn("TreatmentTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Writer", typeof(int)));
            dt.Columns.Add(new DataColumn("TableType", typeof(int)));
            return dt;
        }
        public void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["PointID"] = PointID;
            row["SubStationID"] = SubStationID;
            row["PortNO"] = PortNO;
            row["PointType"] = PointType;
            row["PointName"] = PointName;

            row["Location"] = Location;
            row["AlarmValue"] = AlarmValue;
            row["AlarmState"] = AlarmState;
            row["AlarmStateName"] = AlarmStateName;

            row["State"] = State;
            row["StartTime"] = StartTime;
            row["EndTime"] = EndTime;
            row["SpanTime"] = SpanTime;
            row["Treatment"] = Treatment;
            row["TreatmentTime"] = DateTime.Now;

            row["Writer"] = "";
            row["TableType"] = TableType;
            dt.Rows.Add(row);
        }


        internal static void UpdateAlarmToday(ref Alarm_TodayModel alarmTodayModel, List<Alarm_TodayModel> alarmTodayModels, RealDataModel realDataModel,
            AnalogPointModel analogPointModel,
            Func<PointState, bool> isAlarmState)
        {
            if (alarmTodayModel == null)
            {
                NewAlarmAndSaveDatabase(out alarmTodayModel, alarmTodayModels, realDataModel, analogPointModel, isAlarmState);
            }
            else
            {
                alarmTodayModel.EndTime = realDataModel.RealDate;

                if (alarmTodayModel.IsRequireNew(realDataModel))
                {
                    // 有新的记录, 把老记录状态置1
                    alarmTodayModel.State = 1;
                }
                var realValue = realDataModel.RealValue.Value<float>();

                if (realValue > alarmTodayModel.AlarmValue
                    && !alarmTodayModel.IsRequireNew(realDataModel))
                {
                    // 没有新记录情况下, 把报警值设置为最大值.
                    alarmTodayModel.AlarmValue = realValue;
                }

                if (isAlarmState((PointState)alarmTodayModel.AlarmState) &&
                    (alarmTodayModel.IsTimeToSave(realDataModel) || alarmTodayModel.IsRequireNew(realDataModel)))
                {
                    // 报警情况下, 写入数据库.
                    var newM = alarmTodayModel.DeepClone();
                    Alarm_TodayModel existM;
                    if ((existM = alarmTodayModels.FirstOrDefault(o => o.PointID == newM.PointID && o.StartTime == newM.StartTime && o.AlarmState == newM.AlarmState)) != null)
                    {
                        existM.State = newM.State;
                        existM.AlarmValue = newM.AlarmValue;
                        existM.EndTime = newM.EndTime;
                    }
                    else
                        alarmTodayModels.Add(alarmTodayModel.DeepClone());
                }

                if (alarmTodayModel.IsRequireNew(realDataModel))
                {
                    // 有新记录.
                    NewAlarmAndSaveDatabase(out alarmTodayModel, alarmTodayModels, realDataModel, analogPointModel, isAlarmState);
                }
            }
        }

        public bool IsRequireNew(RealDataModel realDataModel)
        {
            // 如果状态变动, 那么写入一条数据.
            if (AlarmState != realDataModel.RealState)
            {
                return true;
            }
            return false;
        }
        public bool IsTimeToSave(RealDataModel realDataModel)
        {
            // 如果大于10秒, 那么写入数据, 延续报警.
            //if (realDataModel.RealTime.Subtract(_lastSaveTime).TotalSeconds >= 10)
            //{
            //_lastSaveTime = realDataModel.RealTime;
            return true;
            //}

            //return false;
        }

        private static void NewAlarmAndSaveDatabase(out Alarm_TodayModel alarmTodayModel, List<Alarm_TodayModel> alarmTodayModels,
          RealDataModel realDataModel, AnalogPointModel analogPointModel, Func<PointState, bool> isAlarmState)
        {
            // 需要新增加一条数据.
            alarmTodayModel = NewAlarmTodayModel(realDataModel, analogPointModel);

            var newStartTime = alarmTodayModel.StartTime;
            var valueState = alarmTodayModel.AlarmState;
            var existSameStartTime = alarmTodayModels.Find(o => newStartTime.Subtract(o.StartTime).TotalSeconds < 1
                && o.AlarmState == valueState);
            if (existSameStartTime != null)
            {
                // 只有当开始时间和结束时间不一致的时候, 排除初始化出现主键冲突问题.
                alarmTodayModels.Remove(existSameStartTime);

                LogD.Warn($"分站{alarmTodayModel.PointID} 状态{existSameStartTime.AlarmState}出现一秒内多次状态报警状态改变, 会发生主键冲突.");
            }

            if (isAlarmState((PointState)alarmTodayModel.AlarmState))
            {
                alarmTodayModels.Add(alarmTodayModel.DeepClone()); // 直接写入数据库
            }
        }

        private static Alarm_TodayModel NewAlarmTodayModel(RealDataModel realData, AnalogPointModel analogPointModel)
        {
            var model = new Alarm_TodayModel();
            model.PointID = realData.PointID;
            model.SubStationID = realData.SubStationID;
            model.PortNO = realData.PortNO;
            model.PointName = realData.PointName;
            model.Location = analogPointModel.Location;
            model.StartTime = realData.RealDate;
            model.EndTime = realData.RealDate;
            model.AlarmValue = realData.RealValue.Value<float>();
            model.AlarmState = realData.RealState;
            model.AlarmStateName = GetAlarmStateName((PointState)realData.RealState);
            model.State = 0;
            model.StartTime = realData.RealDate;
            model.EndTime = realData.RealDate;
            model.SpanTime = 0;
            model.TableType = 2;
            return model;
        }

        public static string GetAlarmStateName(PointState state)
        {
            var alarmStateName = string.Empty;
            switch (state)
            {
                case PointState.UnKnow:
                    break;
                case PointState.Init:
                    break;
                case PointState.OFF:
                    alarmStateName = "模拟量断线";
                    break;
                case PointState.ServiceOff:
                    break;
                case PointState.Unused:
                    break;
                case PointState.OverflowOFF:
                    break;
                case PointState.UnderflowOFF:
                    break;
                case PointState.SubStationOFF:
                    alarmStateName = "分站断线";
                    break;
                case PointState.AC:
                    break;
                case PointState.DC:
                    break;
                case PointState.BitError:
                    break;
                case PointState.NetworkInterruption:
                    break;
                case PointState.OK:
                    break;
                case PointState.UpperLimitSwitchingOff:
                    alarmStateName = "模拟量上限断电";
                    break;
                case PointState.UpperLimitWarning:
                    alarmStateName = "模拟量上限报警";
                    break;
                case PointState.UpperLimitResume:
                    alarmStateName = "模拟量上限恢复";
                    break;
                case PointState.UpperLimitEarlyWarning:
                    alarmStateName = "模拟量上限预警";
                    break;
                case PointState.LowerLimitSwitchingOff:
                    alarmStateName = "模拟量下限断电";
                    break;
                case PointState.LowerLimitWarning:
                    alarmStateName = "模拟量下限报警";
                    break;
                case PointState.LowerLimitResume:
                    alarmStateName = "模拟量下限恢复";
                    break;
                case PointState.LowerLimitEarlyWarning:
                    alarmStateName = "模拟量下限预警";
                    break;
                case PointState.State0:
                    break;
                case PointState.State1:
                    break;
                case PointState.State2:
                    break;
                default:
                    alarmStateName = "报警";
                    break;
            }
            return alarmStateName;
        }
    }
}
