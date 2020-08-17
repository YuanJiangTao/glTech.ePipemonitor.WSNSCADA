using BootstrapUI.TinyIoC;
using MahApps.Metro.Controls;
using MvvmFoundation.Wpf;
using PluginContract;
using PluginContract.Helper;
using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class PluginViewModel : ObservableObject, IDisposable
    {
        private readonly IPlugin _plugin;
        private readonly ILogDog _log;

        private readonly XmlStorage<PluginKvSgs> _xmlPluginKvSgsStorage;
        private readonly PluginKvSgs _pluginKvSgs;

        public PluginViewModel(IPlugin plugin)
        {
            this._plugin = plugin;
            var protocalSetting = plugin.GetProtocalSetting();
            Title = protocalSetting.Title;
            Description = protocalSetting.Description;
            _log = TinyIoCContainer.Current.Resolve<ILogDog>();
            PluginKVViewModels = new ObservableCollection<PluginKVViewModel>();
            PluginMonitorViewModels = new ObservableCollection<PluginMonitorViewModel>();

            _xmlPluginKvSgsStorage = new XmlStorage<PluginKvSgs>(PathHelper.Combine(MessageToken.CONFIG_FOLDER, MessageToken.SETTINGCONFIG_FOLDER));
            _xmlPluginKvSgsStorage.Load();
            _pluginKvSgs = _xmlPluginKvSgsStorage.Storage;
            var pluginKvs = protocalSetting.PluginKVs;
            var pluginMonitors = protocalSetting.PluginMonitors;
            foreach (var item in pluginKvs)
            {
                if (_pluginKvSgs.PluginKvSgList != null && _pluginKvSgs.PluginKvSgList.Exists(p => p.Key == item.Key))
                {
                    item.Value = _pluginKvSgs.PluginKvSgList.FirstOrDefault(p => p.Key == item.Key).Value;
                }
                var kvm = new PluginKVViewModel(item);
                PluginKVViewModels.Add(kvm);
                kvm.PropertyChanged += Kvm_PropertyChanged;
            }
            foreach(var item in plugin.GetProtocalSetting().PluginMonitors)
            {
                PluginMonitorViewModels.Add(new PluginMonitorViewModel(item));
            }
            SelectedPluginMonitorVm = PluginMonitorViewModels.FirstOrDefault();

        }

        public void Start()
        {

        }
        public void Stop()
        {

        }

        private void Kvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is PluginKVViewModel kvm)
            {
                var kvsg = _pluginKvSgs.PluginKvSgList.FirstOrDefault(p => p.Key == kvm.Key);
                if (kvsg == null)
                {
                    _pluginKvSgs.PluginKvSgList.Add(new PluginKvSg()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
                else
                {
                    kvsg.Value = kvm.Value;
                }
                _xmlPluginKvSgsStorage.Save();
            }
        }

        public void Dispose()
        {
            try
            {
                foreach (var item in PluginKVViewModels)
                {
                    item.PropertyChanged -= Kvm_PropertyChanged;
                }
                PluginKVViewModels.Clear();
                PluginMonitorViewModels.Clear();
            }
            catch (Exception)
            {

            }

        }

        public string Title { get; } = "";
        public string Description { get; } = "";

        public PluginMonitorViewModel SelectedPluginMonitorVm { get; set; }

        public ObservableCollection<PluginKVViewModel> PluginKVViewModels { get; set; }

        public ObservableCollection<PluginMonitorViewModel> PluginMonitorViewModels { get; set; }
    }
}
