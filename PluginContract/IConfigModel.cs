using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace PluginContract
{
    public interface IConfigModel
    {
        ObservableCollection<Setting> GeneralSettings { get; }
    }



    /// <summary>
    ///     配置信息类, 承载配置信息.
    /// </summary>
    public class Setting : ObservableObject
    {
        private string _key;


        private object _value;
        private string _description;
        private string _commandText;

        public Setting(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public Setting(string key, object value, SettingType settingType = SettingType.Text)
            : this(key, value)
        {
            Key = key;
            Value = value;
            SettingType = settingType;
        }

        public Setting(string key, object value, string commandText, Func<string> commandExecutor)
            : this(key, value, SettingType.TextWithCommand)
        {
            CommandText = commandText;
            CommandExecutor = commandExecutor;
        }

        public Setting(string key, object value, Dictionary<string, string> comboBoxItems)
            : this(key, value)
        {
            ComboBoxItems = comboBoxItems;
            SettingType = SettingType.Combobox;
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get => _key;
            set
            {
                if (value == _key) return;
                _key = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get => _value;
            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Setting类型
        /// </summary>
        public SettingType SettingType { get; }

        /// <summary>
        /// 是否只在admin下可见
        /// </summary>
        public bool IsAdmin { get; set; } = false;


        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get; set; } = true;

        public Dictionary<string, string> ComboBoxItems { get; set; }

        public string CommandText
        {
            get { return _commandText; }
            set
            {
                if (value == _commandText) return;
                _commandText = value;
                RaisePropertyChanged();
            }
        }

        public Func<string> CommandExecutor { get; set; }

        public override string ToString()
        {
            return $"{Key}:{Value}";
        }
    }


    /// <summary>
    /// 配置类型,通过不同类型可以设置在UI上面的展示效果.
    /// </summary>
    public enum SettingType
    {
        Text = 0,
        Combobox = 1,
        FilePath = 2,
        TextWithCommand = 3,
    }
    /// <summary>
    /// taskschedule的ui上下文.
    /// </summary>
    public class UIContext
    {
        public static TaskScheduler Current { get; private set; }

        public static void Initialize()
        {
            if (Current != null)
                return;

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            Current = TaskScheduler.FromCurrentSynchronizationContext();     //获得对同步上下文调度器的引用
        }
    }
}
