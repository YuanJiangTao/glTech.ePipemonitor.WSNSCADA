using System;
using System.Collections.Generic;
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
    }
}
