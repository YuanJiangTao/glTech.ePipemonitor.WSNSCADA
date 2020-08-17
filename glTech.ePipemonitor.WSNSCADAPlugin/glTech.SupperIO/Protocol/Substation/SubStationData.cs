using PluginContract.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Substation
{
    class SubStationData
    {

        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool IsValid { get; private set; } = true;
        public SubStationData(int subStationID, byte[] body)
        {
            try
            {
                SubstationId = subStationID;
                if (body == null)
                {
                    IsValid = false;
                    return;
                }
                SensorRealDataInfos = new List<SensorRealDataInfo>();
                for (var index = 0; index < body.Length / 6; index++)
                {
                    var state = ByteHelper.ToInt16(body.Skip(index * 6).ToArray(), 0);
                    var value = ByteHelper.ToSingle(body.Skip(index * 6 + 2).Reverse().ToArray(), 0);
                    var realData = new SensorRealDataInfo()
                    {
                        Value = value,
                        ValueState = state,
                    };
                    if (index == 0)
                    {
                        realData.EquipCodes.AddRange(new string[] { "020001", "020007", "020008", "020014", "029903", "029904" });
                    }
                    else if (index == 1)
                    {
                        realData.EquipCodes.AddRange(new string[] { "020003", "020010" });
                    }
                    else if (index == 2)
                    {
                        realData.EquipCodes.AddRange(new string[] { "020002", "020009" });
                    }
                    else if (index == 3)
                    {
                        realData.EquipCodes.AddRange(new string[] { "020004", "020006", "020011", "020013" });
                    }
                    else if (index == 4)
                    {
                        realData.EquipCodes.AddRange(new string[] { "020005", "020012" });
                    }
                    
                    SensorRealDataInfos.Add(realData);

                }
                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }
        public int SubstationId { get; private set; }
        public List<SensorRealDataInfo> SensorRealDataInfos { get; private set; }
    }
    class SensorRealDataInfo
    {
        public List<string> EquipCodes { get; set; } = new List<string>();
        public float Value { get; set; }
        public int ValueState { get; set; }
    }
}
