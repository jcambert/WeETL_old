using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using WeETL.Core;

namespace WeETL.Tests
{
    class NotifyClass : ETLCoreComponent
    {

    }
    [TestClass]
    public class ExtensionsTest
    {
        static NotifyClass notifyClass;
        [ClassInitialize]
        public static void Intilialize(TestContext ctx)
        {
            notifyClass = new NotifyClass();
        }
        [TestMethod]
        public void TestEnabledChanged()
        {
            bool res = notifyClass.Enabled;
            notifyClass.OnPropertyChanged.Where(p=>p=="Enabled").Subscribe(v => { 
                Debug.WriteLine(notifyClass.Enabled); 
            });
            notifyClass.Enabled = !res;
            Assert.AreEqual(res, !notifyClass.Enabled);

        }

        [TestMethod]
        public void TestIntIterator()
        {
            foreach (var item in Enumerable.Range(1,12).Select(x=>x*2))
            {
                Debug.WriteLine(item);
            }
        }
    }
}
