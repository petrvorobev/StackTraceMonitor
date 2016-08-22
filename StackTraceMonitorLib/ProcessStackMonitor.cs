using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StackTraceMonitorLib
{
    class ProcessStackMonitor
    {
        private int _pid;

        private ConfigSettings _settings;

        private StackDataManager _dataManager;
        public ProcessStackMonitor(int pid, ConfigSettings settings, StackDataManager dataManager)
        {
            _pid = pid;
            _settings = settings;
            _dataManager = dataManager;
        }

        public void RunMonitoringThread()
        {
            var thread = new System.Threading.Thread(new ThreadStart(RunMonitoringThreadInternal));
            thread.Start();
        }

        private void RunMonitoringThreadInternal()
        {
            // just using the first runtime


            //string dacLocation = dataTarget.ClrVersions[0].TryGetDacLocation();
            //var runtime = dataTarget.CreateRuntime()
            while (true)
            {
                using (var dataTarget = DataTarget.AttachToProcess(_pid, 5000, AttachFlag.Passive))
                {
                    ClrInfo runtimeInfo = dataTarget.ClrVersions[0];
                    ClrRuntime runtime = runtimeInfo.CreateRuntime();

                    foreach (var t in runtime.Threads)
                    {
                        var treadId = t.ManagedThreadId;
                        var trace = t.StackTrace.Select(f =>
                           {
                               if (f.Method != null)
                               {
                                   return f.Method.Type.Name + "." + f.Method.Name;
                               }

                               return null;
                           }).Where(x=>x!=null).ToArray();
                        if (trace.Length > 0)
                        {
                            _dataManager.AddItem(new StackDataItem { pid = _pid, threadId = treadId, trace = trace });
                        }

                    }
                }
                Thread.Sleep(_settings.TraceInterval);

            }
        }
    }
}
