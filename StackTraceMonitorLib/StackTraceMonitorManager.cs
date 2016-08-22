using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceMonitorLib
{
    public static class StackTraceMonitorManager
    {

        public static StackTraceMonitor CreateMonitor()
        {
            var settings = ConfigSettingsManager.LoadSettingsOrGetDefault();

            var monitor = new StackTraceMonitor(settings);

            return monitor;
        }

    }
}
