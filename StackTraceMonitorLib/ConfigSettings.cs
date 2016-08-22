using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceMonitorLib
{
    public class ConfigSettings
    {
        public int FlushInterval { get; set; }
        public int FlushTimeout { get; set; }
        public bool MonitorSelf { get; set; }
        public string ProcessName { get; set; }
        public int TraceInterval { get; set; }
    }
}
