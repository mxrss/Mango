using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace michaeltroth.blog
{
    public static class Configuration
    {

        public static string MongoServer
        {
            get
            {
                var configSetting = ConfigurationManager.AppSettings["mongoStore"];

                if (string.IsNullOrEmpty(configSetting))
                {
                    throw new ConfigurationException("mongoStore is not configured");
                }

                return configSetting;
            }

        }
    }
}