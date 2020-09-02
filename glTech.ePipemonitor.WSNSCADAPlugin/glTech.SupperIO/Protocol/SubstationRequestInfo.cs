using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Filter;
using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol
{
    public class SubstationRequestInfo : RequestInfo<byte[], byte[]>
    {
        public SubstationRequestInfo(byte[] data)
        {
            try
            {
                Data = data;
                SubstationId = data[0];
                CmdKey = data[1];
                BodyLength = data[2];
                var body = new byte[BodyLength];
                Buffer.BlockCopy(data, 3, body, 0, body.Length);
                Initialize("Key", body);
                var crc1 = data[^2];
                var crc2 = data[^1];
                var crc = CRC16Helper.GetCRC16(data.Take(data.Length - 2).ToArray());
                CrcOk = crc1 == crc[0] && crc2 == crc[1];
            }
            catch (Exception)
            {

            }
        }
        public byte[] Data { get; }
        public int SubstationId { get; private set; }
        public byte CmdKey { get; private set; }
        public byte BodyLength { get; private set; }
        public bool CrcOk { get; private set; }

        public string DataString
        {
            get
            {
                var data = new StringBuilder();
                for (int i = 0; i < Data.Length; i++)
                {
                    data.Append(string.Format("{0:X2} ", Data[i]));
                }
                return data.ToString();
            }
        }
    }
}
