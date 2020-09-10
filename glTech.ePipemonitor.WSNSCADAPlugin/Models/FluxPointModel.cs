using PluginContract.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.Models
{
    class FluxPointModel
    {
        /// <summary>
        /// 流量编号
        /// </summary>
        public short FluxID { get; set; }

        /// <summary>
        /// 分站编号
        /// </summary>
        public short SubStationID { get; set; }


        /// <summary>
        /// 抽放安装位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 抽放名称
        /// </summary>
        public string FluxName { get; set; }


        /// <summary>
        /// （甲烷）浓度端口号
        /// </summary>
        public short ConcentrationPort { get; set; }



        /// <summary>
        /// 流量端口号
        /// </summary>
        public short FluxPort { get; set; }

        /// <summary>
        /// 压力端口号
        /// </summary>
        public short PressurePort { get; set; }

        /// <summary>
        /// 温度端口号
        /// </summary>
        public short TemperaturePort { get; set; }

        /// <summary>
        /// CO端口号（非必要）
        /// </summary>
        public short COPort { get; set; }

        /// <summary>
        /// 抽采管道管径 (mm)
        /// </summary>
        public float PipeDiameter { get; set; }

        /// <summary>
        /// 压力标志  0-负压侧绝压 1-负压侧负压 2-正压侧绝压 3-正压侧压力，默认是负压侧负压
        /// </summary>
        public int PressureFlag { get; set; }


        /// <summary>
        /// 当地标准大气压（Kpa） 
        /// </summary>
        public float StandardatMosphere { get; set; } = 101.325f;

        /// <summary>
        /// (混合)流量累积
        /// </summary>
        public float FluxTotal { get; set; }

        /// <summary>
        /// 纯流量累积
        /// </summary>
        public float PureFluxTotal { get; set; }

        /// <summary>
        /// 工况(混合)流量累积
        /// </summary>
        public float IndustrialFluxTotal { get; set; }

        /// <summary>
        /// 是否显示在报表中
        ///0 不显示，1显示
        /// </summary>
        public int IsReport { get; set; }

        /// <summary>
        /// 是否启用
        /// 0 不启用，1启用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 显示在报表中的顺序号
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 启用工况流量(默认标况流量) 0-标况流量;1-工况流量
        /// </summary>
        public bool IsIndustrialFlux { get; set; }

        public FluxRealDataModel FluxRealDataModel { get; } = new FluxRealDataModel();


        public FluxRunModel FluxRunModel { get; private set; } = new FluxRunModel();

        private DateTime _preTime, _realTime;
        internal void InitPointModel(FluxRunModel initModel)
        {
            var fluxCode = $"{SubStationID:D3}A{FluxPort:D2}";
            var concentCode = $"{SubStationID:D3}A{ConcentrationPort:D2}";
            var temptCode = $"{SubStationID:D3}A{TemperaturePort:D2}";
            var pressCode = $"{SubStationID:D3}A{PressurePort:D2}";
            var coCode = $"{SubStationID:D3}A{COPort:D2}";

            InitRealData(fluxCode, ref _fluxReal);
            InitRealData(temptCode, ref _temperatureReal);
            InitRealData(concentCode, ref _concentrationReal);
            InitRealData(pressCode, ref _pressureReal);
            InitRealData(coCode, ref _coReal);

            FluxRealDataModel.FluxID = FluxID;
            FluxRealDataModel.RealDate = DateTime.Now;
            FluxRunModel = InitFluxRunModel(DateTime.Now);
            if (initModel == null)
                return;
            // 防止重新计算.
            _realTime = DateTime.Now;
            _preTime = DateTime.Now;

            FluxRunModel.SpanTime = initModel.SpanTime;
            FluxRunModel.CountSum = initModel.CountSum;
            FluxRunModel.FluxSum = initModel.FluxSum;
            FluxRunModel.PressureSum = initModel.PressureSum;
            FluxRunModel.TemperatureSum = initModel.TemperatureSum;
            FluxRunModel.MethaneChromaSum = initModel.MethaneChromaSum;
            FluxRunModel.IndustrialPureFluxTotal = initModel.IndustrialPureFluxTotal;
            FluxRunModel.IndustrialFluxTotal = initModel.IndustrialFluxTotal;
            FluxRunModel.PureFluxTotal = initModel.PureFluxTotal;
            FluxRunModel.FluxTotal = initModel.FluxTotal;

            FluxRunModel.MethaneChromaMax = initModel.MethaneChromaMax;
            FluxRunModel.MethaneChromaMaxTime = initModel.MethaneChromaMaxTime;
            FluxRunModel.MethaneChromaMin = initModel.MethaneChromaMin;
            FluxRunModel.MethaneChromaMinTime = initModel.MethaneChromaMinTime;
            FluxRunModel.FluxMax = initModel.FluxMax;
            FluxRunModel.FluxMaxTime = initModel.FluxMaxTime;
            FluxRunModel.FluxMin = initModel.FluxMin;
            FluxRunModel.FluxMinTime = initModel.FluxMinTime;
            FluxRunModel.TemperatureMax = initModel.TemperatureMax;
            FluxRunModel.TemperatureMaxTime = initModel.TemperatureMaxTime;
            FluxRunModel.TemperatureMin = initModel.TemperatureMin;
            FluxRunModel.TemperatureMinTime = initModel.TemperatureMinTime;
            FluxRunModel.PressureMax = initModel.PressureMax;
            FluxRunModel.PressureMaxTime = initModel.PressureMaxTime;
            FluxRunModel.PressureMin = initModel.PressureMin;
            FluxRunModel.PressureMinTime = initModel.PressureMinTime;
        }

        /// <summary>
        /// 将5个参数的实时模型初始化
        /// </summary>
        private void InitRealData(string code, ref RealDataModel realModel)
        {
            if (realModel == null)
            {
                realModel = new RealDataModel();
            }

            realModel.PointID = code;
            realModel.RealValue = "初始化";
            realModel.RealState = 1;
            realModel.RealDate = DateTime.Now;
        }

        private FluxRunModel InitFluxRunModel(DateTime now)
        {
            var model = new FluxRunModel();
            model.UpdateTime = now;
            model.FluxID = FluxID;
            model.SubStationID = SubStationID;
            model.Location = Location;
            model.FluxName = FluxName;
            model.ConcentrationPort = (byte)ConcentrationPort;
            model.FluxPort = (byte)FluxPort;
            model.PressurePort = (byte)PressurePort;
            model.TemperaturePort = (byte)TemperaturePort;
            model.PressureFlag = PressureFlag % 2 == 1;
            model.StandardatMosphere = StandardatMosphere;
            model.MethaneChromaMaxTime = now;
            model.MethaneChromaMinTime = now;
            model.FluxMaxTime = now;
            model.FluxMinTime = now;
            model.TemperatureMaxTime = now;
            model.TemperatureMinTime = now;
            model.PressureMaxTime = now;
            model.PressureMinTime = now;
            model.UpdateTime = now;
            model.Year = now.Year;
            model.Month = now.Month;
            model.Day = now.Day;
            model.Hour = now.Hour;
            model.Flag = 1;
            return model;
        }


        public override string ToString()
        {
            return $"FluxId:{FluxID}\tSubstationId:{SubStationID}\tLocation:{Location}\tFluxName:{FluxName}";
        }

        internal void Update(DateTime now, List<RealDataModel> realDataModels)
        {
            //Console.WriteLine($"{"=".Repeat(20)}开始计算抽采测点{FluxID} {Location}{"=".Repeat(20)}");
            _preTime = _realTime;
            _realTime = now;
            float tmpConcertReal, tmpTemptReal, tmpFluxReal, tmpPressure, tmpNegativePress, tmpAbsolutePress;
            double flux, pureFlux, industFlux, industPureFlux;
            var spanTime = CalcSpanTime();
            int count = 1;
            FillRealData(realDataModels);

            CalcFluxRun(spanTime, out tmpConcertReal,
                out tmpTemptReal, out tmpFluxReal, out tmpPressure,
                out tmpNegativePress, out tmpAbsolutePress,
                out flux, out pureFlux,
                out industFlux, out industPureFlux);

            if (_preTime.Hour != _realTime.Hour)
            {
                FluxRunModel = InitFluxRunModel(_realTime);
                FluxRunModel.FluxMax = tmpFluxReal;
                FluxRunModel.FluxMin = tmpFluxReal;
                FluxRunModel.MethaneChromaMax = tmpConcertReal;
                FluxRunModel.MethaneChromaMin = tmpConcertReal;
                FluxRunModel.TemperatureMax = tmpTemptReal;
                FluxRunModel.TemperatureMin = tmpTemptReal;
                FluxRunModel.PressureMax = tmpPressure;
                FluxRunModel.PressureMin = tmpPressure;
                Console.WriteLine("跨小时, 重新累计.");
            }

            FluxRunModel.UpdateTime = _realTime;
            FluxRunModel.FluxTotal += (float)flux;
            FluxRunModel.PureFluxTotal += (float)pureFlux;
            FluxRunModel.IndustrialFluxTotal += (float)industFlux;
            FluxRunModel.IndustrialPureFluxTotal += (float)industPureFlux;
            FluxRunModel.MethaneChromaSum += tmpConcertReal;
            FluxRunModel.TemperatureSum += tmpTemptReal;
            FluxRunModel.FluxSum += tmpFluxReal;
            FluxRunModel.CountSum += count;
            FluxRunModel.SpanTime += spanTime.TotalSeconds;
            Console.WriteLine($"累计时间:{spanTime.TotalSeconds}\t累次次数{FluxRunModel.CountSum}");
            FillFluxRunMinMax(tmpConcertReal, tmpTemptReal, tmpFluxReal, tmpPressure);
            FillFluxRealData();
        }

        private void FillFluxRealData()
        {
            FluxRealDataModel.RealDate = _realTime;
            FluxRealDataModel.MethaneChromaState = _concentrationReal.RealState;
            FluxRealDataModel.MethaneChromaRealValue = _concentrationReal.RealValue;
            FluxRealDataModel.FluxState = _fluxReal.RealState;
            FluxRealDataModel.FluxRealValue = _fluxReal.RealValue;
            FluxRealDataModel.PressureState = _pressureReal.RealState;
            FluxRealDataModel.PressureRealValue = AbsolutePressReal;
            FluxRealDataModel.TemperatureState = _temperatureReal.RealState;
            FluxRealDataModel.TemperatureRealValue = _temperatureReal.RealValue;
            FluxRealDataModel.COState = _coReal.RealState;
            FluxRealDataModel.CORealValue = _coReal.RealValue;
            FluxRealDataModel.PureFluxRealValue = PureFluxReal;
            FluxRealDataModel.IndustrialFluxRealValue = IndustFluxReal;
            FluxRealDataModel.FluxHour = FluxRunModel.FluxTotal.ToString("f2");
            FluxRealDataModel.PureFluxHour = FluxRunModel.PureFluxTotal.ToString("f2");
            FluxRealDataModel.IndustrialFluxHour = FluxRunModel.IndustrialFluxTotal.ToString("f2");
        }


        private void FillFluxRunMinMax(float tmpConcertReal, float tmpTemptReal, float tmpFluxReal, float tmpPressure)
        {
            if (tmpFluxReal > FluxRunModel.FluxMax)
            {
                FluxRunModel.FluxMax = tmpFluxReal;
                FluxRunModel.FluxMaxTime = _realTime;
                Console.WriteLine($"流量最大值:{tmpFluxReal}\t{_realTime}");
            }
            else if (tmpFluxReal < FluxRunModel.FluxMin)
            {
                FluxRunModel.FluxMin = tmpFluxReal;
                FluxRunModel.FluxMinTime = _realTime;
                Console.WriteLine($"流量最小值:{tmpFluxReal}\t{_realTime}");
            }

            if (tmpConcertReal > FluxRunModel.MethaneChromaMax)
            {
                FluxRunModel.MethaneChromaMax = tmpConcertReal;
                FluxRunModel.MethaneChromaMaxTime = _realTime;
                Console.WriteLine($"瓦斯最大值:{tmpConcertReal}\t{_realTime}");
            }
            else if (tmpConcertReal < FluxRunModel.MethaneChromaMin)
            {
                FluxRunModel.MethaneChromaMin = tmpConcertReal;
                FluxRunModel.MethaneChromaMinTime = _realTime;
                Console.WriteLine($"瓦斯最小值:{tmpConcertReal}\t{_realTime}");
            }


            if (tmpTemptReal > FluxRunModel.TemperatureMax)
            {
                FluxRunModel.TemperatureMax = tmpTemptReal;
                FluxRunModel.TemperatureMaxTime = _realTime;
                Console.WriteLine($"温度最大值:{tmpTemptReal}\t{_realTime}");
            }
            else if (tmpTemptReal < FluxRunModel.TemperatureMin)
            {
                FluxRunModel.TemperatureMin = tmpTemptReal;
                FluxRunModel.TemperatureMinTime = _realTime;
                Console.WriteLine($"温度最小值:{tmpTemptReal}\t{_realTime}");
            }


            if (tmpPressure > FluxRunModel.PressureMax)
            {
                FluxRunModel.PressureMax = tmpPressure;
                FluxRunModel.PressureMaxTime = _realTime;
                Console.WriteLine($"压力最大值:{tmpPressure}\t{_realTime}");
            }
            else if (tmpPressure < FluxRunModel.PressureMin)
            {
                FluxRunModel.PressureMin = tmpPressure;
                FluxRunModel.PressureMinTime = _realTime;
                Console.WriteLine($"压力最小值:{tmpPressure}\t{_realTime}");
            }
        }

        private RealDataModel _fluxReal;
        private RealDataModel _temperatureReal;
        private RealDataModel _concentrationReal;
        private RealDataModel _pressureReal;
        private RealDataModel _coReal;

        /// <summary>
        /// 填充5个参数的实时信息
        /// </summary>
        /// <param name="realDataModels"></param>
        private void FillRealData(List<RealDataModel> realDataModels)
        {
            var flux = realDataModels.Find(o => o.PointID == _fluxReal.PointID);
            if (flux != null)
            {
                _fluxReal.RealValue = flux.RealValue;
                _fluxReal.RealState = flux.RealState;
                _fluxReal.RealDate = flux.RealDate;

            }
            else
            {
                throw new Exception($"无法在实时表中找到流量{_fluxReal.PointID}");
            }

            var tempt = realDataModels.Find(o => o.PointID == _temperatureReal.PointID);
            if (tempt != null)
            {
                _temperatureReal.RealValue = tempt.RealValue;
                _temperatureReal.RealState = tempt.RealState;
                _temperatureReal.RealDate = tempt.RealDate;
            }
            else
            {
                throw new Exception($"无法在实时表中找到温度{_temperatureReal.PointID}");
            }

            var concert = realDataModels.Find(o => o.PointID == _concentrationReal.PointID);
            if (concert != null)
            {
                _concentrationReal.RealValue = concert.RealValue;
                _concentrationReal.RealState = concert.RealState;
                _concentrationReal.RealDate = concert.RealDate;
            }
            else
            {
                throw new Exception($"无法在实时表中找到甲烷{_concentrationReal.PointID}");
            }

            var press = realDataModels.Find(o => o.PointID == _pressureReal.PointID);
            if (press != null)
            {
                _pressureReal.RealValue = press.RealValue;
                _pressureReal.RealState = press.RealState;
                _pressureReal.RealDate = press.RealDate;
            }
            else
            {
                throw new Exception($"无法在实时表中找到压力{_pressureReal.PointID}");
            }

            var co = realDataModels.Find(o => o.PointID == _coReal.PointID);
            if (co != null)
            {
                _coReal.RealValue = co.RealValue;
                _coReal.RealState = press.RealState;
                _coReal.RealDate = co.RealDate;
            }
        }
        /// <summary>
        /// 工况流量实时值
        /// </summary>
        public string IndustFluxReal
        {
            get; private set;
        } = "";

        /// <summary>
        /// 工况纯流量实时值
        /// </summary>
        public string IndustPureFluxReal
        {
            get; private set;
        } = "";

        /// <summary>
        /// 标况纯流量实时值
        /// </summary>
        public string PureFluxReal
        {
            get; private set;
        } = "";

        public string AbsolutePressReal
        {
            get;
            private set;
        } = "";

        /// <summary>
        /// 进行实际计算
        /// </summary>
        private void CalcFluxRun(TimeSpan spanTime, out float tmpConcertReal,
            out float tmpTemptReal, out float tmpFluxReal, out float tmpPressure,
            out float tmpNegativePress, out float tmpAbsolutePress,
            out double flux, out double pureFlux,
            out double industFlux, out double industPureFlux)
        {
            //标矿混合流量实时
            tmpFluxReal = CheckReal(_fluxReal);
            //标矿流量
            tmpTemptReal = CheckReal(_temperatureReal, 20);
            tmpConcertReal = CheckReal(_concentrationReal);
            tmpPressure = CheckReal(_pressureReal);
            tmpNegativePress = tmpPressure;
            CalcPressure(tmpPressure, ref tmpNegativePress, out tmpAbsolutePress);
            /*
            float tmpFluxValue = 0;


            if (IsIndustrialFlux)
            {
                tmpFluxValue = (tmpFluxReal * tmpAbsolutePress * 293) / (101.325f * (273 + tmpTemptReal));
            }
            */

            if (!IsIndustrialFlux)
            {
                //tmpFluxReal为标矿混合流量实时
                // 标况纯流量实时值
                PureFluxReal = (tmpFluxReal * tmpConcertReal / 100).ToString("F2");
                // 标况混合流量累计
                flux = tmpFluxReal * spanTime.TotalSeconds / 60;
                // 标况纯流量累计
                pureFlux = flux * tmpConcertReal / 100;

                float tmpIndustFluxReal;
                if (tmpAbsolutePress == 0)
                {
                    LogD.Info("绝压是0");
                    tmpIndustFluxReal = 0f;
                }
                else
                {
                    // 工况混合量实时
                    tmpIndustFluxReal = (101.325f * (273 + tmpTemptReal) / (tmpAbsolutePress * 293)) * tmpFluxReal;
                }

                IndustFluxReal = tmpIndustFluxReal.ToString("F2");

                IndustPureFluxReal = (tmpIndustFluxReal * tmpConcertReal / 100).ToString("F2");

                // 工况混合流量累计
                industFlux = tmpIndustFluxReal * spanTime.TotalSeconds / 60;
                // 工况纯流量累计
                industPureFlux = industFlux * tmpConcertReal / 100;

                AbsolutePressReal = tmpAbsolutePress.ToString("F2");
            }
            else
            {

                //工况纯流量实时值
                IndustPureFluxReal = (tmpFluxReal * tmpConcertReal / 100).ToString("F2");
                //工况混合流量
                IndustFluxReal = tmpFluxReal.ToString("F2");
                //工况混合流量累计
                industFlux = tmpFluxReal * spanTime.TotalSeconds / 60;
                // 工况纯流量累计
                industPureFlux = industFlux * tmpConcertReal / 100;


                if (tmpAbsolutePress == 0)
                {
                    LogD.Info("绝压是0");
                }
                var tmpFluxRealback = (tmpAbsolutePress * 293) / (101.325f * (273 + tmpTemptReal)) * tmpFluxReal;
                // 标况混合量实时
                PureFluxReal = (tmpFluxRealback * tmpConcertReal / 100).ToString("F2");
                //PureFluxReal = tmpFluxRealback .ToString("F2");  标况混合流量实时值
                // 标况混合流量累计
                flux = tmpFluxRealback * spanTime.TotalSeconds / 60;
                // 标况纯流量累计
                pureFlux = flux * tmpConcertReal / 100;
                AbsolutePressReal = tmpAbsolutePress.ToString("F2");
            }
        }

        /// <summary>
        /// 计算绝压&负压
        /// </summary>
        private void CalcPressure(float analogValue, ref float tmpNegativePress, out float tmpAbsolutePress)
        {
            switch (PressureFlag)
            {
                case 0:
                    //负压测绝压
                    FluxRunModel.PressureFlag = false;
                    tmpAbsolutePress = Math.Abs(analogValue);
                    tmpNegativePress = 0 - Math.Abs(Math.Abs(analogValue) - StandardatMosphere);
                    FluxRunModel.PressureSum += tmpAbsolutePress;
                    break;
                case 1:
                    //负压测负压

                    FluxRunModel.PressureFlag = true;
                    tmpAbsolutePress = StandardatMosphere - Math.Abs(analogValue);
                    tmpNegativePress = 0 - Math.Abs(analogValue);
                    FluxRunModel.PressureSum += tmpNegativePress;
                    break;
                case 2:
                    //正压测绝压
                    FluxRunModel.PressureFlag = false;
                    tmpAbsolutePress = Math.Abs(analogValue);
                    tmpNegativePress = Math.Abs(analogValue) - StandardatMosphere;
                    FluxRunModel.PressureSum += tmpAbsolutePress;
                    break;
                case 3:
                    //正压测压力
                    FluxRunModel.PressureFlag = true;
                    tmpAbsolutePress = StandardatMosphere + Math.Abs(analogValue);
                    tmpNegativePress = Math.Abs(analogValue);
                    FluxRunModel.PressureSum += tmpAbsolutePress;
                    break;
                default:
                    tmpNegativePress = 0;
                    tmpAbsolutePress = 0;
                    FluxRunModel.PressureSum += 0;
                    FluxRunModel.PressureFlag = false;
                    break;
            }
        }

        private float CheckReal(RealDataModel realDataModel, float? placeholder = null)
        {
            var state = (PointState)realDataModel.RealState;

            switch (state)
            {
                case PointState.OK:
                case PointState.UpperLimitEarlyWarning:
                case PointState.UpperLimitResume:
                case PointState.UpperLimitSwitchingOff:
                case PointState.UpperLimitWarning:
                case PointState.LowerLimitEarlyWarning:
                case PointState.LowerLimitResume:
                case PointState.LowerLimitSwitchingOff:
                case PointState.LowerLimitWarning:
                    break;
                default:
                    if (placeholder == null)
                    {
                        throw new Exception($"传感器 {realDataModel} {state} ");
                    }
                    else
                    {
                        return placeholder.Value;
                    }
            }

#if DEBUG
            if (Math.Abs(_realTime.Subtract(realDataModel.RealDate).TotalSeconds) > 300)
#else
            if (Math.Abs(_realTime.Subtract(realDataModel.RealDate).TotalSeconds) > 120)
#endif

            {
                LogD.Info($"传感器 {realDataModel} 时间超过1分钟");
            }

            if (!float.TryParse(realDataModel.RealValue, out float floatValue))
            {

            }
            return floatValue;
        }
        private TimeSpan CalcSpanTime()
        {
            var spanTime = _realTime.Subtract(_preTime);
            if (_preTime == DateTime.MinValue)
            {
                spanTime = _realTime.Subtract(_realTime);
            }
            else if (_preTime > _realTime)
            {
                spanTime = _preTime.Subtract(_realTime);
            }

            return spanTime;
        }

    }
}
