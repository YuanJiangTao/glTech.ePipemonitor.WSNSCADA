using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class DeviceFaultRunModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public int PointType { get; set; }
        public string Location { get; set; }
        public int PointState { get; set; }
        public string PointStateName { get; set; }
        public int FeedState { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpanTime { get; set; }
        public int State { get; set; }
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

        public static IEnumerable<DataTable> List2Table(List<DeviceFaultRunModel> deviceFaultRunModels)
        {
            var dt = BuildDeviceFaultRunTable();
            foreach (var model in deviceFaultRunModels)
            {
                model.AddDataRow(dt);
            }
            dt.TableName = TableName();
            yield return dt;
        }
        public static string TableName()
        {
            return "DeviceFaultRun";
        }

        /// <summary>
        /// 建立数据内存表结构.
        /// </summary>
        /// <returns>返回datatable</returns>
        public static DataTable BuildDeviceFaultRunTable()
        {
            var dt = new DataTable("DeviceFaultRun");
            dt.Columns.Add(new DataColumn("PointID", typeof(string)));
            dt.Columns.Add(new DataColumn("PointName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubStationID", typeof(int)));
            dt.Columns.Add(new DataColumn("PortNO", typeof(int)));
            dt.Columns.Add(new DataColumn("PointType", typeof(int)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("PointState", typeof(int)));
            dt.Columns.Add(new DataColumn("PointStateName", typeof(string)));
            dt.Columns.Add(new DataColumn("FeedState", typeof(int)));
            dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SpanTime", typeof(int)));
            dt.Columns.Add(new DataColumn("State", typeof(int)));
            dt.Columns.Add(new DataColumn("Treatment", typeof(string)));
            dt.Columns.Add(new DataColumn("TreatmentTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Writer", typeof(int)));
            return dt;
        }
        public void AddDataRow(DataTable dt)
        {
            var row = dt.NewRow();
            row["PointID"] = PointID;
            row["PointName"] = PointName;
            row["SubStationID"] = SubStationID;
            row["PortNO"] = PortNO;
            row["PointType"] = PointType;

            row["Location"] = Location;
            row["PointState"] = PointState;
            row["PointStateName"] = PointStateName;
            row["FeedState"] = FeedState;
            row["StartTime"] = StartTime;
            row["EndTime"] = EndTime;
            row["SpanTime"] = SpanTime;
            row["State"] = State;
            row["Treatment"] = Treatment;
            row["TreatmentTime"] = DateTime.Now;
            row["Writer"] = Writer;
            dt.Rows.Add(row);
        }

        public bool IsRequireNew(RealDataModel realDataModel)
        {
            // 如果状态变动, 那么写入一条数据.
            if (PointState != realDataModel.RealState)
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

        public static void UpdateDeviceFaultRun(ref DeviceFaultRunModel deviceFaultRunModel,
            List<DeviceFaultRunModel> deviceFaultRunModels,
            RealDataModel realDataModel,
            AnalogPointModel analogPointModel, Func<PointState, bool> isAlarmState)
        {
            if (deviceFaultRunModel == null)
            {
                NewAlarmAndSaveDatabase(out deviceFaultRunModel, deviceFaultRunModels, realDataModel, analogPointModel, isAlarmState);
            }
            else
            {
                deviceFaultRunModel.EndTime = realDataModel.RealDate;

                if (deviceFaultRunModel.IsRequireNew(realDataModel))
                {
                    // 有新的记录, 把老记录状态置1
                    deviceFaultRunModel.State = 1;
                }
                if (!deviceFaultRunModel.IsRequireNew(realDataModel))
                {
                    deviceFaultRunModel.PointState = realDataModel.RealState;
                }

                if (isAlarmState((PointState)deviceFaultRunModel.PointState) &&
                    (deviceFaultRunModel.IsTimeToSave(realDataModel) || deviceFaultRunModel.IsRequireNew(realDataModel)))
                {
                    // 报警情况下, 写入数据库.
                    var newM = deviceFaultRunModel.DeepClone();
                    DeviceFaultRunModel existM;
                    if ((existM = deviceFaultRunModels.FirstOrDefault(o => o.PointID == newM.PointID && o.StartTime == newM.StartTime && o.PointState == newM.PointState)) != null)
                    {
                        existM.State = newM.State;
                        existM.PointState = newM.PointState;
                        existM.PointStateName = Alarm_TodayModel.GetAlarmStateName((PointState)newM.PointState);
                        existM.EndTime = newM.EndTime;
                        existM.SpanTime = (int)newM.EndTime.Subtract(newM.StartTime).TotalSeconds;
                    }
                    else
                        deviceFaultRunModels.Add(deviceFaultRunModel.DeepClone());
                }

                if (deviceFaultRunModel.IsRequireNew(realDataModel))
                {
                    // 有新记录.
                    NewAlarmAndSaveDatabase(out deviceFaultRunModel, deviceFaultRunModels, realDataModel, analogPointModel, isAlarmState);
                }
            }
        }

        private static void NewAlarmAndSaveDatabase(out DeviceFaultRunModel deviceFaultRunModel,
            List<DeviceFaultRunModel> deviceFaultRunModels,
            RealDataModel realDataModel, 
            AnalogPointModel analogPointModel, 
            Func<PointState, bool> isAlarmState)
        {
            deviceFaultRunModel = NewDeviceFaultRunModel(realDataModel, analogPointModel);

            var newStartTime = deviceFaultRunModel.StartTime;
            var valueState = deviceFaultRunModel.PointState;
            var existSameStartTime = deviceFaultRunModels.Find(o => newStartTime.Subtract(o.StartTime).TotalSeconds < 1
                && o.PointState == valueState);
            if (existSameStartTime != null)
            {
                // 只有当开始时间和结束时间不一致的时候, 排除初始化出现主键冲突问题.
                deviceFaultRunModels.Remove(existSameStartTime);

                LogD.Warn($"分站{deviceFaultRunModel.PointID} 状态{existSameStartTime.PointState}出现一秒内多次状态报警状态改变, 会发生主键冲突.");
            }

            if (isAlarmState((PointState)deviceFaultRunModel.PointState))
            {
                deviceFaultRunModels.Add(deviceFaultRunModel.DeepClone()); // 直接写入数据库
            }
        }
        private static DeviceFaultRunModel NewDeviceFaultRunModel(RealDataModel realDataModel,AnalogPointModel analogPointModel)
        {
            var model = new DeviceFaultRunModel();
            model.PointID = analogPointModel.PointID;
            model.PointName = analogPointModel.PointName;
            model.SubStationID = analogPointModel.SubStationID;
            model.PortNO = analogPointModel.PortNO;
            model.PointType = (int)Models.PointType.Analog;
            model.Location = analogPointModel.Location;
            model.PointState = realDataModel.RealState;
            model.PointStateName = Alarm_TodayModel.GetAlarmStateName((PointState)realDataModel.RealState);
            model.FeedState = realDataModel.FeedState;
            model.StartTime = realDataModel.RealDate;
            model.EndTime = realDataModel.RealDate;
            model.SpanTime = 0;
            model.State = 0;
            return model;
        }
    }
}
