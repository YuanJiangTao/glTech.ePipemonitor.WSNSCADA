using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class SubStationRunModel
    {
        public int SubStationID { get; set; }
        public string SubStationName { get; set; }
        public string Location { get; set; }
        public string SubStationStateValue { get; set; }
        public int SubStationState { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpanTime { get; set; }
        public int State { get; set; }

        public string Key
        {
            get { return $"{SubStationID}-{StartTime:G}"; }
        }

        public string Treatement { get; set; }
        public DateTime TreatmentTime { get; set; }
        public int Writer { get; set; }
        public static string[] PrimaryKeys
        {
            get
            {
                return new[] { "SubStationID", "StartTime" };
            }
        }
        private static string TableName()
        {
            return "SubStationRun";
        }
        public static IEnumerable<DataTable> List2Table(List<SubStationRunModel> models)
        {
            var dt = BuildSubstationRun();
            foreach (var model in models)
            {
                model.AddDataRow(dt);
            }
            dt.TableName = TableName();

            yield return dt;
        }

        public static DataTable BuildSubstationRun()
        {
            var dt = new DataTable("SubStationRun");
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("SubStationName", typeof(string)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationStateValue", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationState", typeof(int)));
            dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SpanTime", typeof(int)));
            dt.Columns.Add(new DataColumn("State", typeof(int)));
            dt.Columns.Add(new DataColumn("Treatement", typeof(string)));
            dt.Columns.Add(new DataColumn("TreatmentTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Writer", typeof(int)));
            return dt;
        }

        private void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["SubStationID"] = SubStationID;
            row["SubStationName"] = SubStationName;
            row["Location"] = Location;
            row["SubStationStateValue"] = SubStationStateValue;
            row["SubStationState"] = SubStationState;
            row["StartTime"] = StartTime;
            row["EndTime"] = EndTime;
            row["SpanTime"] = SpanTime;
            row["State"] = State;
            row["Treatement"] = Treatement;
            row["TreatmentTime"] = TreatmentTime;
            row["Writer"] = Writer;
            dt.Rows.Add(row);
        }

        public bool IsRequireNew(RealDataModel realDataModel)
        {
            // 如果大于5分钟, 那么写入一条数据.
            //if (realDataModel.RealDate.Subtract(_lastSaveTime).TotalMinutes >= 5)
            //{
            //    return true;
            //}

            // 如果值或状态变动, 并且时间大于10秒, 那么写入一条数据.
            if (SubStationState != realDataModel.RealState ||
                SubStationStateValue != realDataModel.RealValue)
            {
                //if (realDataModel.RealTime.Subtract(_lastSaveTime).TotalSeconds >= 10)
                {
                    return true;
                }
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
        public static void UpdateSubStationRun(ref SubStationRunModel subStationRunModel,
         List<SubStationRunModel> subStationRunModels,
         RealDataModel realDataModel, SubStationModel subStationModel)
        {
            if (subStationRunModel == null)
            {
                subStationRunModel = NewSubStationRunModel(realDataModel, subStationModel);
                subStationRunModel.State = 0;
                subStationRunModels.Add(subStationRunModel.DeepClone());
            }
            else
            {
                subStationRunModel.EndTime = realDataModel.RealDate;

                if (subStationRunModel.IsTimeToSave(realDataModel))
                {
                    var newM = subStationRunModel.DeepClone();
                    SubStationRunModel existM;
                    if ((existM = subStationRunModels.FirstOrDefault(o => o.Key == newM.Key)) != null)
                    {
                        existM.EndTime = newM.EndTime;
                        existM.SubStationState = newM.SubStationState;
                        existM.SubStationStateValue = newM.SubStationStateValue;
                        existM.SpanTime = (int)newM.EndTime.Subtract(existM.StartTime).TotalSeconds;
                    }
                    else
                        subStationRunModels.Add(subStationRunModel.DeepClone());
                }

                if (subStationRunModel.IsRequireNew(realDataModel))
                {
                    // 需要新增加一条数据.
                    subStationRunModel = NewSubStationRunModel(realDataModel, subStationModel);
                    subStationRunModel.State = 0;
                    subStationRunModels.Add(subStationRunModel.DeepClone());
                }
            }
        }

        private static SubStationRunModel NewSubStationRunModel(RealDataModel realDataModel, SubStationModel subStationModel)
        {
            var model = new SubStationRunModel();
            model.SubStationID = realDataModel.SubStationID;
            model.SubStationName = subStationModel.SubStationName;
            model.Location = subStationModel.Location;
            model.SubStationState = realDataModel.RealState;
            model.SubStationStateValue = realDataModel.RealValue;
            model.StartTime = realDataModel.RealDate;
            model.EndTime = realDataModel.RealDate;
            model.SpanTime = 0;
            model.State = 0;
            model.Treatement = "";
            model.TreatmentTime = DateTime.Now;
            model.Writer = 0;
            return model;
        }

    }
}
