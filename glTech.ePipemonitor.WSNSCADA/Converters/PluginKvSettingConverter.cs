using glTech.ePipemonitor.WSNSCADA.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace glTech.ePipemonitor.WSNSCADA.Converters
{
    class PluginKvSettingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var kvms = value as ObservableCollection<PluginKVViewModel>;
            if (kvms != null)
            {
                return kvms.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
