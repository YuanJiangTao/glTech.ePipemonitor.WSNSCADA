using BootstrapUI.TinyIoC;
using PluginContract.Utils;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading;
using System.Windows;

namespace glTech.ePipemonitor.WSNSCADA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application,IDisposable
    {
        private const string CLIENTNAME = "WSNSCADA";
        private static Mutex AppMutex = null;  //设为Static成员，是为了在整个程序生命周期内持有Mutex
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            AppMutex = new Mutex(true, CLIENTNAME, out var createdNew);
            if (!createdNew)
            {
                MessageBox.Show("程序已经运行!");
                Shutdown();
            }
            base.OnStartup(e);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var log = TinyIoCContainer.Current.Resolve<ILogDog>();
            log?.Info($"程序错误：{e.ExceptionObject}");
        }
    }
}
