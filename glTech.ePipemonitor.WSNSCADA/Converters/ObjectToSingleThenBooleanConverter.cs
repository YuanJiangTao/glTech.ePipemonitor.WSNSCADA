using System;
using System.Globalization;
using System.Windows.Data;

namespace glTech.ePipemonitor.WSNSCADA.Converters
{
    public class ObjectToSingleThenBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse(value?.ToString(), out _))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
