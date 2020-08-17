using System;
using System.Collections.Generic;
using System.Text;
using BootstrapUI.TinyIoC;
using MvvmFoundation.Wpf;
using PluginContract;
using System.Windows.Forms;
using System.Windows.Input;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class PluginKVViewModel : ObservableObject
    {
        private readonly MetroDialog MetroDialog;
        private PluginKV _pluginKV;
        public PluginKVViewModel(PluginKV pluginKV)
        {
            _pluginKV = pluginKV;
            InitExecutor();
            MetroDialog = TinyIoCContainer.Current.Resolve<MetroDialog>();
        }

        private void SelectFilePath()
        {
            try
            {
                var folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Value = folderBrowserDialog.SelectedPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 上传文件备份目录
        /// </summary>
        private void BackupFilePath()
        {
            try
            {
                var folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Value = folderBrowserDialog.SelectedPath;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string Key
        {
            get => _pluginKV.Key;
            set
            {
                _pluginKV.Key = value;
                RaisePropertyChanged();
            }
        }

        public string Value
        {
            get => _pluginKV.Value;
            set
            {
                _pluginKV.Value = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _pluginKV.Description;
            set
            {
                _pluginKV.Description = value;
                RaisePropertyChanged();
            }
        }

        public KvType KvType
        {
            get => _pluginKV.KvType;
            set
            {
                _pluginKV.KvType = value;
                RaisePropertyChanged();


            }
        }

        public string[] ComboBoxItems
        {
            get => _pluginKV.ComboBoxItems;
            set
            {
                _pluginKV.ComboBoxItems = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get => _pluginKV.IsAdmin;
            set
            {
                _pluginKV.IsAdmin = value;
                RaisePropertyChanged();
            }
        }


        public string VisiableByKey
        {
            get
            {
                return _pluginKV.VisiableByKey;
            }
            set
            {
                _pluginKV.VisiableByKey = value;
                RaisePropertyChanged();
            }
        }

        public bool VisiablePredicate(string visiableByValue)
        {
            return _pluginKV.VisiablePredicate(visiableByValue);
        }
        public ICommand SelectFilePathCommand { get; set; }
        public ICommand TestWebApiCommand { get; set; }
        public ICommand NewWindowCommand { get; set; }
        public ICommand BackupFilePathCommand { get; set; }
        public string ExecutorName
        {
            get
            {
                return _pluginKV.ExecutorName;
            }
            set
            {
                _pluginKV.ExecutorName = value;
                RaisePropertyChanged();
            }
        }
        public async void TestExecutor()
        {
            try
            {
                Tuple<string, string> item;
                if (Executor != null)
                {
                    item = Executor(_pluginKV.Key, _pluginKV.Value);
                }
                else
                {
                    item = _pluginKV.Executor(_pluginKV.Key, _pluginKV.Value);
                }
                var title = item.Item1;
                var message = item.Item2;
                await MetroDialog.ShowMessageAsync("提示", message, MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 用作全局选项执行用.
        /// </summary>
        public Func<string, string, Tuple<string, string>> Executor { get; set; }
        private void InitExecutor()
        {
            switch (_pluginKV.KvType)
            {
                case KvType.String:
                    break;
                case KvType.Int:
                    break;
                case KvType.Float:
                    break;
                case KvType.File:
                    SelectFilePathCommand = new RelayCommand(SelectFilePath);
                    break;
                case KvType.Combobox:
                    break;
                case KvType.Bool:
                    break;
                case KvType.StringApi:
                    TestWebApiCommand = new RelayCommand(TestExecutor);
                    break;
                case KvType.BackupFile:
                    BackupFilePathCommand = new RelayCommand(BackupFilePath);
                    break;
            }
        }
    }

}
