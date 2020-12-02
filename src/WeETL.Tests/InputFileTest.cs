using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables;
using WeETL.Schemas;

namespace WeETL.Tests
{
    [TestClass]
    public class InputFileTest
    {
        ETLContext ctx;
        Job job;
        TestScheduler scheduler;
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
        }
        [TestCleanup]
        public void TestCleanup()
        {

            job.Dispose();
            job = null;
        }

        [DataTestMethod]
        [DataRow(@"15563001.lcc", InputFileMode.Full)]
        [DataRow(@"15563001.lcc", InputFileMode.LineByLine)]
        public void TestInputFileLine(string filename, InputFileMode mode)
        {
            FileReadLine fl = new FileReadLine() { Filename = filename };
            int caller = 0;
            if (mode == InputFileMode.LineByLine)
            {
                fl.Output.Select(s => new ContentSchema<string>() { Content = s }).Subscribe(s =>
                {

                    Debug.WriteLine($"{++caller}-{s.Content.ToString()}");
                }, () =>
                {

                    Assert.AreEqual(caller, 349);
                });

            }
            if (mode == InputFileMode.Full)
            {
                fl.Output.Aggregate("", (a, b) => $"{a}\n{b}").Select(s => new ContentSchema<string>() { Content = s }).Subscribe(s =>
                {
                    Debug.WriteLine($"{++caller}-{s.Content.ToString()}");

                }, () =>
                {
                    Assert.AreEqual(caller, 1);

                });
            }
        }

        [DataTestMethod]
        [DataRow(@"15563001.lcc", InputFileMode.Full)]
        [DataRow(@"15563001.lcc", InputFileMode.LineByLine)]
        public async Task TestInputFileLineTestOpenFileETL(string filename, InputFileMode mode)
        {
            var inputFileService = ctx.GetService<TInputFile>();
            Assert.IsNotNull(inputFileService);
            inputFileService.RegisterComponentForEvents(scheduler);
            inputFileService.Filename = filename;
            inputFileService.Mode = mode;
            inputFileService.AddToJob(job);

            int caller = 0;
            if (mode == InputFileMode.LineByLine)
            {

                inputFileService.OnOutput.Subscribe(s =>
                {
                    ++caller;
                    //Debug.WriteLine($"{++caller}-{s.Content.ToString()}");
                }, () =>
                {

                    Assert.AreEqual(caller, 349);
                });
                var logRow = ctx.GetService<TLogRow<ContentSchema<string>>>();
                Assert.IsNotNull(logRow);
                logRow.ShowHeader = true;
                logRow.ShowItemNumber = true;
                logRow.Mode = TLogRowMode.Table;
                logRow.RegisterComponentForEvents(scheduler);
                logRow.AddInput(job, inputFileService.OnOutput);
            }
            if (mode == InputFileMode.Full)
            {
                inputFileService.OnOutput.Subscribe(s =>
                {

                    Debug.WriteLine($"{++caller}-{s.Content.ToString()}");

                }, () =>
                {
                    Assert.AreEqual(caller, 1);

                });
            }

            //scheduler.Start();
            await job.Start();
        }

        [DataTestMethod]
        [DataRow(@"15563001{0}.lcc", "")]
        [DataRow(@"15563001{0}.lcc", "ThatDoesNotExist")]

        public void TestInputFileFull(string filename, string notExist)
        {

            var _filename = string.Format(filename, notExist).GetLocalPath();
            var _fileExist = File.Exists(_filename);
            FileReadFull fl = new FileReadFull() { Filename = _filename };
            int caller = 0; int error = 0;
            fl.Output.Select(s => new ContentSchema<string>() { Content = s })
                .Catch((Exception ex) =>
                {
                    Debug.WriteLine(ex.Message); ++error;
                    return Observable.Empty<ContentSchema<string>>();
                })
                .Subscribe(s =>
            {
                Debug.WriteLine($"{++caller}-{s.Content.ToString()}");
            },
            (ex) =>
            {
                if (_fileExist) throw ex;
                Debug.WriteLine($"Good catch of {nameof(TestInputFileFull)}");
            },
            () =>
            {
                if (_fileExist)
                {
                    Assert.AreEqual(caller, 1);
                    Assert.AreEqual(error, 0);
                }
                else
                {
                    Assert.AreEqual(caller, 0);
                    Assert.AreEqual(error, 1);
                }
            });
        }

        [DataTestMethod]
        [DataRow(@"weather.json")]
        [DataRow(@"weatherThatDoesNotExist.json")]
        public async Task TestInputFileJson(string filename)
        {
            bool hasRun = false;
            var _filename = filename.GetLocalPath();
            var _fileExist = File.Exists(_filename);
            var inputJson = ctx.GetService<TInputFileJson<WeatherSchema, WeatherSchema>>();
            Assert.IsNotNull(inputJson);
            inputJson.RegisterComponentForEvents(scheduler);
            inputJson.AddToJob(job);
            inputJson.OnOutput.Subscribe(w =>
            {
                Assert.IsNotNull(w);
                Assert.AreEqual("Delle", w.Name);
                Debug.WriteLine($"the weather at {w.Name} is {w.Coordonnee}");
                hasRun = true;
            },
            (ex) =>
            {
                Debug.WriteLine(ex.Message);
                if (!_fileExist)
                    hasRun = true;
            },
            () => { });
            inputJson.Filename = _filename;
            await job.Start().ContinueWith(t => { Assert.IsTrue(hasRun); });
        }
    }
}
