using System;
using System.Collections.Generic;
using System.Text;

namespace PluginContract.Utils
{
    public interface ILogDog : IDisposable
    {
        void Info(string msg);
        void Debug(string msg);
        void Warn(string msg);
        void Error(string msg);
        void Fatal(string msg);


        void Info(string msg, Exception ex);
        void Debug(string msg, Exception ex);
        void Warn(string msg, Exception ex);
        void Error(string msg, Exception ex);
        void Fatal(string msg, Exception ex);
    }
}
