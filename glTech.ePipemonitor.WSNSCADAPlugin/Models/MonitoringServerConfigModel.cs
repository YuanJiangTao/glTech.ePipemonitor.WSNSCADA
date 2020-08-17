using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class MonitoringServerConfigModel
    {



        public List<SubStationModel> SubStationModels { get; } = new List<SubStationModel>();
        public void InitSubStation(List<SubStationModel> dasSubStationModels)
        {
            SubStationModels.AddRange(dasSubStationModels);
        }
        public int MonitoringServerID { get; set; }
        public string Configuration { get; set; }
        public int Protocol { get; set; }
        public int State { get; set; }
        public string Notes { get; set; }
        public int RMan { get; set; }
        public DateTime RDate { get; set; }
        public int LMan { get; set; }
        public DateTime LDate { get; set; }
    }
}
