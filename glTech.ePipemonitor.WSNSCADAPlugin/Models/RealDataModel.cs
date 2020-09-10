using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class RealDataModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public int PointType { get; set; }
        public string RealValue { get; set; }
        public DateTime RealDate { get; set; }
        public int RealState { get; set; } = 1;
        public int FeedState { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "PointID" };
            }
        }
        public static IEnumerable<DataTable> List2Table(List<RealDataModel> realDataModels)
        {
            var dt = BuildRealData();
            foreach (var model in realDataModels)
            {
                var row = dt.NewRow();
                FillRealDataRow(row, model.PointID, model.PointName, model.SubStationID,
                    model.PortNO, model.PointType, model.RealValue,
                    model.RealDate, model.RealState, model.FeedState);
                dt.Rows.Add(row);
            }
            yield return dt;
        }

        public override string ToString()
        {
            return $"{PointID}:{RealValue}";
        }

        /// <summary>
        /// 建立数据内存表结构.
        /// </summary>
        /// <returns>返回datatable</returns>
        private static DataTable BuildRealData()
        {
            DataTable dt = new DataTable("RealData");
            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));

            dt.Columns.Add(new DataColumn("PointType", typeof(int)));
            dt.Columns.Add(new DataColumn("RealValue", typeof(string)));

            dt.Columns.Add(new DataColumn("RealDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("RealState", typeof(int)));
            dt.Columns.Add(new DataColumn("FeedState", typeof(int)));

            return dt;
        }

        /// <summary>
        /// 填充RealData内存表中的一行
        /// </summary>
        private static void FillRealDataRow(DataRow row, string pointID, string pointName,
            int subStationID, int portNO, int pointType,
            string realValue, DateTime realDate, int realState,
            int feedState)
        {
            row["PointID"] = pointID;
            row["PointName"] = pointName;
            row["SubStationID"] = subStationID;
            row["PortNO"] = portNO;
            row["PointType"] = pointType;
            row["RealValue"] = realValue;
            row["RealDate"] = realDate;
            row["RealState"] = realState;
            row["FeedState"] = feedState;
        }
        internal void Update(DateTime now, string value, PointState pointState, FeedState feedState= Models.FeedState.OK)
        {
            RealDate = now;
            RealValue = value;
            RealState = (int)pointState;
            FeedState = (int)feedState;
        }
        public void Update(DateTime realTime)
        {
            RealDate = realTime;
        }
        public bool IaAnalogOK
        {
            get
            {
                return RealState == (int)PointState.OK || RealState == (int)PointState.UpperLimitEarlyWarning || RealState == (int)PointState.UpperLimitResume
                    || RealState == (int)PointState.UpperLimitSwitchingOff || RealState == (int)PointState.UpperLimitWarning
                    || RealState == (int)PointState.LowerLimitEarlyWarning || RealState == (int)PointState.LowerLimitResume
                    || RealState == (int)PointState.LowerLimitSwitchingOff || RealState == (int)PointState.LowerLimitWarning;
            }
        }
    }
}
