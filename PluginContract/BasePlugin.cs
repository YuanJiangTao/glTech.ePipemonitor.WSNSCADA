using PluginContract.Helper;
using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using PluginContract.Data;
using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace PluginContract
{
    public abstract class BasePlugin : IPlugin, IDisposable
    {
        protected ILogDogCollar LogDogCollar;
        protected ILogDog LogDogRoot;
        public string BaseDirectory;
        //private Thread _loadThread;
        public BasePlugin()
        {
            CultureInfoHelper.SetDateTimeFormat();
            LogDogCollar = new LogDogCollar();
            LogDogRoot = RegisterLogDog(Title);
            BaseDirectory = Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location);
            _pluginMonitors = InitMonitors();
            _pluginKVs = InitKvs();
        }
        public abstract string Title { get; }
        public abstract string Description { get; }

        protected ILogDog RegisterLogDog(string logName, string level = "ALL")
        {
            LogDogCollar.Setup(Title, logName, level);
            return LogDogCollar.GetLogger();
        }


        public abstract void Start();

        public abstract void Stop();

        public abstract void OnLoad(IDatabaseConfig hostConfig);

        private ObservableCollection<PluginMonitor> _pluginMonitors;
        private ObservableCollection<PluginKV> _pluginKVs;
        public abstract ObservableCollection<PluginMonitor> InitMonitors();

        public abstract ObservableCollection<PluginKV> InitKvs();

        public void Dispose()
        {
            //_loadThread.Join(3000);
            if (_pluginMonitors != null)
            {
                _pluginMonitors.Clear();
            }
            if (_pluginKVs != null)
            {
                _pluginKVs.Clear();
            }
        }

        public PluginSetting GetProtocalSetting()
        {
            if (_pluginKVs != null)
            {
                foreach (var group in _pluginKVs.GroupBy(o => o.Key))
                {
                    if (group.Count() > 1)
                    {
                        throw new ArgumentException($"{group.Key} 设置了带个相同的键值的选项.");
                    }
                }
            }
            return new PluginSetting()
            {
                Title = Title,
                Description = Description,
                PluginMonitors = _pluginMonitors,
                PluginKVs = _pluginKVs
            };
        }

        public void Load(IDatabaseConfig hostConfig)
        {
            try
            {
                OnLoad(hostConfig);
                //_loadThread = new Thread(() =>
                //{

                //});
                //_loadThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Watch(int index, params string[] fields)
        {
            if (index < 0 || index >= _pluginMonitors.Count)
            {
                throw new IndexOutOfRangeException("超出定义的监视器");
            }
            var monitor = _pluginMonitors[index];
            monitor.OnWatch(monitor.Id, fields);
        }
        public string this[string index]
        {
            get
            {
                if (_pluginKVs == null) return null;

                var kv = _pluginKVs.FirstOrDefault(o => o.Key == index);
                if (kv != null)
                {
                    return kv.Value;
                }
                return null;
            }
            set
            {
                var kv = _pluginKVs.FirstOrDefault(o => o.Key == index);
                if (kv != null)
                {
                    kv.Value = value;
                }
                else
                {
                    // 增加通用配置.
                    kv = new PluginKV
                    {
                        Key = index,
                        Value = value
                    };
                    _pluginKVs.Add(kv);
                    Console.WriteLine($"增加通用配置{kv.Key}-{kv.Value}");
                }
            }
        }
    }
}
