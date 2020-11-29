using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Components;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Observers;
using WeETL.Schemas;

namespace WeETL.Tests
{
    [DebuggerDisplay("Index={Index}")]
    class TestSchema1
    {
        public int Index { get; set; }
    }
    [TestClass]
    public class JobTest
    {
        static string path;
        ETLContext ctx;
        Job job;
        TestScheduler scheduler;
        private async Task Start()
        {
            scheduler.Start();
            await job.Start();

        }

        [ClassInitialize]
        public static void ClassIntilialize(TestContext ctx)
        {
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "temp");

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
        }
        [TestCleanup]
        public void TestCleanup()
        {

            job.Dispose();
            job = null;
        }

        void GenerateFiles(int nbre)
        {

            Directory.CreateDirectory(path);
            RowGenerator<FilenameSchema> fileGen = new RowGenerator<FilenameSchema>(new RowGeneratorOptions<FilenameSchema>().GeneratorFor(r => r.Filename, r => ETLString.GetAsciiRandomString()));
           
           
            fileGen.NumberOfRowToGenerate = nbre;
            for (int i = 0; i < nbre; i++)
            {
                using (File.Create(Path.Combine(path, $"{fileGen.Generate().Filename}.txt")))
                {

                }
            }


        }
        void CleanFiles()
        {
            try
            {

                Directory.Delete(path, true);
            }
            finally
            {
                Directory.CreateDirectory(path);
            }
        }

        [DataTestMethod]
        [DataRow(10)]
        public async Task TestTFileList(int nbreOfFile)
        {
            CleanFiles();
            GenerateFiles(nbreOfFile);
            Assert.AreEqual(nbreOfFile, Directory.EnumerateFiles(path).Count());
            TFileList<FilenameSchema> liste = ctx.GetService<TFileList<FilenameSchema>>();
            Assert.IsNotNull(liste);
            RegisterComponentForEvents(liste);
            liste.Path = path;
            liste.AddToJob(job);
            var dbg = new DebugObserver<FilenameSchema>(nameof(TestTFileList));
            liste.OnOutput.SubscribeOn(scheduler).Subscribe(dbg);
            scheduler.Start();
            await job.Start();
        }
        [DataTestMethod]
        [DataRow(true, 10, false)]
        [DataRow(false, 10, false)]
        [DataRow(true, 10, true)]
        [DataRow(false, 10, true)]
        public async Task TestTFileListExist(bool enabled, int nbreOfFile, bool deleting)
        {
            CleanFiles();
            GenerateFiles(nbreOfFile);
            Assert.AreEqual(nbreOfFile, Directory.EnumerateFiles(path).Count());

            bool hasfile = false;
            int counterExist = 0, counterDeleted = 0;

            TFileList<FilenameSchema> liste = ctx.GetService<TFileList<FilenameSchema>>();
            Assert.IsNotNull(liste);
            RegisterComponentForEvents(liste);
            liste.Path = path;
            liste.Enabled = enabled;
            liste.OnOutput.SubscribeOn(scheduler).Subscribe(e =>
            {
                Debug.WriteLine(e.Filename);
                hasfile = true;
                ++counterExist;
            });

            liste.AddToJob(job);


            TFileDelete<FilenameSchema, FilenameSchema> delete = ctx.GetService<TFileDelete<FilenameSchema, FilenameSchema>>();
            Assert.IsNotNull(delete);
            RegisterComponentForEvents(delete);
            delete.OnOutput.SubscribeOn(scheduler).Subscribe(e =>
            {
                ++counterDeleted;
            });
            delete.Passthrue = deleting;
            delete.AddInput(job, liste.OnOutput);


            await Start();
            if (enabled)
            {
                Assert.IsTrue(hasfile);
                Assert.IsTrue(liste.Enabled);
                Assert.AreEqual(nbreOfFile, counterExist);

            }
            else
            {
                Assert.IsFalse(hasfile);
                Assert.IsFalse(liste.Enabled);
                Assert.AreEqual(counterExist, 0);



            }
            Assert.AreEqual(counterDeleted, counterExist);
            if (!delete.Passthrue && liste.Enabled)
            {
                Assert.AreEqual(Directory.EnumerateFiles(path).Count(), 0);
            }

        }

        [TestMethod]
        public async Task TestTWaitForFile()
        {
            bool hasfile = false;
            CleanFiles();
            TWaitForFile wff = ctx.GetService<TWaitForFile>();
            Assert.IsNotNull(wff);
            wff.Options.Path = path;
            wff.Options.StopOnFirst = true;
            RegisterComponentForEvents(wff);
           

            wff.OnOutput.SubscribeOn(scheduler)/*.Where(f => f.ChangeType == WatcherChangeTypes.Created)*/.Subscribe(f =>
                {
                    Debug.WriteLine($"{f.Name} was created");
                    hasfile = true;
                },()=> {
                    wff.Stop();
                });
            wff.OnCompleted.Subscribe(x => {
                Debug.WriteLine("WFF Completed");
            });
            wff.AddToJob(job);

            await Start();
            GenerateFiles(10);
            job.OnCompleted.Subscribe(job =>
            {
                Assert.IsTrue(hasfile);
            });

           
        }

        [DataTestMethod]
        [DataRow(SortOrder.Ascending, 10, 1, 100)]
        [DataRow(SortOrder.Descending, 100, 200, 300)]
        public async Task TestTRowGenerator(SortOrder order, int nbre, int from, int to)
        {
            int _counter = 0;
            int _index = order switch
            {
                SortOrder.Ascending => from - 1,
                SortOrder.Descending => to + 1,
                _ => throw new Exception("this Sort order is not managed")

            };
            TRowGenerator<TestSchema1> gen = ctx.GetService<TRowGenerator<TestSchema1>>();
            Assert.IsNotNull(gen);
            gen.Options=new RowGeneratorOptions<TestSchema1>().GeneratorFor(s => s.Index, e => ETLString.GetIntRandom(from, to));
            gen.NumberOfRowToGenerate = nbre;
            RegisterComponentForEvents(gen);
            gen.AddToJob(job);

            TSortRow<TestSchema1, TestSchema1> sort = ctx.GetService<TSortRow<TestSchema1, TestSchema1>>();
            Assert.IsNotNull(sort);
            RegisterComponentForEvents(sort);
            sort.OnOutput.Subscribe(row =>
            {
                // Debug.WriteLine($"{row.Index.ToString()}");
                if (order == SortOrder.Ascending)
                    Assert.IsTrue(row.Index >= _index);
                else
                    Assert.IsTrue(row.Index <= _index);
                _index = row.Index;
                ++_counter;
            });

            sort.AddOrderBy(r => r.Index, order);
            sort.AddInput(job, gen.OnOutput);

            TLogRow<TestSchema1> log = ctx.GetService<TLogRow<TestSchema1>>();
            Assert.IsNotNull(log);
            log.ShowHeader = true;
            log.Mode = TLogRowMode.Table;
            log.Alignment = TLogRowAlignment.Center;
            log.AdditionalSpace = 2;
            log.ShowItemNumber = true;
            RegisterComponentForEvents(log);
            log.AddInput(job, sort.OnOutput);
            job.OnCompleted.SubscribeOn(scheduler).Subscribe(job =>
            {
                Assert.AreEqual(_counter, nbre);

            });
            await Start().ContinueWith(t =>
            {
                Thread.Sleep(1000);
            });
        }

        [TestMethod]
        public async Task TestTFileFetch()
        {
            TRest<WeatherSchema> ff = ctx.GetService<TRest<WeatherSchema>>();
            Assert.IsNotNull(ff);
            
            
            ff.Options.Headers.Add("X-Rapidapi-Key", "151c615575msh9dcd2d04eaacee6p1b536fjsnffc5ef311334");
            ff.Options.Headers.Add("X-Rapidapi-Host", "community-open-weather-map.p.rapidapi.com");
            ff.RequestUri = "https://community-open-weather-map.p.rapidapi.com/weather?q=paris&lang=fr";
            RegisterComponentForEvents(ff);

            ff.AddToJob(job);

            TLogRow<WeatherSchema> log = ctx.GetService<TLogRow<WeatherSchema>>();
            Assert.IsNotNull(log);
            log.ShowHeader = true;
            log.Mode = TLogRowMode.Table;
            log.Alignment = TLogRowAlignment.Center;
            log.AdditionalSpace = 2;
            log.ShowItemNumber = true;
            RegisterComponentForEvents(log);
            log.AddInput(job, ff.OnOutput);


            await Start().ContinueWith(t => Thread.Sleep(1000));
        }


        [TestMethod]
        public async Task TestTConvertType()
        {
            TRowGenerator<ConvertSchemaFrom> gen = ctx.GetService<TRowGenerator<ConvertSchemaFrom>>();
            Assert.IsNotNull(gen);
            RegisterComponentForEvents(gen);
            gen.Options=new RowGeneratorOptions<ConvertSchemaFrom>()
            .GeneratorFor(e => e.Index, e => ETLString.GetIntRandom(1, 100))
            .GeneratorFor(e => e.AProperty, e => ETLString.GetAsciiRandomString(20));
            gen.AddToJob(job);
            TLogRow<ConvertSchemaFrom> log1 = ctx.GetService<TLogRow<ConvertSchemaFrom>>();
            Assert.IsNotNull(log1);
            RegisterComponentForEvents(log1);
            log1.AddInput(job, gen.OnOutput);
            log1.Mode = TLogRowMode.Table;
            log1.ShowItemNumber = true;
            TConvertType<ConvertSchemaFrom, ConvertSchemaTo> ct = ctx.GetService<TConvertType<ConvertSchemaFrom, ConvertSchemaTo>>();
            Assert.IsNotNull(ct);
            RegisterComponentForEvents(ct);
            ct.AddInput(job, gen.OnOutput);
            ct.MapperConfiguation(s => s.ForMember(e => e.AnotherProperty, opt => opt.MapFrom(src => $"{src.AProperty}-{src.Index}")));

            TLogRow<ConvertSchemaTo> log = ctx.GetService<TLogRow<ConvertSchemaTo>>();
            Assert.IsNotNull(log);
            RegisterComponentForEvents(log);
            log.AddInput(job, ct.OnOutput);
            log.Mode = TLogRowMode.Table;
            log.ShowItemNumber = true;
            await Start().ContinueWith(t => Thread.Sleep(1000));
        }


        [TestMethod]
        public void TestServicesName()
        {
            var log1 = ctx.GetService<TLogRow<WeatherSchema>>();
            Assert.IsNotNull(log1);
            Assert.AreEqual(log1.Name, "TLogRow-1");
            var log2 = ctx.GetService<TLogRow<WeatherSchema>>();
            Assert.IsNotNull(log2);
            Assert.AreEqual(log2.Name, "TLogRow-2");
            TConvertType<ConvertSchemaFrom, ConvertSchemaTo> ct = ctx.GetService<TConvertType<ConvertSchemaFrom, ConvertSchemaTo>>();
            Assert.IsNotNull(ct);
            Assert.AreEqual(ct.Name, "TConvertType-1");
        }

        [TestMethod]
        public async Task TestCustomGenerator()
        {
            var gen = ctx.GetService<TRowGenerator<CustomGenSchema>>();
            Assert.IsNotNull(gen);
            RegisterComponentForEvents(gen);
            gen.Options = new RowGeneratorOptions<CustomGenSchema>() { Retain = true, InitFunction = (CustomGenSchema row) => { row.Year = DateTime.Now.Year; } }
            .GeneratorFor(e => e.Month, (gen, row) =>
            {
                return row.Month + 1;
            })
            .GeneratorFor(e => e.Year, (gen, row) =>
            {
                if (row.Month > 12)
                {
                    row.Month = 1;
                    return row.Year + 1;
                }
                return row.Year;

            });
            gen.NumberOfRowToGenerate = 100;
            gen.OnOutput.SubscribeOn(scheduler).Subscribe(row =>
            {
                Assert.IsTrue(row.Month > 0);
                Assert.IsTrue(row.Month <= 12);
            });
            gen.AddToJob(job);

            var log = ctx.GetService<TLogRow<CustomGenSchema>>();
            Assert.IsNotNull(log);
            RegisterComponentForEvents(log);
            log.AddInput(job, gen.OnOutput);
            log.ShowItemNumber = true;
            log.AdditionalSpace = 2;
            log.Mode = TLogRowMode.Table;
            log.Alignment = TLogRowAlignment.Center;
            await Start();//.ContinueWith(t => Thread.Sleep(1000));
        }
        [TestMethod]
        public async Task TestTUnite()
        {
            int counter = 0;
            var gen1 = ctx.GetService<TRowGenerator<CustomGenSchema>>();
            Assert.IsNotNull(gen1);
            RegisterComponentForEvents(gen1);
            gen1.Options=new RowGeneratorOptions<CustomGenSchema>(){ Retain=true,InitFunction=(CustomGenSchema row)=> { row.Year = 2020; } }
            
            .GeneratorFor(e => e.Month, (gen, row) =>
            {
                return row.Month + 1;
            });
            
            gen1.NumberOfRowToGenerate = 12;
            gen1.AddToJob(job);

            var gen2 = ctx.GetService<TRowGenerator<CustomGenSchema>>();
            Assert.IsNotNull(gen2);
            RegisterComponentForEvents(gen2);
            gen2.Options=new RowGeneratorOptions<CustomGenSchema>() {Retain=true,InitFunction=(CustomGenSchema row)=> { row.Year = 2021; } }
            .GeneratorFor(e => e.Month, (gen, row) =>
            {
                return row.Month + 1;
            });
            
            gen2.NumberOfRowToGenerate = 12;
            
            gen2.AddToJob(job);

            TUnite<CustomGenSchema> unite = ctx.GetService<TUnite<CustomGenSchema>>();
            Assert.IsNotNull(unite);
            RegisterComponentForEvents(unite);
            unite.AddInput(gen1.OnOutput);
            unite.AddInput(gen2.OnOutput);
            unite.OnOutput.Subscribe(row =>
            {
                Console.WriteLine(row.ToString());
                ++counter;
            },
            () =>
            {
                Console.WriteLine("Unite completed");
            });


            await job.Start().ContinueWith(t =>
            {
                Assert.AreEqual(counter, 24);
            });
        }

        [TestMethod]
        public async Task TestRestOpenWeather()
        {
            var weatherService = ctx.GetService<TOpenWeather>();
            Assert.IsNotNull(weatherService);
            //RegisterComponentForEvents(weatherService);
            weatherService.AddToJob(job);
            weatherService.City = "delle";
            weatherService.ApiKey = "d288da12b207992dd796241cf56014b1";
            weatherService.OnOutput.SubscribeOn(scheduler).Subscribe(new DebugObserver<OpenWeatherMapSchema>(nameof(TestRestOpenWeather)));
            scheduler.Start();
            await job.Start().ContinueWith(t=>Thread.Sleep(2000));
        }
        private void RegisterComponentForEvents(ETLCoreComponent c)
        {
            c.OnStart.SubscribeOn(scheduler).Subscribe(OnComponentStart);
            c.OnError.SubscribeOn(scheduler).Subscribe(OnError);
            c.OnCompleted.SubscribeOn(scheduler).Subscribe(OnComponentCompleted);
        }

        private void OnComponentStart((ETLCoreComponent comp, DateTime dt) c)
        {
            Debug.WriteLine($"{c.comp.Name} start at".PadLeft(50) + $" -> {c.dt.ToShortTimeString()}");

        }
        private void OnComponentCompleted((ETLCoreComponent comp, DateTime dt) c)
        {
            Debug.WriteLine($"{c.comp.Name} Completed in".PadLeft(50) + $" -> {c.comp.ElapsedTime}");
        }


        public void OnError(ConnectorException e)
        {
            Debug.Write("** AN ERROR HAPPENED ** => ");
            Debug.WriteLine(e.Message + "\n" + e.InnerException.Message);
            Assert.Fail(e.InnerException.Message);
        }
    }
}
