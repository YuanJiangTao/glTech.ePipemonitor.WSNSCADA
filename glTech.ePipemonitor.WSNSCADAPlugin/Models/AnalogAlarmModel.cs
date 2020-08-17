using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class AnalogAlarmModel
    {
        public string PointID { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string PointName { get; set; }
        public string Location { get; set; }
        public float AlarmValue { get; set; }
        public float MaxValue { get; set; }
        public DateTime MaxValueTime { get; set; }
        public float MinValue { get; set; }
        public DateTime MinValueTime { get; set; }
        public float SumValue { get; set; }
        public int SumCount { get; set; }
        public float AvgValue { get; set; }
        public int AlarmState { get; set; }
        public int State { get; set; }
        public int AbnormalFeed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpanTime { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatmentTime { get; set; }
        public int Writer { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "PointID", "StartTime" };
            }
        }
        public static IEnumerable<DataTable> List2Table(List<AnalogAlarmModel> analogAlarmModels)
        {
            foreach (var group in analogAlarmModels.GroupBy(o => o.EndTime.ToString("yyyyMM")))
            {
                var models = group.ToList();
                var dt = BuildAnalogAlarm();
                foreach (var model in models)
                {
                    model.AddDataRow(dt);
                }
                dt.TableName = TableName(models.First().EndTime);
                yield return dt;
            }
        }
        /// <summary>
        /// 建立数据内存表结构.
        /// </summary>
        /// <returns>返回datatable</returns>
        public static DataTable BuildAnalogAlarm()
        {
            var dt = new DataTable("AnalogAlarm");
            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmValue", typeof(float)));
            dt.Columns.Add(new DataColumn("MaxValue", typeof(float)));
            dt.Columns.Add(new DataColumn("MaxValueTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("MinValue", typeof(float)));
            dt.Columns.Add(new DataColumn("MinValueTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SumValue", typeof(float)));
            dt.Columns.Add(new DataColumn("SumCount", typeof(int)));
            dt.Columns.Add(new DataColumn("AvgValue", typeof(float)));
            dt.Columns.Add(new DataColumn("AlarmState", typeof(int)));
            dt.Columns.Add(new DataColumn("State", typeof(int)));
            dt.Columns.Add(new DataColumn("AbnormalFeed", typeof(int)));
            dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SpanTime", typeof(int)));
            dt.Columns.Add(new DataColumn("Treatment", typeof(string)));
            dt.Columns.Add(new DataColumn("TreatmentTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Writer", typeof(int)));
            return dt;
        }

        public static string TableName(DateTime startTime)
        {
            return "AnalogAlarm" + startTime.ToString("yyyyMM");
        }

        public void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["PointID"] = PointID;
            row["SubStationID"] = SubStationID;
            row["PortNO"] = PortNO;
            row["PointName"] = PointName;
            row["Location"] = Location;

            row["AlarmValue"] = AlarmValue;
            row["MaxValue"] = MaxValue;
            row["MaxValueTime"] = MaxValueTime;
            row["MinValue"] = MinValue;
            row["MinValueTime"] = MinValueTime;

            row["SumValue"] = SumValue;
            row["SumCount"] = SumCount;
            row["AvgValue"] = AvgValue;
            row["AlarmState"] = AlarmState;
            row["State"] = State;
            row["AbnormalFeed"] = AbnormalFeed;
            row["StartTime"] = StartTime;

            row["EndTime"] = EndTime;
            row["SpanTime"] = SpanTime;
            row["Treatment"] = Treatment;
            row["TreatmentTime"] = TreatmentTime;
            row["Writer"] = Writer;
            dt.Rows.Add(row);
        }


        internal static void UpdateAnalogAlarm(ref AnalogAlarmModel analogAlarmModel,
            List<AnalogAlarmModel> analogAlarmModels,
            RealDataModel realDataModel, AnalogPointModel analogPointModel,
            Func<PointState, bool> isAlarmState)
        {
            if (analogAlarmModel == null)
            {
                NewAlarmAndSaveDatabase(out analogAlarmModel, analogAlarmModels, realDataModel, analogPointModel, isAlarmState);
            }
            else
            {
                analogAlarmModel.EndTime = realDataModel.RealDate;

                if (analogAlarmModel.IsRequireNew(realDataModel))
                {
                    // 有新的记录, 把老记录状态置1
                    analogAlarmModel.State = 1;
                }
                if (isAlarmState((PointState)analogAlarmModel.AlarmState) &&
                    (analogAlarmModel.IsTimeToSave(realDataModel) || analogAlarmModel.IsRequireNew(realDataModel)))
                {
                    // 报警情况下, 当状态改变或者时间大于10秒时候, 写入数据库.
                    var newM = analogAlarmModel.DeepClone();
                    AnalogAlarmModel existM;
                    if ((existM = analogAlarmModels.FirstOrDefault(o => o.PointID == newM.PointID && o.StartTime == newM.StartTime && o.AlarmState == newM.AlarmState)) != null)
                    {
                        existM.State = newM.State;
                        existM.EndTime = newM.EndTime;
                    }
                    else
                        analogAlarmModels.Add(newM);
                }
                var realValue = realDataModel.RealValue.Value<float>();
                if (realValue < analogAlarmModel.MinValue)
                {
                    analogAlarmModel.MinValue = realValue;
                    analogAlarmModel.MinValueTime = realDataModel.RealDate;
                }
                else if (realValue > analogAlarmModel.MaxValue)
                {
                    analogAlarmModel.MaxValue = realValue;
                    analogAlarmModel.MaxValueTime = realDataModel.RealDate;
                }

                analogAlarmModel.SumValue += realValue;
                analogAlarmModel.SumCount++;

                if (analogAlarmModel.IsRequireNew(realDataModel))
                {
                    // 有新记录.
                    NewAlarmAndSaveDatabase(out analogAlarmModel, analogAlarmModels, realDataModel, analogPointModel, isAlarmState);
                }
            }
        }
        /// <summary>
        /// 新增一条AnalogAlarm记录, 并且把它保存到数据库中.
        /// </summary>
        private static void NewAlarmAndSaveDatabase(out AnalogAlarmModel analogAlarmModel, List<AnalogAlarmModel> analogAlarmModels,
            RealDataModel realDataModel, AnalogPointModel analogPointModel, Func<PointState, bool> isAlarmState)
        {
            // 需要新增加一条数据.
            analogAlarmModel = NewAnalogAlarmModel(realDataModel, analogPointModel);

            if (isAlarmState((PointState)analogAlarmModel.AlarmState))
            {
                analogAlarmModels.Add(analogAlarmModel.DeepClone()); // 直接写入数据库
            }
        }
        private bool IsTimeToSave(RealDataModel realDataModel)
        {
            // 如果大于10秒, 那么写入数据, 延续报警.
            //if (realDataModel.RealTime.Subtract(_lastSaveTime).TotalSeconds >= 10)
            //_lastSaveTime = realDataModel.RealTime;
            return true;
        }
        public bool IsRequireNew(RealDataModel realDataModel)
        {
            //// 如果大于5分钟, 那么写入一条数据.
            //if (realDataModel.RealTime.Subtract(_lastSaveFiveTime).TotalMinutes >= 5)
            //{
            //    _lastSaveFiveTime = realDataModel.RealTime;
            //    return true;
            //}

            if (AlarmState != realDataModel.RealState)
            {
                return true;
            }
            return false;
        }
        private static AnalogAlarmModel NewAnalogAlarmModel(RealDataModel realData, AnalogPointModel analogPointModel)
        {
            var model = new AnalogAlarmModel();
            model.PointID = realData.PointID;
            model.SubStationID = realData.SubStationID;
            model.PortNO = realData.PortNO;
            model.PointName = realData.PointName;
            model.Location = analogPointModel.Location;
            model.StartTime = realData.RealDate;
            model.EndTime = realData.RealDate;
            model.AlarmValue = realData.RealValue.Value<float>();
            model.AlarmState = realData.RealState;
            model.MaxValue = realData.RealValue.Value<float>();
            model.MaxValueTime = realData.RealDate;
            model.MinValue = realData.RealValue.Value<float>();
            model.MinValueTime = realData.RealDate;
            model.AvgValue = realData.RealValue.Value<float>();
            model.SumValue += realData.RealValue.Value<float>();
            model.AbnormalFeed = 0;
            model.SumCount++;
            model.State = 0;
            model.SpanTime = 0;
            return model;
        }
    }
}
