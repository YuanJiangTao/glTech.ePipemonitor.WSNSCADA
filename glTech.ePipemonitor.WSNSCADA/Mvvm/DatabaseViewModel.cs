using MvvmFoundation.Wpf;
using PluginContract.Data;
using PluginContract.Helper;
using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
     class DatabaseViewModel : ObservableObject
    {
        private readonly XmlStorage<DatabaseSg> _databaseStorage;
        private readonly MetroDialog _dialog;
        public DatabaseViewModel(MetroDialog dialog)
        {
            _databaseStorage = new XmlStorage<DatabaseSg>(
                PathHelper.Combine(MessageToken.CONFIG_FOLDER, MessageToken.DATABASEXML));
            _databaseStorage.Load();

            DatabaseSg = _databaseStorage.Storage;

            ConnectCommand = new RelayCommand(Connect);

            _dialog = dialog;
        }
        public DatabaseSg DatabaseSg { get; }

        public string ServerName
        {
            get => DatabaseSg.ServerName;
            set
            {
                DatabaseSg.ServerName = value;
                RaisePropertyChanged();
            }
        }
        public string DatabaseName
        {
            get => DatabaseSg.DatabaseName;
            set { DatabaseSg.DatabaseName = value; RaisePropertyChanged(); }
        }
        public string UserId
        {
            get => DatabaseSg.UserId;
            set { DatabaseSg.UserId = value; RaisePropertyChanged(); }
        }
        public string Password
        {
            get => DatabaseSg.Password;
            set { DatabaseSg.Password = value; RaisePropertyChanged(); }
        }

        public ICommand ConnectCommand { get; }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
            _databaseStorage.Save();
        }
        private async void Connect()
        {
            if (ConnectUtil.TryConnect(DatabaseSg.ConnectionString()))
                await _dialog.ShowTipsAsync("连接成功!");
            else
                await _dialog.ShowTipsAsync("连接失败!");
            _databaseStorage.Save();
        }
    }

    [Serializable]
    public class DatabaseSg
    {
        public DatabaseSg()
        {

        }
        public string ServerName { get; set; } = "127.0.0.1";
        public string DatabaseName { get; set; } = "ePipemonitor";
        public string UserId { get; set; } = "sa";
        public string Password { get; set; } = "gl";

        public bool IntegratedSecurity { get; set; } = false;

        internal string ConnectionString()
        {
            if (IntegratedSecurity)
                return $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True";
            return
                $"Data Source={ServerName};Initial Catalog={DatabaseName};Persist Security Info=True;User ID={UserId};Password={Password};Application Name=WSNSCADA";
        }

        public DatabaseConfig ToDatabaseConifg()
        {
            return new DatabaseConfig()
            {
                ServerName = ServerName,
                DatabaseName = DatabaseName,
                UserId = UserId,
                Password = Password,
                IntegratedSecurity = IntegratedSecurity,
            };
        }
    }
}
