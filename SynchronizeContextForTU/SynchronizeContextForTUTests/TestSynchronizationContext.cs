using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SynchronizeContextForTU.Tests
{
    internal class TestSynchronizationContext : SynchronizationContext
    {
        //private BlockingCollection<Tuple<SendOrPostCallback, object>> pending = new BlockingCollection<Tuple<SendOrPostCallback, object>>();

        //public void Depile()
        //{
        //    Tuple<SendOrPostCallback, object> item;
        //    while (pending.TryTake(out item))
        //    {
        //        System.Diagnostics.Debug.Write("depile");

        //        item.Item1(item.Item2);
        //    }
        //}

        //public override void Post(SendOrPostCallback d, object state)
        //{
        //    System.Diagnostics.Debug.Write("empile");
        //    pending.Add(new Tuple<SendOrPostCallback, object>(d, state));
        //}

        public event EventHandler NotifyCompleted;

        public override void Post(SendOrPostCallback d, object state)
        {
            d.Invoke(state);
            NotifyCompleted(this, System.EventArgs.Empty);
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            d(state);
        }
    }
}