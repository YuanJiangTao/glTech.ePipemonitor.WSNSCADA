using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class AnalogStatisticModel
    {

        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string Location { get; set; }
        public string UnitName { get; set; }
        public string MonitoringValue { get; set; }
        public int State { get; set; }
        public float MaxValue { get; set; }
        public DateTime MaxValueTime { get; set; }
        public float MinValue { get; set; }
        public DateTime MinValueTime { get; set; }
        public float SumValue { get; set; }
        public int SumCount { get; set; }
        public string AvgValue { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "PointID", "StartTime" };
            }
        }
        public static IEnumerable<DataTable> List2Table(List<AnalogStatisticModel> analogStatisticModels)
        {
            foreach (var group in analogStatisticModels.GroupBy(o => o.EndTime.ToString("yyyyMMdd")))
            {
                var models = group.ToList();
                var dt = BuildAnalogStatistic();
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
        public static DataTable BuildAnalogStatistic()
        {
            var dt = new DataTable("AnalogStatistic");
            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
            dt.Columns.Add(new DataColumn("MonitoringValue", typeof(string)));
            dt.Columns.Add(new DataColumn("State", typeof(int)));
            dt.Columns.Add(new DataColumn("MaxValue", typeof(float)));
            dt.Columns.Add(new DataColumn("MaxValueTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("MinValue", typeof(float)));
            dt.Columns.Add(new DataColumn("MinValueTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SumValue", typeof(float)));
            dt.Columns.Add(new DataColumn("SumCount", typeof(int)));
            dt.Columns.Add(new DataColumn("AvgValue", typeof(string)));
            dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));

            return dt;
        }

        public void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["PointID"] = PointID;
            row["PointName"] = PointName;
            row["SubStationID"] = SubStationID;
            row["PortNO"] = PortNO;
            row["Location"] = Location;
            row["UnitName"] = UnitName;
            row["MonitoringValue"] = MonitoringValue;
            row["State"] = State;
            row["MaxValue"] = MaxValue;
            row["MaxValueTime"] = MaxValueTime;
            row["MinValue"] = MinValue;
            row["MinValueTime"] = MinValueTime;
            row["SumValue"] = SumValue;
            row["SumCount"] = SumCount;
            row["AvgValue"] = AvgValue;

            row["StartTime"] = StartTime;
            row["EndTime"] = EndTime;
            dt.Rows.Add(row);
        }

        public static string TableName(DateTime startTime)
        {
            return "AnalogStatistic" + startTime.ToString("yyyyMMdd");
        }
        public static void UpdateAnalogStatistic(ref AnalogStatisticModel analogStatisticModel,
            List<AnalogStatisticModel> analogStatisticModels,
            RealDataModel realDataModel, AnalogPointModel model)
        {
            if (analogStatisticModel == null)
            {
                analogStatisticModel = NewAnalogStatisticModel(realDataModel, model);
                analogStatisticModels.Add(analogStatisticModel);
            }
            else
            {
                if (!realDataModel.IaAnalogOK)
                    return;
                var realValue = realDataModel.RealValue.Value<float>();
                if (realValue < analogStatisticModel.MinValue)
                {
                    analogStatisticModel.MinValue = realValue;
                    analogStatisticModel.MinValueTime = realDataModel.RealDate;
                }
                else if (realValue > analogStatisticModel.MaxValue)
                {
                    analogStatisticModel.MaxValue = realValue;
                    analogStatisticModel.MaxValueTime = realDataModel.RealDate;
                    analogStatisticModel.State = realDataModel.RealState;
                }

                analogStatisticModel.SumValue += realValue;
                analogStatisticModel.SumCount++;
                if (analogStatisticModel.IsTimeToSave(realDataModel))
                {
                    var newM = analogStatisticModel.DeepClone();
                    AnalogStatisticModel existM;
                    if ((existM = analogStatisticModels.FirstOrDefault(o => o.PointID == newM.PointID && o.StartTime == newM.StartTime)) != null)
                    {
                        existM.SumCount = newM.SumCount;
                        existM.SumValue = newM.SumValue;
                        existM.MaxValueTime = newM.MaxValueTime;
                        existM.MaxValue = newM.MaxValue;
                        existM.MinValue = newM.MinValue;
                        existM.MinValueTime = newM.MinValueTime;
                    }
                    else
                        analogStatisticModels.Add(analogStatisticModel.DeepClone());
                }

                if (analogStatisticModel.IsRequireNew(realDataModel))
                {
                    // 需要新增加一条数据.
                    analogStatisticModel = NewAnalogStatisticModel(realDataModel, model);
                    analogStatisticModels.Add(analogStatisticModel.DeepClone());
                }
            }
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
        public bool IsRequireNew(RealDataModel realDataModel)
        {
            // 如果大于5分钟, 那么写入一条数据.
            return RoundDown(realDataModel.RealDate, TimeSpan.FromMinutes(5)) != StartTime;
        }

        private static DateTime RoundUp(DateTime dt, TimeSpan ts)
        {
            return new DateTime(((dt.Ticks + ts.Ticks - 1) / ts.Ticks) * ts.Ticks);
        }

        private static DateTime RoundDown(DateTime dt, TimeSpan ts)
        {
            return new DateTime(((dt.Ticks - 1) / ts.Ticks) * ts.Ticks);
        }
        private static AnalogStatisticModel NewAnalogStatisticModel(RealDataModel realDataModel, AnalogPointModel analogPointModel)
        {
            var model = new AnalogStatisticModel();
            model.PointID = realDataModel.PointID;
            model.PointName = realDataModel.PointName;
            model.SubStationID = realDataModel.SubStationID;
            model.PortNO = realDataModel.PortNO;
            model.Location = analogPointModel.Location;
            model.UnitName = analogPointModel.UnitName;
            model.MonitoringValue = realDataModel.RealValue;
            model.State = realDataModel.RealState;
            model.StartTime = RoundDown(realDataModel.RealDate, TimeSpan.FromMinutes(5));
            model.EndTime = RoundUp(realDataModel.RealDate, TimeSpan.FromMinutes(5));
            model.MinValue = realDataModel.RealValue.Value<float>();
            model.MinValueTime = realDataModel.RealDate;
            model.MaxValue = realDataModel.RealValue.Value<float>();
            model.MaxValueTime = realDataModel.RealDate;
            model.SumValue = realDataModel.RealValue.Value<float>();
            model.SumCount = 1;
            model.AvgValue = realDataModel.RealValue;
            return model;
        }
    }
}
