using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    interface IDas
    {
        int MonitoringServerID { get; }
        bool IsGood { get; }
        void Start();
        void Stop();
        PluginMonitor Monitor { get; }
        bool DeleteSubstation(int substationId);
        bool ReloadSubstation(SubStationModel subStationModel);
        bool HasSubstation(int substationId);

        event EventHandler<SubStationUpdateRealDataEventArgs> DasUpdateRealData;
    }
}
