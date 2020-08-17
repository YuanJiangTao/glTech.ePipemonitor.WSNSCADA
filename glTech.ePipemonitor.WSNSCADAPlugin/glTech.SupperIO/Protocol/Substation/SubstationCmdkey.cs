using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation
{
    sealed class SubstationCmdkey
    {
        /// <summary>
        /// 报文组 1 主站查询实时数据
        /// </summary>
        public const byte QueryRealData = 0x03;

        /// <summary>
        /// 从机错误响应功能码
        /// </summary>
        public const byte ErrorResponse = 0x83;

        internal static string GetSubstationCmdName(byte cmd)
        {
            string cmdStr;
            switch (cmd)
            {
                case QueryRealData:
                    cmdStr = "查询传感器实时数据";
                    break;
                case ErrorResponse:
                    cmdStr = "从站响应查询实时数据错误";
                    break;
                default:
                    cmdStr = "未知的命令";
                    break;
            }
            return cmdStr;
        }
    }
}
