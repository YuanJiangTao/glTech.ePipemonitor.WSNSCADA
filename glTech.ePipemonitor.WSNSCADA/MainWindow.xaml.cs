using System.Windows;
using MahApps.Metro.Controls;
using PluginContract;

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

        private void BtnAboutInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
