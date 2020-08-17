using System;
using System.Collections.Generic;
using System.Text;

namespace PluginContract.Utils
{
    public interface ILogDogCollar : IDisposable
    {
        void Setup(string dir, string logName, string level = "ALL");

        ILogDog GetLogger();
    }
}

