using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class ToastController
    {
        private MainViewModel _mainVm;
        private readonly ILogDog _logDog;

        public ToastController(MainViewModel mainViewModel, ILogDog logDog)
        {
            _mainVm = mainViewModel;
            _logDog = logDog;
        }
        public void ShowToast(string msg, Exception ex)
        {
            _mainVm.ToastText = $"{msg}\r\n{ex}";
            _logDog.Error(msg, ex);
        }
    }
}
