using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Components;
using WeETL.Observables;
using WeETL.Observers;
using WeETL.Schemas;

namespace WeETL.Tests
{
    [TestClass]
    public class DirectoryFileTest
    {

        TestScheduler scheduler;
        private Task Start()
        {
            scheduler.Start();
            return Task.CompletedTask;

        }

        [ClassInitialize]
        public static void ClassIntilialize(TestContext ctx)
        {

        }
        [TestInitialize]
        public void TestInitialize()
        {
            scheduler = new TestScheduler();
        }

        [TestMethod]
        public void TestDirectoryFile()
        {
            DirectoryFile df = new DirectoryFile(@"d:\", "*.txt");
            DebugObserver<string> db = new DebugObserver<string>(nameof(DirectoryFile));
            df.Output.Subscribe(db);
        }

        [TestMethod]
        public async Task TestTFileList()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            TFileList<FilenameSchema> tf = new TFileList<FilenameSchema>();
            tf.Path = @"d:\";
            tf.SearchPattern = "*.txt";

            DebugObserver<FilenameSchema> db = new DebugObserver<FilenameSchema>(nameof(DirectoryFile));
            tf.OnOutput.SubscribeOn(scheduler).Subscribe(db);
            await Start();
            await tf.Start(cts.Token);
        }
    }
}
