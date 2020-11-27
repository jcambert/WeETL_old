using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reactive.Linq;
using WeETL.Observers;

namespace WeETL.Tests
{
    [TestClass]
    public class ReactiveTest
    {
        TestScheduler scheduler;
        [TestInitialize]
        public void TestInitialize()
        {
            scheduler = new TestScheduler();
        }

        [TestMethod]
        public void TestNumberObservable()
        {
            var numbers = new NumbersObservable(5);
            var subscription =numbers.ObserveOn(scheduler).Subscribe(new DebugObserver<int>("numbers"));
            scheduler.Start();
        }


    }

}
