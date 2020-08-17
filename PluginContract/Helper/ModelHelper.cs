using System.Collections.Generic;
using System.Linq;

namespace PluginContract.Helper
{
    public static class ModelHelper
    {
        public static List<T> CopyThenRemove<T>(List<T> items)
        {
            if (items.Any())
            {
                var ret = items.ToList();
                items.Clear();
                return ret;
            }
            return items;
        }
    }
}
