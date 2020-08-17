using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class AnalogRunModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string Location { get; set; }
        public string MonitoringValue { get; set; }
        public DateTime MonitoringTime { get; set; }
        public int ValueState { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "PointID", "MonitoringTime" };
            }
        }
        public static IEnumerable<DataTable> List2Table(List<AnalogRunModel> analogRunModels)
        {
            foreach (var group in analogRunModels.GroupBy(o => o.MonitoringTime.ToString("yyyyMMdd")))
            {
                var models = group.ToList();
                var dt = BuildAnalogRun();
                foreach (var model in models)
                {
                    model.AddDataRow(dt);
                }
                dt.TableName = TableName(models.First().MonitoringTime);
                yield return dt;
            }
        }

        public static string TableName(DateTime startTime)
        {
            return "AnalogRun" + startTime.ToString("yyyyMMdd");
        }

        public void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["PointID"] = PointID;
            row["PointName"] = PointName;
            row["SubStationID"] = SubStationID;
            row["PortNO"] = PortNO;
            row["Location"] = Location;
            row["MonitoringValue"] = MonitoringValue;
            row["MonitoringTime"] = MonitoringTime;
            row["ValueState"] = ValueState;
            dt.Rows.Add(row);
        }
        /// <summary>
        /// 建立数据内存表结构.
        /// </summary>
        /// <returns>返回datatable</returns>
        public static DataTable BuildAnalogRun()
        {
            var dt = new DataTable("AnalogRun");

            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("MonitoringValue", typeof(string)));
            dt.Columns.Add(new DataColumn("MonitoringTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ValueState", typeof(int)));

            dt.PrimaryKey = new[] { dt.Columns["PointID"], dt.Columns["MonitoringTime"] };
            return dt;
        }


        public void Update(RealDataModel model)
        {
            MonitoringValue = model.RealValue;
            MonitoringTime = model.RealDate;
            ValueState = model.RealState;
        }
    }
}
