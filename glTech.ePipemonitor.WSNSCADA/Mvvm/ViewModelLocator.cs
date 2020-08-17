using BootstrapUI.TinyIoC;
using PluginContract.Utils;
using System;

namespace glTech.ePipemonitor.WSNSCADA.Mvvm
{
    class ViewModelLocator
    {
        private TinyIoCContainer _container;
        public ViewModelLocator()
        {
            try
            {
                _container = TinyIoCContainer.Current;
                //must be registered static
                _container.Register<MainViewModel>().AsSingleton();
                RegisterInstance();
                MainViewModel = _container.Resolve<MainViewModel>();
                var metroDialog = new MetroDialog(MainViewModel);
                _container.Register<MetroDialog>().AsSingleton();
                 DatabaseViewModel = _container.Resolve<DatabaseViewModel>();
                GeneralSettingViewModel = _container.Resolve<GeneralSettingViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RegisterInstance()
        {
            ILogDogCollar logDogCollar = new LogDogCollar();
            logDogCollar.Setup(MessageToken.MAINWINDOWTITLE, MessageToken.WSNSCADA);
            var _log = logDogCollar.GetLogger();
            _log.Info($"{MessageToken.MAINWINDOWTITLE} 开始运行...");
            _container.Register<ILogDog>(_log);
            _container.Register<PluginEntryController>().AsSingleton();
            _container.Register<ToastController>().AsSingleton();

        }
        public MainViewModel MainViewModel { get; }
        public DatabaseViewModel DatabaseViewModel { get; }

        public GeneralSettingViewModel GeneralSettingViewModel { get; }
    }
}
