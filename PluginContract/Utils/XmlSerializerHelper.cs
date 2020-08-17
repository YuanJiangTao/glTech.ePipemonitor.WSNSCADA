using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PluginContract.Utils
{
    public class XmlSerializerHelper
    {
        public static T Serializer<T>(string xmlString) where T : class, new()
        {
            T t = default;
            using (StringReader sr = new StringReader(xmlString))
            {
                var serializer = new XmlSerializer(typeof(T));
                t = (T)serializer.Deserialize(sr);
            }
            return t;
        }
    }
}
