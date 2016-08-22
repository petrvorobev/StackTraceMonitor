using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace StackTraceMonitorLib
{
    class StackDataManager
    {

        private ConcurrentBag<StackDataItem> _itemBag;

        private int _flushTimeout;

        public StackDataManager(int flushTimeout)
        {
            _flushTimeout = flushTimeout;
            _itemBag = new ConcurrentBag<StackDataItem>();
        }

        public void AddItem(StackDataItem item)
        {
            _itemBag.Add(item);
        }

        private void AsyncFlush(ConcurrentBag<StackDataItem> itemBag, Action<StackDataItem[]> callback)
        {
            Thread.Sleep(_flushTimeout);
            callback(itemBag.ToArray());
        }

        public void Flush(Action<StackDataItem[]> callback)
        {
            var itemBag = _itemBag;
            _itemBag = new ConcurrentBag<StackDataItem>();
            if (_flushTimeout==0)
            {
                callback(itemBag.ToArray());
            }
            else
            {
                new Action<ConcurrentBag<StackDataItem>, Action<StackDataItem[]>>(AsyncFlush).BeginInvoke(itemBag, callback, null, null);
            }
        }



    }
}
