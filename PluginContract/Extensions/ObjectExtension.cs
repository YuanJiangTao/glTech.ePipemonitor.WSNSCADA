using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginContract.Extensions
{
    public static class ObjectExtension
    {
        public static T DeepClone<T>(this T @this)
             where T : class
        {
            var json = JsonConvert.SerializeObject(@this);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
