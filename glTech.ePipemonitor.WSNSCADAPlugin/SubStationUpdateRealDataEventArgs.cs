using System;
using System.Linq;
using System.Collections;
using System.Windows.Documents;
using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using System.Collections.Generic;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class SubStationUpdateRealDataEventArgs : EventArgs
    {
        public List<RealDataModel> RealDataModels { get; set; }

        public List<AnalogRunModel> AnalogRunModels { get; set; }
        public List<AnalogStatisticModel> AnalogStatisticModels { get; set; }
        public List<Alarm_TodayModel> Alarm_TodayModels { get; set; }

        public List<AnalogAlarmModel> AnalogAlarmModels { get; set; }

        public List<FluxRealDataModel> FluxRealDataModels { get; set; }
        public List<FluxRunModel> FluxRunModels { get; set; }
        public DateTime UpdateTime { get; }

        public int DasId { get; }
        public SubStationUpdateRealDataEventArgs(
         int dasId,
         List<RealDataModel> realdataModels,
         List<AnalogRunModel> analogRunModels,
         List<AnalogStatisticModel> analogStatisticModels,
         List<Alarm_TodayModel> alarmTodayModels,
         List<AnalogAlarmModel> analogAlarmModels,
         List<FluxRealDataModel> fluxRealDataModels,
         List<FluxRunModel> fluxRunModels)
        {
            DasId = dasId;
            RealDataModels = realdataModels;
            Alarm_TodayModels = alarmTodayModels;
            AnalogRunModels = analogRunModels;
            AnalogStatisticModels = analogStatisticModels;
            AnalogAlarmModels = analogAlarmModels;
            FluxRealDataModels = fluxRealDataModels;
            FluxRunModels = fluxRunModels;
            UpdateTime = DateTime.Now;
        }
    }
}