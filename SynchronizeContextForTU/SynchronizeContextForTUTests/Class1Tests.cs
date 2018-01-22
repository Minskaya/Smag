using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchronizeContextForTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizeContextForTU.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void MethodToTestTest()
        {
            var sync = new ManualResetEvent(false);
            (SynchronizationContext.Current as TestSynchronizationContext)
                .NotifyCompleted += (sender, args) => sync.Set();

            var target = new Class1();

            target.MethodToTest();

            //(SynchronizationContext.Current as TestSynchronizationContext).Depile();
            sync.WaitOne();
            Assert.AreEqual(2, target.MyProperty.Count);
            Assert.AreEqual("B", target.MyProperty.Last());
        }

        [TestInitialize]
        public void TestInitialize()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSynchronizationContext());
        }
    }
}