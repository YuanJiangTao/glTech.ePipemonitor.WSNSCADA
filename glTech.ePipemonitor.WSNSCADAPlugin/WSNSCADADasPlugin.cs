using BootstrapUI.TinyIoC;
using PluginContract;
using PluginContract.Data;
using PluginContract.Utils;
using System;
using System.Collections.ObjectModel;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    public class WSNSCADADasPlugin : BasePlugin
    {
        private readonly ILogDog _log;
        private DasManagerService _service;
        public WSNSCADADasPlugin()
        {
            _log = RegisterLogDog(Title);
            LogD.Ini(_log);
            LogD.Info("Hello Plugin...");
            _service = new DasManagerService();
        }
        public override string Title => "GPRS传感器数据采集";

        public override string Description => "GPRS传感器数据采集";

        public override ObservableCollection<PluginKV> InitKvs()
        {
            return new ObservableCollection<PluginKV>()
            {
                new PluginKV()
                {
                    Key=KvSettingKeyConst.TIMEOUT,
                    Value="3",
                    KvType=KvType.Int,
                    Description="传感器在超时时间内不回应,则判断为断线, 单位秒."
                },
                new PluginKV()
                {
                    Key=KvSettingKeyConst.DAS_GATHER_INTERVAL,
                    Value="10",
                    KvType=KvType.String,
                    Description="数据采集轮训周期，最小周期为5秒."
                },
                new PluginKV()
                {
                    Key=KvSettingKeyConst.NETWORK_OFF_COUNT,
                    Value="1",
                    KvType=KvType.String,
                    Description="网络连接断线屏蔽次数"
                },
                new PluginKV()
                {
                    Key=KvSettingKeyConst.SHOW_DETAILS_LOG,
                    Value="false",
                    KvType=KvType.Bool,
                    Description="是否显示详细日志"
                },
                new PluginKV()
                {
                    Key=KvSettingKeyConst.SENDCOMMAND_AGAIN,
                    Value="false",
                    KvType=KvType.Bool,
                    Description="是否启用命令补发"
                },
                new PluginKV()
                {
                    Key=KvSettingKeyConst.ANALOG_OFF_COUNT,
                    Value="5",
                    KvType=KvType.String,
                    Description="模拟量断线屏蔽次数"
                }
            };
        }
        PluginMonitor _dataBaseMonitor = new PluginMonitor()
        {
            Title = "数据库",
            Columns = new[] { "时间", "日志" },
        };
        PluginMonitor _dasMonitor = new PluginMonitor()
        {
            Title = "采集日志",
            Columns = new[] { "时间", "日志" },
        };

        public override ObservableCollection<PluginMonitor> InitMonitors()
        {
            return new ObservableCollection<PluginMonitor>()
            {
                _dasMonitor,
                _dataBaseMonitor
            };
        }
        private ObservableCollection<PluginMonitor> _pluginMonitors
            = new ObservableCollection<PluginMonitor>();

        public override void OnLoad(IDatabaseConfig hostConfig)
        {
            DasConfig.Init(this);
            DasConfig.InitRepo(hostConfig);

        }

        public override void Start()
        {
            _service.Start(_dataBaseMonitor, _dasMonitor);
        }

        public override void Stop()
        {
            _service.Stop();
        }
    }
}
