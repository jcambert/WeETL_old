using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Components;
using WeETL.Observables;

namespace WeETL.Tests
{
    [TestClass]
    public class WaitFileTest
    {
        ETLContext ctx;
        Job job;
        TestScheduler scheduler;
        WaitFileOptions options;
        private async Task Start()
        {
            scheduler.Start();
            await job.Start();

        }

        [ClassInitialize]
        public static void ClassIntilialize(TestContext ctx)
        {

        }
        [TestInitialize]
        public void TestInitialize()
        {
            ctx = new ETLContext();
            Assert.IsNotNull(ctx);
            job = ctx.CreateJob();
            Assert.IsNotNull(job);
            scheduler = new TestScheduler();
            job.OnStart.SubscribeOn(scheduler).Subscribe(j => Debug.WriteLine($"Job start at {j.Item2.ToShortTimeString()}"));
            job.OnCompleted.SubscribeOn(scheduler).Subscribe(j => Debug.WriteLine($"Job completed in {j.Item1.ElapsedTime.Duration()}"));

            options = new WaitFileOptions()
            {
                Path = @"d:\",
                Filter = "*.txt"
            };
        }
        [TestCleanup]
        public void TestCleanup()
        {

            job.Dispose();
            job = null;
        }
        [TestMethod]
        public void TestWaitfile()
        {
            // bool completed = false;
            Debug.WriteLine("Start wait file Test");

            WaitFile wf = new WaitFile(options);
            ConfigureTest(wf);
        }

        [TestMethod]

        public void TestWaitfileETL()
        {
            var wf = ctx.GetService<TWaitFile>();
            Assert.IsNotNull(wf);
            wf.AddToJob(job);
            ConfigureTest(wf,true);
            //await Start();
        }

        private void ConfigureTest(WaitFile wf, bool startWithJob = false)
        {
            CancellationTokenSource cts =startWithJob?job.TokenSource:  new CancellationTokenSource();
            var token = cts.Token;
            token.ThrowIfCancellationRequested();
            //wf.StopOnFirst = true;
            var disp = wf.Output.Subscribe(file =>
            {
                Debug.WriteLine($"{ file.EventArgs.Name} has {file.EventArgs.ChangeType.ToString()}");
                cts.Cancel();//stop on first
            }, () =>
            {
                //System.Environment.Exit(1);
                Debug.WriteLine("Stop listening");
                //completed = true;
            });
            var cpt = 0;

            var filename = $@"d:\{ ETLString.GetAsciiRandomString(6)}.txt";
            var delayed = Task.Delay(500).ContinueWith(t =>
            {
                //if (File.Exists(filename)) File.Delete(filename);
                Debug.WriteLine($"Create {filename}");
                File.WriteAllText(filename, ETLString.GetAsciiRandomString(50));
                Debug.WriteLine($"modify {filename}");
                File.AppendAllLines(filename, new string[] { ETLString.GetAsciiRandomString(50) });
                Debug.WriteLine($"{filename} modified");
                Assert.IsTrue(true, "Test passed");
                cts.Cancel();
            });
            var checker = Task.Delay(3000).ContinueWith(t =>
            {
                cts.Cancel();
                Assert.IsTrue(false, "Checker cancelled test");
            });
            Task.Run(() => { Debug.WriteLine("Start delayed"); return delayed; });
            Task.Run(() => checker);
            if (startWithJob)
                Task.Run(()=>Start());
            else
                wf.Start(cts.Token);
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(100);
                Debug.Write(".");
                if (++cpt > 20)
                {
                    Debug.WriteLine("");
                    cpt = 0;
                }
            }
            File.Delete(filename);
            Assert.IsFalse(File.Exists(filename));
        }
    }
}
