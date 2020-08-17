using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls.Primitives;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class CustomCommandModel
    {
        public int CMDKey { get; set; }
        public int SubStationID { get; set; }
        public int ChannelNO { get; set; }
        public int Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string CMDData { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ResponseData { get; set; }
        public int State { get; set; }
        public int MonitoringServerID { get; set; }
        public int ServicePortNO { get; set; }

        /// <summary>
        /// 接收到命令处理
        /// </summary>
        public void Accept(string response = "")
        {
            if (string.IsNullOrEmpty(response))
            {
                response = "正在执行";
            }
            this.ResponseData = response;
            this.ResponseTime = DateTime.Now;
            this.State = (int)CustomCommandResponseState.Executing;
            DasConfig.Repo.Update(this, ResponseData, CustomCommandResponseState.Executing);
        }
        public void Finish(bool isSuccess = true, string response = "")
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(response))
            {
                response = "执行完成";
            }
            if (isSuccess)
                sb.Append("执行成功,");
            else
                sb.Append("执行失败,");
            sb.Append(response);
            this.ResponseData = sb.ToString();
            this.ResponseTime = DateTime.Now;
            this.State = (int)CustomCommandResponseState.Done;
            DasConfig.Repo.Update(this, ResponseData, CustomCommandResponseState.Done);
        }
    }
    /// <summary>
    /// 自定义命令：响应状态标识
    /// </summary>
    enum CustomCommandResponseState
    {
        /// <summary>
        /// 0：未完成(初始化)
        /// </summary>
        Initial = 0,
        /// <summary>
        /// 1.命令正在执行
        /// </summary>
        Executing = 1,
        /// <summary>
        /// 2：完成
        /// </summary>
        Done = 2,
    }
    /// <summary>
    /// 自定义命令字
    /// </summary>
    enum CustomCommandKey
    {
        /// <summary>
        /// 配置分站
        /// </summary>
        ConfigureSubSation = 1,
        /// <summary>
        /// 删除分站
        /// </summary>
        DeleteSubSation = 2,
        /// <summary>
        /// 开始读历史数据
        /// </summary>
        StartReadHistoryData = 3,
        /// <summary>
        /// 停止读历史数据
        /// </summary>
        StopReadHistoryData = 4,
        /// <summary>
        /// 手动控制
        /// </summary>
        ManualControl = 5,
        /// <summary>
        /// 分站复位
        /// </summary>
        StationReset = 6,
        /// <summary>
        /// 累积量清零
        /// </summary>
        AccuDataClear = 7,
        /// <summary>
        /// 采集软件重启
        /// </summary>
        SoftRestart = 8,
        /// <summary>
        /// 配置(添加/编辑)抽放
        /// </summary>
        ConfigureFlux = 9,
        /// <summary>
        /// 删除抽放
        /// </summary>
        DeleteFlux = 10,
        /// <summary>
        /// 编辑采集服务器
        /// </summary>
        MonitoringServerSetting = 11,
        /// <summary>
        /// 清空抽放累计量
        /// </summary>
        ResetFlux = 12,
        /// <summary>
        /// 逻辑测点更新
        /// </summary>
        EditLogicPoint = 13,
        /// <summary>
        /// 配置(添加/编辑)电力测点定义
        /// </summary>
        ConfigureElectricPoint = 31,
        /// <summary>
        /// 删除电力测点定义
        /// </summary>
        DeleteElectricPoint = 32
    }
}

