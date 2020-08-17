using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    [Serializable]
   public class PluginKvSg
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    [Serializable]
    public class PluginKvSgs
    {
        public PluginKvSgs()
        {
            PluginKvSgList = new List<PluginKvSg>();
        }
        public List<PluginKvSg> PluginKvSgList { get; set; }
    }
}
