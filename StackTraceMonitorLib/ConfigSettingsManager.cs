using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceMonitorLib
{
    static class ConfigSettingsManager
    {
        private static readonly ConfigSettings _defaultSettings = new ConfigSettings
        {
            ProcessName = "LoopTest",
            TraceInterval = 100,
            FlushTimeout = 10,
            FlushInterval = 5000
        };

        public static ConfigSettings DefaultSettings
        {
            get
            {
                return _defaultSettings;
            }
        }

        public static ConfigSettings LoadSettings()
        {
            return null;
        }

        public static ConfigSettings LoadSettingsOrGetDefault()
        {
            return LoadSettings() ?? DefaultSettings; 
        }
    }
}
