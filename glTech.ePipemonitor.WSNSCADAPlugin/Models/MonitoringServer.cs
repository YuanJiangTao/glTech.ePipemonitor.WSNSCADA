using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    [Serializable]
    [XmlRoot(ElementName = "MonitoringServer")]
    public class MonitoringServer
    {
        public MonitoringServer()
        {

        }
        [XmlElement(ElementName = "CommunicationMode")]
        public CommunicationMode CommunicationMode { get; set; }
        [XmlElement(ElementName = "CommunicationConfig")]

        public CommunicationConfig CommunicationConfig { get; set; }
        [XmlElement(ElementName = "InstallUp")]
        public InstallUp InstallUp { get; set; }
    }
    [Serializable]
    public class CommunicationMode
    {
        public CommunicationMode()
        {

        }
        [XmlAttribute]
        public int Value { get; set; }
        [XmlIgnore]
        public CommMode CommMode
        {
            get => (CommMode)Value;
        }
    }
    [Serializable]
    public class CommunicationConfig
    {
        public CommunicationConfig()
        {

        }
        [XmlAttribute]
        public int COM { get; set; }
        [XmlAttribute]
        public int BaudRate { get; set; }
        [XmlAttribute]
        public byte DataBits { get; set; }
        [XmlAttribute]
        public byte StopBits { get; set; }
        [XmlAttribute]
        public string Parity { get; set; }
        [XmlAttribute]
        public string VirtualCOM { get; set; }
        [XmlAttribute]
        public string DeviceIP { get; set; }
        [XmlAttribute]
        public int Port { get; set; }

        public Parity GetParity()
        {
            if (Parity == "N")
                return System.IO.Ports.Parity.None;
            else
                return System.IO.Ports.Parity.None;
        }
    }

    [Serializable]
    public class InstallUp
    {
        public InstallUp()
        {

        }
        [XmlAttribute]
        public string IP { get; set; }
    }
}
