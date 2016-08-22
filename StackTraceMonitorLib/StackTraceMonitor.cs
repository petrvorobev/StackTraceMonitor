using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace StackTraceMonitorLib
{
    public class StackTraceMonitor
    {
        private ConfigSettings _settings;

        private StackDataManager _dataManager;

        private System.Threading.Timer _timer;

        internal StackTraceMonitor(ConfigSettings settings)
        {
            _settings = settings;
            _dataManager = new StackDataManager(_settings.FlushTimeout);
        }

        private IEnumerable<int> GetProcessIds()
        {
            if (_settings.MonitorSelf)
            {
                yield return Process.GetCurrentProcess().Id;
            }
            else
            {
                foreach (var process in Process.GetProcessesByName(_settings.ProcessName))
                {
                    yield return process.Id;
                }
            }
        }

        public event EventHandler<StackDataItem[]> DataFlush;

        public void Start()
        {
            foreach (var pid in GetProcessIds())
            {
                var processMonitor = new ProcessStackMonitor(pid, _settings, _dataManager);
                processMonitor.RunMonitoringThread();
            }
            _timer = new Timer(new TimerCallback(FlushTimerCallback), this, _settings.FlushInterval, _settings.FlushInterval);
        }

        private void DataFlushCallback(StackDataItem[] data)
        {
            DataFlush?.Invoke(this, data);
        }

        private void FlushTimerCallback(object state)
        {
            _dataManager.Flush(DataFlushCallback);
        }

    }
}
