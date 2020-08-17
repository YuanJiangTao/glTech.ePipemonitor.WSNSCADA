using glTech.ePipemonitor.WSNSCADAPlugin.Repositories;
using PluginContract;
using PluginContract.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    static class DasConfig
    {
        private static WSNSCADADasPlugin _plugin;
        public static void Init(WSNSCADADasPlugin dasPlugin)
        {
            _plugin = dasPlugin;
        }
        /// <summary>
        /// 命令接收超时时间
        /// </summary>
        public static int RecvTimeout
        {
            get
            {
                if (int.TryParse(_plugin[KvSettingKeyConst.TIMEOUT].ToString(), out int value))
                {
                    if (value < 1000)
                        value *= 1000;
                    return value;
                }
                return 1500;
            }
        }
        /// <summary>
        /// 轮训周期
        /// </summary>
        public static int DasGatherInterval
        {
            get
            {
                if (int.TryParse(_plugin[KvSettingKeyConst.DAS_GATHER_INTERVAL].ToString(), out var value))
                {
                    return value;
                }
                return 5;
            }
        }
        /// <summary>
        /// 网络断线屏蔽次数
        /// </summary>
        public static int NetworkOffCount
        {
            get
            {
                if (int.TryParse(_plugin[KvSettingKeyConst.NETWORK_OFF_COUNT].ToString(), out var value))
                {
                    return value;
                }
                return 1;
            }
        }
        /// <summary>
        /// 在主界面是否打印报文日志.
        /// </summary>
        public static bool ShowDetailLogs
        {
            get
            {
                if (bool.TryParse(_plugin[KvSettingKeyConst.SHOW_DETAILS_LOG].ToString(), out bool value))
                    return value;
                return false;
            }
        }

        /// <summary>
        /// 是否需要开启补发命令功能.
        /// </summary>
        public static bool NeedResend
        {
            get
            {
                if (bool.TryParse(_plugin[KvSettingKeyConst.SENDCOMMAND_AGAIN].ToString(), out bool value))
                    return value;

                return false;
            }
        }
        /// <summary>
        /// 传感器断线次数屏蔽
        /// </summary>
        public static int SensorTimeoutCount 
        {
            get
            {
                if (int.TryParse(_plugin[KvSettingKeyConst.ANALOG_OFF_COUNT].ToString(), out var value))
                    return value;

                return 3;
            }
        }


        public static void InitRepo(IDatabaseConfig hostConfig)
        {
            Repo = new SqlRepo(hostConfig);
        }

        public static SqlRepo Repo { get; set; }
    }
}
