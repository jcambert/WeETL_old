using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
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
            notifyClass.OnPropertyChanges(p => p.Enabled).Subscribe(v => { 
                Debug.WriteLine(v); 
            });
            notifyClass.Enabled = !res;
            Assert.AreEqual(res, !notifyClass.Enabled);

        }
    }
}
