using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADA
{
    class MessageToken
    {
        public const string MAINWINDOWTITLE = "传感器数据采集软件";
        public const string WSNSCADA = nameof(WSNSCADA);

        public const string PLUGINS_FOLDER = "Plugins";
        public const string PLUGIN_TOKEN = "Plugin";

        public const string CONFIG_FOLDER = "Config";

        public const string THEMEXML = "themes.config";
        public const string DATABASEXML = "database.config";
        public const string STANDBY_DATABASEXML = "standby_database.config";
        public const string SETTINGCONFIG_FOLDER = "setting.config";
        public const string GENERAL_SETTING = "general_settting.config";

        public const string SAVELOGDAYS = "保留日志天数";
    }
}
