using Microsoft.Diagnostics.Runtime;
using StackTraceMonitorLib;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace StackTraceMonitorConsole
{
    class Program
    {

        class Dummy
        {
            public int Num { get; set; }
        }

        private static ConcurrentDictionary<string, Dummy> _stats = new ConcurrentDictionary<string, Dummy>();

        static void Main(string[] args)
        {

            var  monitor = StackTraceMonitorManager.CreateMonitor();

            monitor.DataFlush += Monitor_DataFlush;

            monitor.Start();

            Console.ReadKey();

            foreach (var pair in _stats)
            {
                Console.WriteLine(pair.Key);
                Console.WriteLine(pair.Value.Num);
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static void Monitor_DataFlush(object sender, StackDataItem[] eargs)
        {
            foreach (var item in eargs)
            {
                var key = string.Join(Environment.NewLine, item.trace);
                _stats.AddOrUpdate(key, new Dummy { Num = 1 }, (k, val) =>
                    {
                        val.Num++;
                        return val;
                    });
            }
        }
    }
}
