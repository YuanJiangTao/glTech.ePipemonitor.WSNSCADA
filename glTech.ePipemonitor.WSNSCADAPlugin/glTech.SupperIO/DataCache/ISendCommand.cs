using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    interface ISendCommand
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 设备地址（分站设备编号与地址一致）
        /// </summary>
        int Address { get; }
        /// <summary>
        /// 命令字
        /// </summary>
        byte Cmd { get; }
        /// <summary>
        /// 命令字节
        /// </summary>
        byte[] Data { get; }
        /// <summary>
        /// 命令发送时间
        /// </summary>
        DateTime SendTime { get; }
        /// <summary>
        /// 命令描述
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 报文字符串描述
        /// </summary>
        string DataString { get; }

        /// <summary>
        /// 是否得到了回应
        /// </summary>
        bool IsReceiveResponse { get; set; }
    }
}
