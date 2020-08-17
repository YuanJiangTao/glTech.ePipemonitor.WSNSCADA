using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class MetroDialog
    {
        private MainViewModel _mainVm;
        private static MetroDialog _instance;

        public MetroDialog(MainViewModel mainVm)
        {
            this._mainVm = mainVm;
            _instance = this;
        }

        private IDialogCoordinator Dialog
        {
            get
            {
                return _mainVm.Dialog;
            }
        }

        public async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return await Dialog.ShowMessageAsync(_mainVm, title, message, style);
        }

        public async Task ShowTipsAsync(string message)
        {
            await Dialog.ShowMessageAsync(_mainVm, "提示", message);
        }


        public static async Task<MessageDialogResult> StaticShowMessageAsync(string title,
            string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return await _instance.ShowMessageAsync(title, message, style);
        }

        public static async Task StaticShowTipsAsync(string message)
        {
            await _instance.ShowTipsAsync(message);
        }
    }
}
