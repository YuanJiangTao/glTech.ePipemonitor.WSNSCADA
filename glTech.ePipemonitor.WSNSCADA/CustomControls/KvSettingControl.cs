using glTech.ePipemonitor.WSNSCADA.Mvvm;
using MvvmFoundation.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace glTech.ePipemonitor.WSNSCADA.CustomControls
{

     class KvSettingControl : Control
    {
        static KvSettingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KvSettingControl), new FrameworkPropertyMetadata(typeof(KvSettingControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            foreach (var item in PluginKVVms)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var triggerKvvm = sender as PluginKVViewModel;
            if (triggerKvvm == null) return;

            foreach (var item in PluginKVVms)
            {
                if (item.VisiableByKey == triggerKvvm.Key)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                    item.RaisePropertyChanged("Value");
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        public ObservableCollection<PluginKVViewModel> PluginKVVms
        {
            get { return (ObservableCollection<PluginKVViewModel>)GetValue(PluginKVVmsProperty); }
            set { SetValue(PluginKVVmsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PluginKVVms.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PluginKVVmsProperty =
            DependencyProperty.Register("PluginKVVms", typeof(ObservableCollection<PluginKVViewModel>), typeof(KvSettingControl), new PropertyMetadata(new ObservableCollection<PluginKVViewModel>()));


        public bool AdminMode
        {
            get { return (bool)GetValue(AdminModeProperty); }
            set { SetValue(AdminModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdminMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdminModeProperty =
            DependencyProperty.Register("AdminMode", typeof(bool), typeof(KvSettingControl), new PropertyMetadata(false));
    }
}
