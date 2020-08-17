using System;
using System.Collections.Generic;
using System.Text;

namespace PluginContract.Data
{
    public interface IDatabaseConfig
    {
        string ServerName { get; set; }
        string DatabaseName { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
        bool IntegratedSecurity { get; set; }
        string ConnectionString();
    }
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ServerName { get; set; } = "127.0.0.1";
        public string DatabaseName { get; set; } = "ePipeMonitor";
        public string UserId { get; set; } = "sa";
        public string Password { get; set; } = "gl";

        public bool IntegratedSecurity { get; set; } = false;
        public string ConnectionString()
        {
            if (IntegratedSecurity)
                return $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True;MultipleActiveResultSets=true";
            return
                $"Data Source={ServerName};Initial Catalog={DatabaseName};Persist Security Info=True;User ID={UserId};Password={Password};MultipleActiveResultSets=true;Application Name=DasManagerPlatform";

        }
        public override string ToString()
        {
            return $"{ConnectionString() }";
        }
    }
}
