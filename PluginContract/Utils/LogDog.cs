//using log4net;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginContract.Utils
{
    public class LogDog : ILogDog
    {
        private readonly Logger _log;

        public LogDog(Logger log)
        {
            _log = log;
        }

        public void Info(string msg)
        {
            _log.Info(msg);
        }
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }
        public void Warn(string msg)
        {
            _log.Warn(msg);
        }
        public void Error(string msg)
        {
            _log.Error(msg);
        }
        public void Fatal(string msg)
        {
            _log.Fatal(msg);
        }


        public void Info(string msg, Exception ex)
        {
            _log.Info(ex, msg);
        }
        public void Debug(string msg, Exception ex)
        {
            _log.Debug(ex, msg);
        }
        public void Warn(string msg, Exception ex)
        {
            _log.Warn(ex, msg);
        }
        public void Error(string msg, Exception ex)
        {
            _log.Error(ex, msg);
        }
        public void Fatal(string msg, Exception ex)
        {
            _log.Fatal(ex, msg);
        }

        public void Dispose()
        {
        }
    }
}
