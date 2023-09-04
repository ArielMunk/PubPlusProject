using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public static class Common
    {
        public static class SettingsFileService
        {
            public static IConfiguration Configuration;
            public static string GetSetting(string key)
            {
                return Configuration[key];
            }
        }
    }
}
