using DY.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Configuration
{
    public class ConfigurationUtility
    {
        #region [ Singleton Instance ]

        private static readonly Lazy<ConfigurationUtility> _Instance = new Lazy<ConfigurationUtility>(() => new ConfigurationUtility());

        public static ConfigurationUtility Instance
        {
            get { return _Instance.Value; }
        }

        private ConfigurationUtility()
        { }

        #endregion

        public string GetValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null)
                throw new CoreException(string.Format("{0} not found", key));
            return value;
        }

        public string GetValue(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return defaultValue;
            return value;
        }

        public string GetConnectionString(string name)
        {
            ConnectionStringSettings stringSettings = ConfigurationManager.ConnectionStrings[name];
            if (stringSettings == null)
                throw new CoreException(string.Format("{0} not connectionstring", name));
            return stringSettings.ConnectionString;
        }
    }
}
