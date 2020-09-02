using System.Windows;
using MahApps.Metro.Controls;
using PluginContract;
using MahApps.Metro.Controls.Dialogs;
using PluginContract.Utils;

namespace glTech.ePipemonitor.WSNSCADA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            UIContext.Initialize();
            InitializeComponent();
        }

        private void BtnDatabaseSetting_Click(object sender, RoutedEventArgs e)
        {
            FlyoutSqlSettings.IsOpen = !FlyoutSqlSettings.IsOpen;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            FlyoutSettings.IsOpen = !FlyoutSettings.IsOpen;
        }

        private async void BtnAboutInfo_Click(object sender, RoutedEventArgs e)
        {
            var message = ProductUtil.GetAssemblyDescription(this.GetType().Assembly);
            await this.ShowMessageAsync("关于",$"版本号：{message}" );
        }
    }
}
