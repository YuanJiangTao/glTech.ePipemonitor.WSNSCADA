using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PluginContract.Helper
{
    public static class ByteHelper
    {
        /// <summary>
        /// 设置Int16指定位的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">位序号（0开始计数）</param>
        /// <param name="value">设置值：0：false；1：true</param>
        /// <returns></returns>
        public static Int16 SetBitValue(Int16 data, int index, bool value)
        {
            BitArray bitArray = new BitArray(GetBytes(data));
            bitArray[index] = value;

            byte[] byteByBitArray = new byte[2];
            bitArray.CopyTo(byteByBitArray, 0);
            return ToInt16(byteByBitArray, 0);
        }

        /// <summary>
        /// 设置Int16指定位,指定长度的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex">位起始序号（0开始计数）</param>
        /// <param name="length">位长度</param>
        /// <param name="value">设置值</param>
        /// <returns>返回变更后的值</returns>
        public static Int16 SetBitValue(Int16 data, int startIndex, int length, Int16 value)
        {
            BitArray bitDataArray = new BitArray(GetBytes(data));
            BitArray valueBitArray = new BitArray(GetBytes(value));
            if (startIndex >= 0 && length > 0 && startIndex + length <= 16)
            {
                for (int i = 0; i < length; i++)
                {
                    bitDataArray[i + startIndex] = valueBitArray[i];
                }
            }

            byte[] byteByBitArray = new byte[2];
            bitDataArray.CopyTo(byteByBitArray, 0);
            return ToInt16(byteByBitArray, 0);

        }
        /// <summary>
        /// 设置Byte指定位的值
        /// </summary>
        /// <param name="data">原始值</param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte SetBitValue(byte data, int index, bool value)
        {
            BitArray bitArray = new BitArray(new byte[] { data });
            byte[] byteByBitArray = new byte[1];
            bitArray[index] = value;
            bitArray.CopyTo(byteByBitArray, 0);
            return byteByBitArray[0];
        }
        /// <summary>
        /// 设置Byte指定位,指定长度的值
        /// </summary>
        /// <param name="data">原始值</param>
        /// <param name="startIndex">位运算开始位置，0~7</param>
        /// <param name="length">位运算长度，1~8，且startIndex+length小于等于8 </param>
        /// <param name="value">设置值</param>
        /// <returns></returns>
        public static byte SetBitValue(byte data, int startIndex, int length, byte value)
        {
            BitArray bitDataArray = new BitArray(new byte[] { data });
            BitArray valueBitArray = new BitArray(new byte[] { value });
            if (startIndex >= 0 && length > 0 && startIndex + length <= 8)
            {
                for (int i = 0; i < length; i++)
                {
                    bitDataArray[i + startIndex] = valueBitArray[i];
                }
            }

            byte[] byteByBitArray = new byte[1];
            bitDataArray.CopyTo(byteByBitArray, 0);
            return byteByBitArray[0];
        }
        /// <summary>
        /// 返回Int16指定位的值(从低位算起)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startBitIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Int16 GetBitValue(Int16 value, int startBitIndex, int length)
        {
            var mast = (value >> startBitIndex) & ((Int16)Math.Pow(2, length) - 1);
            return (Int16)mast;
        }
        public static byte GetBitValue(byte value, int startBitIndex, int length)
        {
            var mast = (value >> startBitIndex) & ((Int16)Math.Pow(2, length) - 1);
            return (byte)mast;
        }
        public static int ToInt32(byte[] data, int startIndex, int length)
        {
            length = length > 4 ? 4 : length;
            var buffer = new byte[4];
            Buffer.BlockCopy(data, startIndex, buffer, 0, length);
            return BitConverter.ToInt32(buffer, 0);
        }
        public static int ToInt32(byte[] data, int startIndex, int length, bool bigEndian)
        {
            length = length > 4 ? 4 : length;
            var buffer = new byte[4];
            Buffer.BlockCopy(data, startIndex, buffer, 0, length);
            if (bigEndian)
            {
                return BitConverter.ToInt32(buffer.Reverse().ToArray(), 0);
            }
            else
            {
                return BitConverter.ToInt32(buffer, 0);
            }
        }

        public static string IntToIp(long ipInt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ipInt & 0xFF).Append(".");
            sb.Append((ipInt >> 8) & 0xFF).Append(".");
            sb.Append((ipInt >> 16) & 0xFF).Append(".");
            sb.Append((ipInt >> 24) & 0xFF);
            return sb.ToString();
        }

        /// <summary>
        /// 将整型转为指定长度字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value, int length)
        {
            var buffer = new byte[length];
            var size = Math.Min(length, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, buffer, 0, size);
            return buffer;
        }
        public static byte[] GetBytes(int value, bool bigEndian = false)
        {
            if (bigEndian)
            {
                return BitConverter.GetBytes(value).Reverse().ToArray();
            }
            else
            {
                return BitConverter.GetBytes(value);
            }
        }
        /// <summary>
        /// 将Int16 转byte数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bigEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(short value, bool bigEndian = false)
        {
            if (bigEndian)
            {
                return BitConverter.GetBytes(value).Reverse().ToArray();
            }
            else
            {
                return BitConverter.GetBytes(value);
            }
        }
        public static byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 将整型转为指定长度的字节数组并直接拷贝到指定数据区
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        public static void CopyInt32(int value, int length, byte[] data, int startIndex)
        {
            var size = Math.Min(length, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, startIndex, size);
        }
        public static short ToInt16(byte[] data, int startIndex, int length = 2, bool bigEndian = false)
        {
            length = length > 2 ? 2 : length;
            var buffer = new byte[2];
            Buffer.BlockCopy(data, startIndex, buffer, 0, length);
            if (bigEndian)
            {
                buffer = buffer.Reverse().ToArray();
            }
            return BitConverter.ToInt16(buffer, 0);

        }
        /// <summary>
        /// 将2字节byte数组转换成UInt16
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="bigEndian"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16(byte[] data, int startIndex, bool bigEndian = false)
        {
            if (bigEndian)
            {
                var temp = new byte[2];
                Buffer.BlockCopy(data, startIndex, temp, 0, 2);
                return BitConverter.ToUInt16(temp.Reverse().ToArray(), 0);
            }
            else
            {
                return BitConverter.ToUInt16(data, startIndex);
            }
        }
        /// <summary>
        /// 将4字节byte数组转换成UInt32
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="bigEndian"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32(byte[] data, int startIndex, bool bigEndian = false)
        {

            if (bigEndian)
            {
                var temp = new byte[4];
                Buffer.BlockCopy(data, startIndex, temp, 0, 4);
                return BitConverter.ToUInt32(temp.Reverse().ToArray(), 0);
            }
            else
            {
                return BitConverter.ToUInt32(data, startIndex);
            }
        }
        public static Single ToSingle(byte[] data, int startIndex, int length = 4, bool bigEndian = false)
        {
            if (data == null || data.Length == 0)
            {
                return Single.MinValue;
            }
            length = length > 4 ? 4 : length;
            var buffer = new byte[4];
            Buffer.BlockCopy(data, startIndex, buffer, 0, length);
            if (bigEndian)
            {
                buffer = buffer.Reverse().ToArray();
            }
            return BitConverter.ToSingle(buffer, 0);

        }
        //
        public static void CopyFloat(float value, byte[] data, int startIndex)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, startIndex, 4);
        }

        private static Regex _addressRegex = new Regex(@"[0-9A-Za-z]{8}");
        public static string ToSetupAddress(byte[] data, int startIndex, int length)
        {
            var buffer = new byte[length];
            Buffer.BlockCopy(data, startIndex, buffer, 0, length);

            var result = string.Join("", buffer.Select(o => o.ToString("X1")));
            if (result.Length == 8)
                return result;

            var setupAddress = Encoding.ASCII.GetString(buffer.Select(o =>
            {
                if (o == 0) { return (byte)48; }
                return o;
            }).ToArray());

            if (!_addressRegex.IsMatch(setupAddress))
            {
                return "";
            }
            return setupAddress;
        }

        public static string ToString(byte[] data)
        {
            return ToSetupAddress(data, 0, data.Length);
        }

        /// <summary>
        /// 整型值转16进制字符串
        /// </summary>
        /// <param name="value">整型值</param>
        /// <param name="size">16进制字符串长度（长度不够，左侧补0）</param>
        /// <returns></returns>
        public static string Long2HexString(long value, int size)
        {
            return value.ToString("X").PadLeft(size, '0');
        }
        /// <summary>
        /// 将16进制字符串转为文本字符串
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string HexString2Text(string hexString)
        {
            return Bytes2Text(String2Bytes(hexString));
            //var ret = string.Empty;
            //try
            //{
            //    string[] hexValuesSplit = hexString.Split(' ');
            //    foreach (String hex in hexValuesSplit)
            //    {
            //        int value = Convert.ToInt32(hex, 16);
            //        string stringValue = Char.ConvertFromUtf32(value);
            //        char charValue = (char)value;
            //        ret += charValue;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogError4Exception(ex);
            //}


            //return ret;
        }
        /// <summary>
        /// 将Byte数组转为文本字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Bytes2Text(byte[] data)
        {
            var ret = string.Empty;
            try
            {
                ret = System.Text.Encoding.Default.GetString(data);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                //                Logger.LogError4Exception(ex);
            }


            return ret;
        }

        /// <summary>
        /// 将字符串文本转换为16进制字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Text2HexString(string text)
        {
            return ByteHelper.ToHexString(System.Text.Encoding.Default.GetBytes(text));
        }

        public static byte[] GetBytes(string text)
        {
            return System.Text.Encoding.Default.GetBytes(text);
        }
        /// <summary>
        /// 将Byte数组转为16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] data)
        {
            return ToHexString(data, 0, data.Length);
        }

        /// <summary>
        /// 将Byte数组转为16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="withPrefix">是否带前缀0x</param>
        /// <returns></returns>
        public static string ToHexString(byte[] data, bool withPrefix)
        {
            return ToHexString(data, 0, data.Length, withPrefix);
        }
        /// <summary>
        /// 将Byte数组转为16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] data, int startIndex, int length)
        {
            return ToHexString(data, startIndex, length, false);
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string ToHexString(byte[] data, int startIndex, int length, bool withPrefix)
        {
            var dataString = string.Empty;
            if (data != null)
            {
                for (int i = startIndex; i < startIndex + length; i++)
                {
                    if (withPrefix)
                    {
                        dataString += string.Format("0x{0:X02} ", data[i]);
                    }
                    else
                    {
                        dataString += string.Format("{0:X02} ", data[i]);
                    }

                }
            }
            return dataString;
        }
        /// <summary>
        /// 将16进制字符串转换为byte数组
        /// 字符串以空格分割
        /// 字符串样本：FC FC FC FE 00 07 86 20 01 02 03 04 05 85 78
        /// 字符串样本：0xFC 0xFC 0xFC 0xFE 0x00 0x07 0x86 0x20 0x01 0x02 0x03 0x04 0x05 0x85 0x78
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] String2Bytes(string hexString)
        {

            var split = hexString.Replace("\r\n", " ").Trim().Split(' ');

            var list = new List<byte>();
            try
            {
                foreach (var data in split)
                {
                    if (data.Length > 2)
                    {
                        var datas = data;
                        //没有间隔区分
                        while (datas.Length > 0)
                        {
                            try
                            {
                                list.Add(Convert.ToByte(datas.Substring(0, 2), 16));
                            }
#pragma warning disable CS0168 // Variable is declared but never used
                            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                            {
                                //                                Logger.LogError4Exception(ex);
                            }
                            datas = datas.Substring(2, datas.Length - 2);
                            if (datas.Length == 1)
                            {
                                datas = "0" + datas;
                            }
                        }

                    }
                    else
                    {
                        var value = data;
                        if (data.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        {
                            value = data.Remove(0, 2);
                        }
                        list.Add(Convert.ToByte(value, 16));
                    }

                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return list.ToArray();
        }



    }
    #region--CRC32--
    public static class Crc32
    {
        private static readonly ushort[] Crc32Table = new ushort[256]{
                    0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
                    0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
                    0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6,
                    0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
                    0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485,
                    0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
                    0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4,
                    0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
                    0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823,
                    0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
                    0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12,
                    0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
                    0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41,
                    0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
                    0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70,
                    0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
                    0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f,
                    0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
                    0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e,
                    0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
                    0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d,
                    0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
                    0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c,
                    0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
                    0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab,
                    0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
                    0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
                    0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
                    0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9,
                    0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
                    0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8,
                    0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
                    };
        /// <summary>
        /// CRC校验公式
        /// </summary>
        /// <param name="crc">CRC</param>
        /// <param name="cp">发送的数据序列</param>
        /// <returns>新CRC</returns>
        private static ushort XCrc32(ushort crc, Int16 cp)
        {
            ushort t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            t1 = (ushort)(crc >> 8);
            t2 = (ushort)(t1 & 0xff);
            t3 = (ushort)(cp & 0xff);
            t4 = (ushort)(crc << 8);
            t5 = (ushort)(t2 ^ t3);
            t6 = (ushort)(Crc32Table[t5] ^ t4);
            return t6;
        }
        public static byte[] GetCrc32(byte[] bufout, int startIndex, int count)
        {
            if (startIndex == 0)
            {
                return GetCrc32(bufout, count);
            }
            else
            {
                var buffer = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    buffer[i] = bufout[i + startIndex];
                }
                return GetCrc32(buffer, buffer.Length);
            }

        }
        public static byte[] GetCrc32(byte[] bufout)
        {
            return GetCrc32(bufout, bufout.Length);
        }
        public static byte[] GetCrc32(byte[] bufout, int count)
        {
            ushort crc16 = 0;
            for (var i = 0; i < (count); i++)
            {
                //System.Console.WriteLine(i);
                crc16 = XCrc32(crc16, bufout[i]);
            }


            byte[] rb = new byte[2];
            byte crcHi = (byte)(crc16 >> 8);
            byte crcLo = (byte)(crc16 & 0xff);

            rb[1] = crcLo;
            rb[0] = crcHi;

            return rb;

        }

    }
    #endregion-CRC32-
}
