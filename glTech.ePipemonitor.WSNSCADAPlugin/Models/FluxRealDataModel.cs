using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class FluxRealDataModel
    {
        public short FluxID { get; set; }
        public DateTime RealDate { get; set; }
        public int MethaneChromaState { get; set; } = 1;
        public string MethaneChromaRealValue { get; set; } = "初始化";
        public int FluxState { get; set; } = 1;
        public string FluxRealValue { get; set; } = "初始化";
        public int PressureState { get; set; } = 1;
        public string PressureRealValue { get; set; } = "初始化";
        public int TemperatureState { get; set; } = 1;
        public string TemperatureRealValue { get; set; } = "初始化";
        public int COState { get; set; } = 1;
        public string CORealValue { get; set; } = "初始化";
        public string PureFluxRealValue { get; set; } = "初始化";
        public string IndustrialFluxRealValue { get; set; } = "初始化";
        public string FluxHour { get; set; } = "初始化";
        public string PureFluxHour { get; set; } = "初始化";
        public string IndustrialFluxHour { get; set; } = "初始化";
        public string FluxDay { get; set; } = "初始化";
        public string PureFluxDay { get; set; } = "初始化";
        public string IndustrialFluxDay { get; set; } = "初始化";
        public string FluxMonth { get; set; } = "初始化";
        public string PureFluxMonth { get; set; } = "初始化";
        public string IndustrialFluxMonth { get; set; } = "初始化";
        public string FluxTotal { get; set; } = "初始化";
        public string PureFluxTotal { get; set; } = "初始化";
        public string IndustrialFluxTotal { get; set; } = "初始化";

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "FluxID" };
            }
        }

        public static IEnumerable<DataTable> List2Table(List<FluxRealDataModel> models)
        {
            var dt = BuildFluxRealData();
            foreach (var model in models)
            {
                model.AddDataRow(dt);
            }
            dt.TableName = TableName();

            yield return dt;
        }
        private static string TableName()
        {
            return "FluxRealData";
        }
        private void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["FluxID"] = FluxID;
            row["RealDate"] = RealDate;
            row["MethaneChromaState"] = MethaneChromaState;
            row["MethaneChromaRealValue"] = MethaneChromaRealValue;
            row["FluxState"] = FluxState;
            row["FluxRealValue"] = FluxRealValue;
            row["PressureState"] = PressureState;
            row["PressureRealValue"] = PressureRealValue;
            row["TemperatureState"] = TemperatureState;
            row["TemperatureRealValue"] = TemperatureRealValue;
            row["COState"] = COState;
            row["CORealValue"] = CORealValue;

            row["PureFluxRealValue"] = PureFluxRealValue;
            row["IndustrialFluxRealValue"] = IndustrialFluxRealValue;
            row["FluxHour"] = FluxHour;
            row["PureFluxHour"] = PureFluxHour;
            row["IndustrialFluxHour"] = IndustrialFluxHour;
            row["FluxDay"] = FluxDay;
            row["PureFluxDay"] = PureFluxDay;
            row["IndustrialFluxDay"] = IndustrialFluxDay;
            row["FluxMonth"] = FluxMonth;
            row["PureFluxMonth"] = PureFluxMonth;
            row["IndustrialFluxMonth"] = IndustrialFluxMonth;
            row["FluxTotal"] = FluxTotal;
            row["PureFluxTotal"] = PureFluxTotal;
            row["IndustrialFluxTotal"] = IndustrialFluxTotal;
            dt.Rows.Add(row);
        }
        public static DataTable BuildFluxRealData()
        {
            var dt = new DataTable("FluxRealData");

            dt.Columns.Add(new DataColumn("FluxID", typeof(string)));
            dt.Columns.Add(new DataColumn("RealDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("MethaneChromaState", typeof(int)));
            dt.Columns.Add(new DataColumn("MethaneChromaRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxState", typeof(int)));
            dt.Columns.Add(new DataColumn("FluxRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("PressureState", typeof(int)));
            dt.Columns.Add(new DataColumn("PressureRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("TemperatureState", typeof(int)));
            dt.Columns.Add(new DataColumn("TemperatureRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("COState", typeof(int)));
            dt.Columns.Add(new DataColumn("CORealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("PureFluxRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("IndustrialFluxRealValue", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxHour", typeof(string)));
            dt.Columns.Add(new DataColumn("PureFluxHour", typeof(string)));
            dt.Columns.Add(new DataColumn("IndustrialFluxHour", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxDay", typeof(string)));
            dt.Columns.Add(new DataColumn("PureFluxDay", typeof(string)));
            dt.Columns.Add(new DataColumn("IndustrialFluxDay", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxMonth", typeof(string)));
            dt.Columns.Add(new DataColumn("PureFluxMonth", typeof(string)));
            dt.Columns.Add(new DataColumn("IndustrialFluxMonth", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("PureFluxTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("IndustrialFluxTotal", typeof(string)));
            dt.PrimaryKey = new[] { dt.Columns["FluxID"] };
            return dt;
        }
    }


}
