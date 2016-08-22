using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceMonitorLib
{
    public struct StackDataItem
    {
        public int pid;

        public int threadId;

        public string[] trace;
    }
}
