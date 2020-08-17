using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class FluxRunModel
    {
        public Int16 FluxID { get; set; }
        public Int16 SubStationID { get; set; }
        public string Location { get; set; }
        public string FluxName { get; set; }
        public Byte ConcentrationPort { get; set; }
        public Byte FluxPort { get; set; }
        public Byte PressurePort { get; set; }
        public Byte TemperaturePort { get; set; }
        public bool PressureFlag { get; set; }
        public float StandardatMosphere { get; set; }
        public float MethaneChromaMax { get; set; }
        public DateTime MethaneChromaMaxTime { get; set; }
        public float TemperatureMax { get; set; }
        public DateTime TemperatureMaxTime { get; set; }
        public float PressureMax { get; set; }
        public DateTime PressureMaxTime { get; set; }
        public float FluxMax { get; set; }
        public DateTime FluxMaxTime { get; set; }
        public float MethaneChromaMin { get; set; }
        public DateTime MethaneChromaMinTime { get; set; }
        public float TemperatureMin { get; set; }
        public DateTime TemperatureMinTime { get; set; }
        public float PressureMin { get; set; }
        public DateTime PressureMinTime { get; set; }
        public float FluxMin { get; set; }
        public DateTime FluxMinTime { get; set; }
        public float FluxTotal { get; set; }
        public float PureFluxTotal { get; set; }
        public float IndustrialFluxTotal { get; set; }
        public float IndustrialPureFluxTotal { get; set; }
        public float MethaneChromaSum { get; set; }
        public float TemperatureSum { get; set; }
        public float PressureSum { get; set; }
        public float FluxSum { get; set; }
        public int CountSum { get; set; }
        public double SpanTime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Flag { get; set; }
        public DateTime UpdateTime { get; set; }

        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "FluxID", "Year", "Month", "Day", "Hour", "Flag" };
            }
        }
        public static IEnumerable<DataTable> List2Table(List<FluxRunModel> models)
        {
            var dt = BuildFluxRun();
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
            row["SubStationID"] = SubStationID;
            row["Location"] = Location;
            row["FluxName"] = FluxName;
            row["ConcentrationPort"] = ConcentrationPort;
            row["FluxPort"] = FluxPort;
            row["PressurePort"] = PressurePort;
            row["TemperaturePort"] = TemperaturePort;
            row["PressureFlag"] = PressureFlag;
            row["StandardatMosphere"] = StandardatMosphere;


            row["MethaneChromaMax"] = MethaneChromaMax;
            row["MethaneChromaMaxTime"] = MethaneChromaMaxTime;

            row["TemperatureMax"] = TemperatureMax;
            row["TemperatureMaxTime"] = TemperatureMaxTime;
            row["PressureMax"] = PressureMax;
            row["PressureMaxTime"] = PressureMaxTime;
            row["FluxMax"] = FluxMax;
            row["FluxMaxTime"] = FluxMaxTime;
            row["MethaneChromaMin"] = MethaneChromaMin;
            row["MethaneChromaMinTime"] = MethaneChromaMinTime;
            row["TemperatureMinTime"] = TemperatureMinTime;
            row["PressureMin"] = PressureMin;
            row["PressureMinTime"] = PressureMinTime;
            row["FluxMin"] = FluxMin;
            row["FluxMinTime"] = FluxMinTime;
            row["FluxTotal"] = FluxTotal;

            row["PureFluxTotal"] = PureFluxTotal;
            row["IndustrialFluxTotal"] = IndustrialFluxTotal;
            row["IndustrialPureFluxTotal"] = IndustrialPureFluxTotal;
            row["MethaneChromaSum"] = MethaneChromaSum;
            row["TemperatureSum"] = TemperatureSum;
            row["PressureSum"] = PressureSum;
            row["FluxSum"] = FluxSum;

            row["CountSum"] = CountSum;
            row["SpanTime"] = SpanTime;
            row["Year"] = Year;
            row["Month"] = Month;
            row["Day"] = Day;
            row["Hour"] = Hour;
            row["Flag"] = Flag;

            row["UpdateTime"] = UpdateTime;
            dt.Rows.Add(row);
        }
        public static DataTable BuildFluxRun()
        {
            var dt = new DataTable("FluxRun");
            dt.Columns.Add(new DataColumn("FluxID", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("FluxName", typeof(string)));
            dt.Columns.Add(new DataColumn("ConcentrationPort", typeof(int)));
            dt.Columns.Add(new DataColumn("MethaneChromaRealValue", typeof(int)));
            dt.Columns.Add(new DataColumn("FluxPort", typeof(int)));
            dt.Columns.Add(new DataColumn("PressurePort", typeof(int)));
            dt.Columns.Add(new DataColumn("TemperaturePort", typeof(int)));
            dt.Columns.Add(new DataColumn("PressureFlag", typeof(bool)));

            dt.Columns.Add(new DataColumn("StandardatMosphere", typeof(float)));
            dt.Columns.Add(new DataColumn("MethaneChromaMax", typeof(float)));
            dt.Columns.Add(new DataColumn("MethaneChromaMaxTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("TemperatureMax", typeof(float)));
            dt.Columns.Add(new DataColumn("TemperatureMaxTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("PressureMax", typeof(float)));
            dt.Columns.Add(new DataColumn("PressureMaxTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FluxMax", typeof(float)));
            dt.Columns.Add(new DataColumn("FluxMaxTime", typeof(DateTime)));


            dt.Columns.Add(new DataColumn("MethaneChromaMin", typeof(float)));
            dt.Columns.Add(new DataColumn("MethaneChromaMinTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("TemperatureMin", typeof(float)));
            dt.Columns.Add(new DataColumn("TemperatureMinTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("PressureMin", typeof(float)));
            dt.Columns.Add(new DataColumn("PressureMinTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FluxMin", typeof(float)));
            dt.Columns.Add(new DataColumn("FluxMinTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FluxTotal", typeof(float)));

            dt.Columns.Add(new DataColumn("PureFluxTotal", typeof(float)));
            dt.Columns.Add(new DataColumn("IndustrialFluxTotal", typeof(float)));
            dt.Columns.Add(new DataColumn("IndustrialPureFluxTotal", typeof(float)));
            dt.Columns.Add(new DataColumn("MethaneChromaSum", typeof(float)));
            dt.Columns.Add(new DataColumn("TemperatureSum", typeof(float)));
            dt.Columns.Add(new DataColumn("PressureSum", typeof(float)));
            dt.Columns.Add(new DataColumn("FluxSum", typeof(float)));
            dt.Columns.Add(new DataColumn("CountSum", typeof(int)));

            dt.Columns.Add(new DataColumn("SpanTime", typeof(int)));
            dt.Columns.Add(new DataColumn("Year", typeof(int)));
            dt.Columns.Add(new DataColumn("Month", typeof(int)));
            dt.Columns.Add(new DataColumn("Day", typeof(int)));

            dt.Columns.Add(new DataColumn("Hour", typeof(int)));
            dt.Columns.Add(new DataColumn("Flag", typeof(int)));
            dt.Columns.Add(new DataColumn("UpdateTime", typeof(DateTime)));
            dt.PrimaryKey = new[] { dt.Columns["FluxID"] };
            return dt;
        }
        public override string ToString()
        {
            return $"FluxId:{FluxID}\tSubstationId:{SubStationID}\t{Year}-{Month}-{Day} {Hour}";
        }
    }
}
