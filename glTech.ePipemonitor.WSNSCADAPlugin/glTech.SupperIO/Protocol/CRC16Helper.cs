using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol
{
    public class CRC16Helper
    {
        public static byte[] GetCRC16(byte[] data)
        {
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;

                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
                byte lo = (byte)(crc & 0x00FF);         //低位置

                //return new byte[] { hi, lo };
                return new byte[] { lo, hi };
            }
            return new byte[] { 0, 0 };
        }

        public static bool ValidCRC16(byte[] data)
        {
            var result = false;
            try
            {
                if (data.Length <= 2)
                    return result;
                var body = data.Take(data.Length - 2);
                var crc_old = data.Skip(data.Length - 2).Take(2).ToArray();
                var crc_new = GetCRC16(body.ToArray());
                if (crc_new[0] == crc_old[0] && crc_new[1] == crc_old[1])
                {
                    result = true;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}
