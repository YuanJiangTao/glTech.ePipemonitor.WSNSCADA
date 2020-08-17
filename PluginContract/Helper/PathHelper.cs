using System;
using System.IO;
using System.Linq;

namespace PluginContract.Helper
{
    public class PathHelper
    {
        public static string Combine(params string[] pathes)
        {
            var p = new[] { AppDomain.CurrentDomain.BaseDirectory };
            return Path.Combine(p.Union(pathes).ToArray());
        }
    }
}
