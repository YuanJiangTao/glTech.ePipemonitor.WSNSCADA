using PluginContract.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class LogD
    {
        private static ILogDog _log;
        public static void Ini(ILogDog log)
        {
            _log = log;
        }

        public static void Info(string msg)
        {
            _log.Info(msg);
        }
        public static void Debug(string msg)
        {
            _log.Debug(msg);
        }
        public static void Warn(string msg)
        {
            _log.Warn(msg);
        }
        public static void Error(string msg)
        {
            _log.Error(msg);
        }
        public static void Fatal(string msg)
        {
            _log.Fatal(msg);
        }
        public static void Info(string msg, Exception ex)
        {
            _log.Info(msg, ex);
        }
        public static void Debug(string msg, Exception ex)
        {
            _log.Debug(msg, ex);
        }
        public static void Warn(string msg, Exception ex)
        {
            _log.Warn(msg, ex);
        }
        public static void Error(string msg, Exception ex)
        {
            _log.Error(msg, ex);
        }
        public static void Fatal(string msg, Exception ex)
        {
            _log.Fatal(msg, ex);
        }
    }
}
