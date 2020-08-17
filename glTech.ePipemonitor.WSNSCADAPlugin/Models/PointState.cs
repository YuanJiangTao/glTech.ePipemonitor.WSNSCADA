using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    enum PointState
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow = 0,
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 1,

        /// <summary>
        /// 分站：通讯中断；模拟量/开关量：断线（没有接传感器）
        /// </summary>
        OFF = 2,
        /// <summary>
        /// 采集服务：挂了(没有初始化数据或者没有刷新数据)
        /// </summary>
        ServiceOff = 3,
        /// <summary>
        /// 设备休眠(分站,模拟量,开关量)
        /// </summary>
        Unused = 4,

        /// <summary>
        /// 模拟量上溢导致的断线
        /// </summary>
        OverflowOFF = 5,
        /// <summary>
        /// 模拟量下溢导致的断线
        /// </summary>
        UnderflowOFF = 6,
        /// <summary>
        /// 分站通讯中断（异常）导致的断线
        /// </summary>
        SubStationOFF = 7,

        /// <summary>
        /// 分站：交流正常
        /// </summary>
        AC = 11,
        /// <summary>
        /// 分站：直流正常
        /// </summary>
        DC = 12,
        /// <summary>
        /// 分站: 通信误码
        /// </summary>
        BitError = 13,
        /// <summary>
        /// 分站：网络中断（如果采用tcp连接有效）
        /// </summary>
        NetworkInterruption = 14,

        /// <summary>
        /// 模拟量：正常
        /// </summary>
        OK = 20,
        /// <summary>
        /// 模拟量:上限断电
        /// </summary>
        UpperLimitSwitchingOff = 21,
        /// <summary>
        /// 模拟量:上限报警
        /// </summary>
        UpperLimitWarning = 22,
        /// <summary>
        /// 模拟量:上限恢复
        /// </summary>
        UpperLimitResume = 23,
        /// <summary>
        /// 模拟量:上限预警
        /// </summary>
        UpperLimitEarlyWarning = 24,
        /// <summary>
        /// 模拟量:下限断电
        /// </summary>
        LowerLimitSwitchingOff = 25,
        /// <summary>
        /// 模拟量:下限报警
        /// </summary>
        LowerLimitWarning = 26,
        /// <summary>
        /// 模拟量:下限恢复
        /// </summary>
        LowerLimitResume = 27,
        /// <summary>
        /// 模拟量:下限预警
        /// </summary>
        LowerLimitEarlyWarning = 28,
        /// <summary>
        /// 开关量:0态
        /// </summary>
        State0 = 30,
        /// <summary>
        /// 开关量:1态
        /// </summary>
        State1 = 31,
        /// <summary>
        /// 开关量:2态
        /// </summary>
        State2 = 32,
    }

    /// <summary>
    /// 馈电类型
    /// </summary>
    enum FeedState
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow = 0,
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 1,
        /// <summary>
        /// 馈电异常，未知原因
        /// </summary>
        Abnormal = 10,

        /// <summary>
        /// 模拟量：馈电正常
        /// </summary>
        OK = 20,
        /// <summary>
        /// 模拟量:上限断电馈电异常
        /// </summary>
        UpperLimitSwitchingOff = 21,
        /// <summary>
        /// 模拟量:上限报警馈电异常
        /// </summary>
        UpperLimitWarning = 22,
        /// <summary>
        /// 模拟量:上限恢复馈电异常
        /// </summary>
        UpperLimitResume = 23,
        /// <summary>
        /// 模拟量:上限预警馈电异常
        /// </summary>
        UpperLimitEarlyWarning = 24,
        /// <summary>
        /// 模拟量:下限断电馈电异常
        /// </summary>
        LowerLimitSwitchingOff = 25,
        /// <summary>
        /// 模拟量:下限报警馈电异常
        /// </summary>
        LowerLimitWarning = 26,
        /// <summary>
        /// 模拟量:下限恢复馈电异常
        /// </summary>
        LowerLimitResume = 27,
        /// <summary>
        /// 模拟量:下限预警馈电异常
        /// </summary>
        LowerLimitEarlyWarning = 28,
        /// <summary>
        /// 开关量:0态馈电异常
        /// </summary>
        State0 = 30,
        /// <summary>
        /// 开关量:1态馈电异常
        /// </summary>
        State1 = 31,
        /// <summary>
        /// 开关量:2态馈电异常
        /// </summary>
        State2 = 32,

    }

    /// <summary>
    /// 测点报警类型;2011-08-22,大于10000为自定义报警
    /// </summary>
    enum AlarmState : byte
    {
        UnKnow = 0,                     //未知

        OFF = 2,                        //分站：通讯中断；模拟量/开关量：断线
        ServiceOff = 3,                  //采集服务挂了(没有初始化数据或者没有刷新数据)

        BitError = 13,                  //分站:通信误码


        UpperLimitSwitchingOff = 21,    //模拟量:上限断电
        UpperLimitWarning = 22,         //模拟量:上限报警
        UpperLimitResume = 23,          //模拟量:上限恢复
        UpperLimitEarlyWarning = 24,    //模拟量:上限预警

        LowerLimitSwitchingOff = 25,    //模拟量:下限断电
        LowerLimitWarning = 26,         //模拟量:下限报警
        LowerLimitResume = 27,          //模拟量:下限恢复
        LowerLimitEarlyWarning = 28,    //模拟量:下限预警

        State0 = 30,                    //开关量:0态
        State1 = 31,                    //开关量:1态
        State2 = 32,                    //开关量:2态
    }

    /// <summary>
    /// 自定义命令关键字
    /// Update：2012.02.28
    /// </summary>
    enum CMDKey
    {
        ConfigureSubSation = 1,             //配置分站
        DeleteSubSation = 2,                //删除分站
        StartReadHistoryData = 3,           //开始读历史数据
        StopReadHistoryData = 4,            //停止读历史数据
        ManualControl = 5,                  //手动控制
        StationReset = 6,                   //分站复位
        AccuDataClear = 7,                  //累积量清零
        SoftRestart = 8,                    //采集软件重启
        ConfigureFlux = 9,                  //配置(添加/编辑)抽放
        DeleteFlux = 10,                    //删除抽放
        MonitoringServerSetting = 11,        //编辑采集服务器
        ResetFlux = 12,                     //清空抽放累计量
        EditLogicPoint = 13,					//逻辑测点更新
        ConfigureElectricPoint = 31,         //配置(添加/编辑)电力测点定义
    }


}
