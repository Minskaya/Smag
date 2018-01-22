using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizeContextForTU
{
    public class Class1
    {
        public List<string> MyProperty = new List<string>();

        public void MethodToTest()
        {
            var context = SynchronizationContext.Current;

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                return new List<string>() { "A" };
            }).
            ContinueWith(taskA =>
            {
                //context.Post(_ =>
                //{
                Thread.Sleep(1000);
                taskA.Result.Add("B");
                MyProperty.AddRange(taskA.Result);
                //},
                //null);
            }/**/,
            TaskScheduler.FromCurrentSynchronizationContext()
            /**/);
        }
    }
}