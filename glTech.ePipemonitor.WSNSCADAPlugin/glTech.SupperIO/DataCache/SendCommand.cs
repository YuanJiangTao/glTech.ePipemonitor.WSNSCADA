using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.DataCache
{
    public class SendCommand : ISendCommand
    {
        public int Id { get; private set; }
        /// <summary>
        /// 设备地址（分站设备编号与地址一致）
        /// </summary>
        public int Address { get; private set; }
        /// <summary>
        /// 命令字
        /// </summary>
        public byte Cmd { get; private set; }
        /// <summary>
        /// 命令
        /// </summary>
        public byte[] Data { get; private set; }
        public string DataString
        {
            get { return ByteHelper.ToHexString(Data); }
        }
        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name { get; private set; }

        public DateTime SendTime { get; }

        public bool IsReceiveResponse { get; set; } = false;

        /// <summary>
        /// 设备命令
        /// </summary>
        /// <param name="keyName">命令名称</param>
        /// <param name="cmdbytes">命令字节数组</param>
        public SendCommand(int id, int address, byte cmd, string keyName, byte[] cmdbytes)
        {
            Id = id;
            Address = address;
            Cmd = cmd;
            SendTime = DateTime.Now;
            Name = keyName;
            Data = cmdbytes;
        }
    }
}
