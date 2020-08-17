using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class KvSettingKeyConst
    {
        public const string TIMEOUT = "命令等待超时时间";

        public const string DAS_GATHER_INTERVAL = "数据采集周期";

        public const string NETWORK_OFF_COUNT = "网络断线次数";

        public const string SHOW_DETAILS_LOG = "显示详细日志";

        public const string SENDCOMMAND_AGAIN = "启用命令补发";

        public const string ANALOG_OFF_COUNT = "传感器断线屏蔽次数";
    }
}
