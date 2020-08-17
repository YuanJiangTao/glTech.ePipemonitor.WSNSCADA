﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace glTech.ePipemonitor.WSNSCADA.Converters
{
    public class MultiBooleanToCollapsedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3)
                return System.Windows.Visibility.Visible;

            // kv 是否需要admin权限
            var isadmin = (bool)values[0];
            // 当前程序是否是adminmode模式
            var adminMode = (bool)values[1];

            var kvvm = (Mvvm.PluginKVViewModel)values[2];

            // kv 可视断言依赖
            var visiableByKey = kvvm.VisiableByKey;
            var pluginKVVms = (ObservableCollection<Mvvm.PluginKVViewModel>)values[3];

            var visiable = !isadmin || (isadmin && adminMode);
            if (visiableByKey != null)
            {
                var visiableBy = pluginKVVms.FirstOrDefault(o => o.Key == visiableByKey);
                if (visiableBy != null)
                {
                    visiable = kvvm.VisiablePredicate(visiableBy.Value);
                }
            }

            return visiable ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
