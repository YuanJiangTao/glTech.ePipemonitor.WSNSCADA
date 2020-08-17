using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.DataCache;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation
{
    class SubstationProtocol
    {
        public static ISendCommand GetRealDataCommand(int substationId)
        {
            return GetSendCommand(substationId, 0, 0x0f);
        }
        public static ISendCommand GetSendCommand(int substationId, UInt16 startIndex, UInt16 length)
        {
            var data = new List<byte>();
            data.Add((byte)substationId);
            data.Add(SubstationCmdkey.QueryRealData);
            data.AddRange(BitConverter.GetBytes(startIndex).Reverse());
            data.AddRange(BitConverter.GetBytes(length).Reverse());

            return new SendCommand(substationId, substationId, SubstationCmdkey.QueryRealData,
                SubstationCmdkey.GetSubstationCmdName(SubstationCmdkey.QueryRealData), GetCrcData(data.ToArray()));
        }
        public static byte[] GetCrcData(byte[] data)
        {
            var result = new List<byte>();
            result.AddRange(data);
            var crc = CRC16Helper.GetCRC16(data);
            result.AddRange(crc);
            return result.ToArray();
        }
    }
}
