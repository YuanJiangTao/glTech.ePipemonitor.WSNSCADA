using MvvmFoundation.Wpf;
using PluginContract.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace PluginContract
{
    public interface IPlugin
    {
        PluginSetting GetProtocalSetting();
        void Load(IDatabaseConfig hostConfig);
        void Start();
        void Stop();
    }
    public class PluginSetting
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ObservableCollection<PluginMonitor> PluginMonitors { get; set; }
        public ObservableCollection<PluginKV> PluginKVs { get; set; }
    }

    public interface IHostConfig
    {
        IRedisConfig RedisConfig { get; set; }
        IDatabaseConfig DatabaseConfig { get; set; }
        IDatabaseConfig StandyDatabaseConfig { get; set; }

    }
    public interface IRedisConfig
    {
        string RedisServer { get; set; }
        string RedisPwd { get; set; }
    }

    public class RedisConfig
    {
        public string RedisServer { get; set; }
        public string RedisPwd { get; set; }
    }

    public class PluginMonitor
    {
        public void OnWatch(string monitorId, params string[] fields)
        {
            WatchEvent?.Invoke(this, new PluginMonitorEventArgs(monitorId, fields));
        }
        public event EventHandler<PluginMonitorEventArgs> WatchEvent;

        public PluginMonitor()
        {
        }

        /// <summary>
        /// 报警级别0=black,1=blue,2=orange,3=red
        /// </summary>
        public int WarningLevel { get; set; }
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        public string[] Columns { get; set; }

        public string[] PrimaryKeys { get; set; }
    }
    public class PluginKV : ObservableObject
    {
        public PluginKV()
        {

        }
        public PluginKV(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public PluginKV(string key, string value, string description)
            : this(key, value)
        {
            Description = description;
        }
        private string key;
        public string Key
        {
            get => key;
            set
            {
                if (Equals(key, value)) return;
                key = value;
                RaisePropertyChanged();
            }
        }
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                RaisePropertyChanged();
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (Equals(_description, value)) return;
                _description = value;
                RaisePropertyChanged();
            }
        }
        private KvType kvType;
        public KvType KvType
        {
            get => kvType;
            set
            {
                if (Equals(kvType, value)) return;
                kvType = value;
                RaisePropertyChanged();
            }
        }
        private string[] comboBoxItems;
        public string[] ComboBoxItems
        {
            get => comboBoxItems;
            set
            {
                comboBoxItems = value;
                RaisePropertyChanged();
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                if (Equals(isAdmin, value)) return;
                isAdmin = value;
                RaisePropertyChanged();
            }
        }
        private string visiableByKey;

        public string VisiableByKey
        {
            get => visiableByKey;
            set
            {
                if (Equals(visiableByKey, value)) return;
                visiableByKey = value;
                RaisePropertyChanged();
            }
        }
        public Func<string, bool> VisiablePredicate { get; set; }
        public Func<string, string, Tuple<string, string>> Executor { get; set; }
        public string ExecutorName { get; set; }
    }

    public enum KvType
    {
        String = 0,
        Int = 1,
        Float = 2,
        File = 3,
        Combobox = 4,
        Bool = 5,
        StringApi = 6,
        Filter = 7,
        BackupFile = 8,
    }
}
