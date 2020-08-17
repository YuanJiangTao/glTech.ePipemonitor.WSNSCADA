using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace PluginContract.Helper
{
    public static class CultureInfoHelper
    {
        public static void SetDateTimeFormat()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
