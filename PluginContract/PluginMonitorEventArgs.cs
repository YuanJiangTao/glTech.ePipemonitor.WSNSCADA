using System;

namespace PluginContract
{
    public class PluginMonitorEventArgs : EventArgs
    {
        public string MonitorId { get; private set; }
        public string[] Fields { get; private set; }

        public PluginMonitorEventArgs(string monitorId, string[] fields)
        {
            this.MonitorId = monitorId;
            this.Fields = fields;
        }
    }
}