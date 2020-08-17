using PluginContract.Helper;
using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using PluginContract;
using System.ComponentModel;
using MvvmFoundation.Wpf;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class GeneralSettingViewModel : ObservableObject, IDisposable
    {
        private readonly XmlStorage<PluginKvSgs> _xmlPluginKvSgsStorage;
        private readonly PluginKvSgs _pluginKvSgs;
        public GeneralSettingViewModel()
        {
            _xmlPluginKvSgsStorage = new XmlStorage<PluginKvSgs>(PathHelper.Combine(MessageToken.CONFIG_FOLDER, MessageToken.GENERAL_SETTING));
            _xmlPluginKvSgsStorage.Load();
            _pluginKvSgs = _xmlPluginKvSgsStorage.Storage;
            GeneralKvViewModels = new ObservableCollection<PluginKVViewModel>
            {
                new PluginKVViewModel(
                new PluginKV()
                {
                    Key = MessageToken.SAVELOGDAYS,
                    Value = "15",
                    KvType = KvType.Int,
                })
            };
            foreach (var kvvm in GeneralKvViewModels)
            {
                var kv = _pluginKvSgs.PluginKvSgList.Find(o => o.Key == kvvm.Key);
                if (kv != null)
                    kvvm.Value = kv.Value;
                else
                {
                    _pluginKvSgs.PluginKvSgList.Add(new PluginKvSg() { Key = kvvm.Key, Value = kvvm.Value });
                }
                kvvm.PropertyChanged += Kvvm_PropertyChanged;
            }
        }

        private void Kvvm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as PluginKVViewModel;
            var kv = _pluginKvSgs.PluginKvSgList.Find(o => o.Key == vm.Key);
            if (kv != null)
                kv.Value = vm.Value;
            _xmlPluginKvSgsStorage.Save();
        }

        public void Dispose()
        {
            try
            {
                foreach (var kvvm in GeneralKvViewModels)
                {
                    kvvm.PropertyChanged -= Kvvm_PropertyChanged;
                }
                GeneralKvViewModels.Clear();
            }
            catch (Exception)
            {

            }

        }

        public ObservableCollection<PluginKVViewModel> GeneralKvViewModels { get; set; }

        public int KeepLogDays
        {
            get
            {
                foreach (var kvvm in GeneralKvViewModels)
                {
                    if (kvvm.Key == MessageToken.SAVELOGDAYS)
                    {
                        int.TryParse(kvvm.Value, out int ret);
                        return ret;
                    }
                }
                return 15;
            }
        }
    }
}
