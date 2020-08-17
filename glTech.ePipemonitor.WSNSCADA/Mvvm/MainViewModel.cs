using System;
using System.Collections.Generic;
using System.Text;
using MahApps.Metro.Controls;
using MvvmFoundation.Wpf;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using PluginContract.Utils;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class MainViewModel : ObservableObject
    {
        private readonly PluginEntryController _controller;
        private readonly ILogDog _log;
        public MainViewModel(PluginEntryController controller, ILogDog log)
        {
            _log = log;
            _controller = controller;
            Dialog = DialogCoordinator.Instance;
            Title = MessageToken.MAINWINDOWTITLE;
            LoadCommand = new RelayCommand(Load);
            ToastCommand = new Lazy<RelayCommand>(() => new RelayCommand(Toast)).Value;

            StartCommand = new Lazy<RelayCommand>(() => new RelayCommand(Start)).Value;
            StopCommand=new Lazy<RelayCommand>(() => new RelayCommand(Stop)).Value;

        }

        private void Start()
        {
            _controller.Start();
        }
        private void Stop()
        {
            _controller.Stop();
        }

        public string Title { get; private set; }

        public IDialogCoordinator Dialog { get; }

        public ICommand LoadCommand { get; set; }


        public ICommand ToastCommand { get; set; }

        public ICommand StartCommand { get; set; }

        public ICommand StopCommand { get; set; }
        private async void Load()
        {
            await _controller.InitializeComponent();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
            SelectedPluginVm = _controller.PluginViewModels.FirstOrDefault();
        }

        private PluginViewModel _selectedPluginVm;
        public PluginViewModel SelectedPluginVm
        {
            get => _selectedPluginVm;
            internal set
            {
                _selectedPluginVm = value;
                RaisePropertyChanged();
            }
        }


        private void Toast()
        {
            FlyoutToastIsOpen = !FlyoutToastIsOpen;
        }
        private bool _flyoutToastIsOpen;
        private string _toastText;

        public bool FlyoutToastIsOpen
        {
            get => _flyoutToastIsOpen;
            set
            {
                _flyoutToastIsOpen = value;
                RaisePropertyChanged();
            }
        }
        public string ToastText
        {
            get => _toastText;
            set
            {
                _toastText = value;
                RaisePropertyChanged();
                FlyoutToastIsOpen = true;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTimeNow = DateTime.Now;
        }

        private DispatcherTimer _dispatcherTimer;

        private DateTime _dateTimeNow;
        public DateTime DateTimeNow
        {
            get => _dateTimeNow;

            set
            {
                _dateTimeNow = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<CancelEventArgs> ClosingCommand => new Lazy<RelayCommand<CancelEventArgs>>(() =>
   new RelayCommand<CancelEventArgs>(async e =>
   {
       try
       {
           e.Cancel = true;
           var mySettings = new MetroDialogSettings
           {
               AffirmativeButtonText = "确定",
               NegativeButtonText = "取消",
               AnimateShow = true,
               AnimateHide = false
           };
           var result = await Dialog.ShowMessageAsync(this, "提示",
                                        "要退出程序吗?",
                                        MessageDialogStyle.AffirmativeAndNegative, mySettings);
           if (result == MessageDialogResult.Affirmative)
           {
               e.Cancel = false;
               _log.Info($"{MessageToken.MAINWINDOWTITLE} 退出...");
               Application.Current.Shutdown();
           }
           else
               e.Cancel = true;
       }
       catch (Exception ex)
       {

           throw ex;
       }
   })).Value;

    }
}
