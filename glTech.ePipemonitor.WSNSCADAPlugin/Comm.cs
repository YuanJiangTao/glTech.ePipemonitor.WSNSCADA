using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol;
using glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Filter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class Comm : IDisposable
    {
        private SerialPort _serialPort = null;

        private ISendCommand _sendCommand;

        private string _commName;
        private int _baud;
        private int _databits;
        private StopBits _stopBits;
        private Parity _parity;

        public event EventHandler<ChannelDataEventArgs> DataReceivedEvent;
        public Comm(CommMode commMode, string portName, int baud, int databits, StopBits stopbits, Parity parity)
        {
            _commName = portName;
            _baud = baud;
            _databits = databits;
            _stopBits = stopbits;
            _parity = parity;

            if (commMode == CommMode.SerialPort)
            {
                SetupSerialPort();
            }
            else if (commMode == CommMode.TcpClient)
            {
                //暂时先不实现
            }
            else
            {
                throw new Exception("未知的通讯模式!");
            }

        }

        private void SetupSerialPort()
        {
            _serialPort = new SerialPort
            {
                PortName = _commName,
                BaudRate = _baud,
                DataBits = _databits,
                StopBits = _stopBits,
                Parity = _parity
            };
            //必须一定要加上这句话，意思是接收缓冲区当中如果有一个字节的话就出发接收函数，如果不加上这句话，那就有时候触发接收有时候都发了好多次了也没有触发接收，有时候延时现象等等，
            _serialPort.ReceivedBytesThreshold = 1;
            _serialPort.ErrorReceived += SerialPort_ErrorReceived;
            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.DtrEnable = true;
            _serialPort.RtsEnable = true;
            _serialPort.Open();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _sendCommand.IsReceiveResponse = true;
            List<byte> listBuffer = new List<byte>();
            ReadBuffer(ref listBuffer);
            Thread.Sleep(50);
            //再次读取
            ReadBuffer(ref listBuffer);
            if (listBuffer.Any())
            {
                var requestInfo = SubstatonFilter.Filter(listBuffer);
                if (requestInfo == null)
                {
                    IsBitError = true;
                }
                else
                {
                    IsBitError = false;
                    //触发事件
                }
                DataReceivedEvent?.Invoke(this, new ChannelDataEventArgs(_sendCommand, requestInfo));
            }
        }
        private void ReadBuffer(ref List<byte> listBuffer)
        {
            do
            {
                int count = _serialPort.BytesToRead;
                if (count <= 0)
                    break;
                var buffer = new byte[count];
                _serialPort.Read(buffer, 0, count);
                listBuffer.AddRange(buffer);
            } while (_serialPort.BytesToRead > 0);
        }

        public bool IsConnect
        {
            get => _serialPort != null && _serialPort.IsOpen;
        }
        public bool Start()
        {
            if (!IsConnect)
            {
                try
                {
                    Close();
                    SetupSerialPort();
                }
                catch (Exception ex)
                {
                    LogD.Error(ex.ToString());
                }
            }
            return IsConnect;
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            string errMsg = e.EventType switch
            {
                SerialError.RXOver => "发生输入缓冲区溢出。输入缓冲区空间不足，或在文件尾 (EOF) 字符之后接收到字符。",
                SerialError.Overrun => "发生字符缓冲区溢出。下一个字符将丢失。",
                SerialError.RXParity => "硬件检测到奇偶校验错误。",
                SerialError.Frame => "硬件检测到一个组帧错误。",
                SerialError.TXFull => "应用程序尝试传输一个字符，但是输出缓冲区已满。",
                _ => "未知错误!",
            };
            LogD.Error($"串口{_commName}接收数据错误:{errMsg}");
        }
        /// <summary>
        /// 关闭Tcp Client,并释放对象
        /// </summary>
        private void Close()
        {
            try
            {
                if (_serialPort != null)
                {
                    _serialPort.ErrorReceived -= SerialPort_ErrorReceived;
                    _serialPort.DataReceived -= SerialPort_DataReceived;
                    _serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            finally
            {
                _serialPort = null;
            }
        }
        public override string ToString()
        {
            return $"串口{_commName}";
        }

        public void Dispose()
        {
            Close();
        }
        internal bool Send(ISendCommand command)
        {
            try
            {
                if (command == null || command.Data == null)
                    return false;
                if (!IsConnect)
                {
                    Start();
                }
                _sendCommand = command;
                _serialPort.Write(command.Data, 0, command.Data.Length);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool IsBitError { get; private set; }
    }

    public enum CommMode
    {
        /// <summary>
        /// 串口通讯模式
        /// </summary>
        SerialPort = 0,
        /// <summary>
        /// Tcp通讯模式
        /// </summary>
        TcpClient = 1,

    }

    class ChannelDataEventArgs : EventArgs
    {
        public ChannelDataEventArgs(ISendCommand sendCommand, SubstationRequestInfo requestInfo)
        {
            SendCommand = sendCommand;
            RequestInfo = requestInfo;
        }
        public ISendCommand SendCommand { get; private set; }

        public SubstationRequestInfo RequestInfo { get; private set; }
    }
}
