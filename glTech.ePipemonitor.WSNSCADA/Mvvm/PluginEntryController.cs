using BootstrapUI.TinyIoC;
using glTech.ePipemonitor.WSNSCADAPlugin;
using PluginContract;
using PluginContract.Helper;
using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class PluginEntryController
    {
        private readonly ILogDog _log;
        private readonly List<IPlugin> _plugins = new List<IPlugin>();
        public PluginEntryController(ILogDog log)
        {
            _log = log;
            PluginViewModels = new ObservableCollection<PluginViewModel>();
        }

        public ObservableCollection<PluginViewModel> PluginViewModels { get; set; }

        public Task InitializeComponent()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    ScanPluginFromAssembly();
                }
                catch (Exception ex)
                {
                    _log.Error($"初始化插件：{ex}");
                }
            });
        }


        public  void Start()
        {
            var databaseConfig = TinyIoCContainer.Current.Resolve<DatabaseViewModel>().DatabaseSg.ToDatabaseConifg();
            _plugins.ForEach(p => p.Load(databaseConfig));
            _plugins.ForEach(o => o.Start());
        }
        public void Stop()
        {
            _plugins.ForEach(o => o.Stop());
        }
        private void ScanPluginFromAssembly()
        {
            var container = TinyIoCContainer.Current;
            var assemblies = new List<Assembly>();
            var pluginDir = PathHelper.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), MessageToken.PLUGINS_FOLDER);
            if (Directory.Exists(pluginDir))
            {
                var files = Directory.GetFiles(pluginDir, "*.dll", SearchOption.AllDirectories).Where(o => Path.GetFileNameWithoutExtension(o).Contains(MessageToken.PLUGIN_TOKEN));
                var moduleAssemblies = files.Select(Assembly.LoadFile);
                assemblies.AddRange(moduleAssemblies);
                container.AutoRegister(assemblies, DuplicateImplementationActions.RegisterMultiple);
                IPlugin plugin = new WSNSCADADasPlugin();
                _plugins.Add(plugin);
                _plugins?.ToList().ForEach(p =>
                {
                    PluginViewModels.Add(new PluginViewModel(p));
                });
            }
        }

    }

  
}
