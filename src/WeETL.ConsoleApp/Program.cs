using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeEFLastic.Extensions.DependencyInjection;
using WeETL.Components;
using WeETL.DependencyInjection;
using WeETL.Numerics;
using WeETL.Observables;
using WeETL.Observables.BySpeed;
using WeETL.Observables.BySpeed.IO;
using WeETL.Observables.Dxf;
using WeETL.Observables.Dxf.Entities;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.IO;
using WeETL.Observables.Dxf.Tables;
using WeETL.Observables.Dxf.Units;
using WeETL.Observers;
using WeETL.Schemas;

namespace WeETL.ConsoleApp
{

    class Program
    {

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        static async Task Main(string[] args)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            //CalculAngle();return;
            //var res=CalculateCenterX(new Vector2(-2, -1.5), new Vector2(3,1.5), 3);

            /* Console.WriteLine(Vector2.AngleBetween(new Vector2(5, 1), new Vector2(2, 3),true) * 180 / Math.PI);
             Console.WriteLine(Vector2.AngleBetween(new Vector2(2, 3), new Vector2(5, 1),true) * 180 / Math.PI);
             Console.WriteLine(Vector2.AngleBetween(new Vector2(5, 1), new Vector2(-3, -4), true) * 180 / Math.PI);
             Console.WriteLine(Vector2.AngleBetween(new Vector2(-3,-4), new Vector2(5,1)) * 180 / Math.PI);
             return;*/

            /* var v1 = new Vector2(20, 5);
             var v2 = new Vector2(-6, 12);
             var v3 = new Vector2(-6, -12);
             var v4 = new Vector2(7,-11);
             Console.WriteLine(Vector2.Angle(v1).ToDegree());
             Console.WriteLine(Vector2.Angle(v2).ToDegree());
             Console.WriteLine(Vector2.Angle(v3).ToDegree());
             Console.WriteLine(Vector2.Angle(v4).ToDegree());*/
            /*
            var res = CalculateCenterX(new Vector2(0, 10), new Vector2(0, -10), 15);
            var v1 = new Vector2(0, 10) - new Vector2(res.Item1.Item1, res.Item1.Item2);
            var v2 = new Vector2(0,-10)- new Vector2(res.Item1.Item1, res.Item1.Item2);
            Console.WriteLine(Vector2.AngleBetween(v1, v2,true) * 180 / Math.PI);

            var v3 = new Vector2(0, 10) - new Vector2(res.Item2.Item1, res.Item2.Item2);
            var v4 = new Vector2(0, -10) - new Vector2(res.Item2.Item1, res.Item2.Item2);
            Console.WriteLine(Vector2.AngleBetween(v4, v3,true) * 180 / Math.PI);*/

            //return;


            string filename = "15563001";// "radioactif";// "calimero";// "asterix";//"15563001";// "15565001";//"temp"
            ETLContext ctx = new ETLContext();
            ctx.ConfigureService(cfg =>
            {
                cfg.UseCommonUtilities()
                .UseGCommands()
                .UseDxf();
            });
            var dxfWriter = ctx.GetService<IDxfWriter>();
            dxfWriter.OnWrited.Subscribe(document =>
            {
                Console.WriteLine($@"d:\{filename}.dxf is writed");
            });

            bool drawFormat = false;
            bool removePriming = false;
            bool useOrigin = true;
            var dxfDocument = ctx.GetService<IDxfDocument>();
            var prgReader = ctx.GetService<ILaserCutBySpeedReader>();
            prgReader.RemovePrimings = removePriming;
            prgReader.OnLoaded.Subscribe(doc =>
            {
                doc.MapToDxf(dxfDocument);
                
                int counter = 0;
                if (drawFormat)
                {
                    dxfDocument.Entities.Add(new Line(Vector2.Zero, new Vector2(doc.Format.Length, 0)) { LayerName = "format" });
                    dxfDocument.Entities.Add(new Line(new Vector2(doc.Format.Length, 0), new Vector2(doc.Format.Length, doc.Format.Width)) { LayerName = "format" });
                    dxfDocument.Entities.Add(new Line(new Vector2(doc.Format.Length, doc.Format.Width), new Vector2(0, doc.Format.Width)) { LayerName = "format" });
                    dxfDocument.Entities.Add(new Line(new Vector2(0, doc.Format.Width), Vector2.Zero) { LayerName = "format" });
                }
                doc.Origins.Keys.ToList().ForEach(originKey =>
                {
                    var origins = doc.Origins[originKey];
                    var piece = doc.Pieces[originKey];
                    origins.ToList().ForEach(origin =>
                    {
                        var cos = Math.Cos(origin.Item3);
                        var sin = Math.Sin(origin.Item3);
                        var rotation = new Matrix3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);

                        int counter2 = 0;
                        var o = useOrigin ? new Vector3(origin.Item1, origin.Item2, 0) : Vector3.Zero;
                        var layer = $"{++counter} {piece.Name}";

                        piece.Lines.ForEach(line =>
                        {
                            Line _line = line.CloneMe();
                            _line.Start += o;
                            _line.End += o;
                            _line.LayerName = layer;
                            if (origin.Item3 != 0.0)
                                _line = _line.TransformMe(rotation, Vector3.Zero);
                            dxfDocument.Entities.Add(_line);
                        });
                        piece.Arcs.ForEach(arc =>
                        {
                            ++counter2;
                            // if( counter2>=29 && counter2<=31)
                            //arc.LayerName = $"{layer}-arc{++counter2}-{arc.Comment}";

                            var _arc = arc.CloneMe();
                            _arc.Center += o;
                            _arc.LayerName = $"arc{counter2} {layer}";
                            _arc.Color = arc.Comment == "G2" ? AciColor.Blue : AciColor.Cyan;

                            if (origin.Item3 != 0.0)
                                _arc = _arc.TransformMe(rotation, Vector3.Zero);
                            dxfDocument.Entities.Add(_arc);
                        });

                    });

                });
                /* doc.Pieces.Keys.ToList().ForEach(key=> {
                     var layer = doc.Pieces[key].Name;
                     doc.Pieces[key].Lines.ForEach(line => { line.LayerName = layer; dxfDocument.Entities.Add(line); });
                     doc.Pieces[key].Arcs.ForEach(arc => { arc.LayerName = layer; dxfDocument.Entities.Add(arc); });


                 });*/
                dxfWriter.Write(dxfDocument, $@"d:\{filename}.dxf");
            });
            prgReader.Load($@"{AppDomain.CurrentDomain.BaseDirectory}{filename}.lcc");


            /* var dxfReader = ctx.GetService<IDxfReader>();
             dxfReader.OnLoaded.Subscribe(doc =>
             {
                 doc.Header.AcadVer = DxfVersion.AutoCad2018;
                 Console.WriteLine("Document Version:"+doc.Header.AcadVer.ToString());
             });
            dxfReader.DxfSection = DxfSection.Entities;
             dxfReader.Load($@"{AppDomain.CurrentDomain.BaseDirectory}ST2018L0804.dxf");*/


            /*   var document = ctx.GetService<IDxfDocument>();
               var dxfWriter = ctx.GetService<IDxfWriter>();
               dxfWriter.OnWrited.Subscribe(document =>
               {
                   Console.WriteLine("Document is writed");
               });

               document.Entities.Add(new Circle(50, 100, 150) { LayerName="Circles"});
               document.Entities.Add(new Line(new Vector2(0, 0), new Vector2(100, 50)) {LayerName="Lines" });
               document.Entities.Add(new Arc(new Vector2(50,0),25,0,180) { LayerName="Arcs",Color=AciColor.Green});
               document.Entities.Add(new Text("HELLO", new Vector2(50, 50),20) { LayerName = "Texts", Color = AciColor.Blue });

               document.Tables.TextStyles.Add(new TextStyle("arial.ttf", "arial", FontStyle.Bold) { Height=5});
               dxfWriter.Write(document, @"d:\circle.dxf");*/

            // Console.ReadLine();
            /* WaitFile wf = new WaitFile(new WaitFileOptions() {
                 Path = @"d:\",
                 Filter = "*.txt"
             });
             var disp=wf.Output.Subscribe(file =>
             {
                 Console.WriteLine($"{ file.EventArgs.Name} has {file.EventArgs.ChangeType.ToString()}");
             },()=> {
                 Console.WriteLine("Stop listening");
             });


             Console.WriteLine($"Listening {wf.Path}");*/

            /*RestRequest<OpenWeatherMapSchema> rest = new RestRequest<OpenWeatherMapSchema>();
            var apiVersion = "2.5";
            var city = "delle";
            var lang = "fr";
            var units = "metric";
            var apiKey = "d288da12b207992dd796241cf56014b1";
            rest.RequestUri= $"http://api.openweathermap.org/data/{apiVersion}/weather?q={city}&lang={lang}&units={units}&appid={apiKey}";
            rest.Subscribe(new ConsoleObserver<OpenWeatherMapSchema>());
            */
            /* var numbers = new NumbersObservable(5);
             var subscription = numbers.Subscribe(new ConsoleObserver<int>("numbers"));*/

            /*     RowGenerator<TestSchema1> rowgen = new RowGenerator<TestSchema1>(new RowGeneratorOptions<TestSchema1>() 
                 .GeneratorFor(e=>e.TextColumn1,e=>ETLString.GetAsciiRandomString(20))
                 .GeneratorFor(e=>e.TextColumn2,(gen,row)=>row.TextColumn1+ " Hacked" )
                  .GeneratorFor(e=>e.UniqueId,e=>Guid.NewGuid())

                 );

                 rowgen.Subscribe(new ConsoleObserver<TestSchema1>());
                 var row=rowgen.Generate();
            */

            //await ReadOfflineProgrammeTurf();
            // await ReadOnlineProgrammeTurf();
            // await Covid19();
            //await Weather();
            // await TestJob();

            //await TestListDirectory();
            //TestInnerJoin();

            // Console.WriteLine(rowgen.Generate());
            // Console.ReadKey();
            // Console.WriteLine(rowgen.Generate());

        }
        static async Task Covid19()
        {
            var ctx = new ETLContext();
            ctx.ConfigureService(cfg =>
            {
                cfg.UseElastic<ElasticDbCovidSettings>(ctx.Configuration, s => s.OnCreate(settings => { settings.DefaultIndex("covid"); }), typeof(Covid19Schema));
            });
            var job = ctx.CreateJob();
            var ff = ctx.GetService<TRest<AllLiveFranceData>>();
            ff.RequestUri = "https://coronavirusapi-france.now.sh/AllDataByDate?date=2020-11-20";

            var log = ctx.GetService<TLogRow<Covid19Schema>>();
            log.AddInput(job, ff.OnOutput.SelectMany(x => x.AllFranceDataByDate));
            log.Mode = TLogRowMode.Table;
            log.ShowItemNumber = true;

            var dboutuput = ctx.GetService<TOutputDb<Covid19Schema, ObjectId>>();
            dboutuput.AddInput(job, ff.OnOutput.SelectMany(x => x.AllFranceDataByDate));

            ff.AddToJob(job);
            await job.Start();
            while (!job.IsCompleted)
            {
                Thread.Sleep(200);
            }
        }
        static async Task Weather()
        {
            var ctx = new ETLContext();
            ctx.ConfigureService(cfg =>
            {
                /* MONGO */
                //cfg.UseMongoDb<MongoDbBookstoreSettings>(ctx.Configuration, null, typeof(OpenWeatherMapSchema));
                /* ELASTIC */
                cfg.UseElastic<ElasticDbBookstoreSettings>(ctx.Configuration, s => s.OnCreate(settings => { settings.DefaultIndex("weather"); }), typeof(OpenWeatherMapSchema));

            });
            var job = ctx.CreateJob();
            //    var dboutuput = ctx.GetService<TOutputDb<OpenWeatherMapSchema, ObjectId>>();
            var ff = ctx.GetService<TOpenWeather>();


            ff.City = "delle";
            ff.ApiKey = "d288da12b207992dd796241cf56014b1";
            ff.OnError.Subscribe(err => { Console.Error.WriteLine($"{err.Message}\n{err.InnerException.Message}"); });
            ff.AddToJob(job);
            //dboutuput.AddInput(job, ff.OnOutput);
            ff.OnOutput.Subscribe(new DebugObserver<OpenWeatherMapSchema>());
            await job.Start();
            while (!job.IsCompleted)
            {
                Thread.Sleep(200);
            }
        }
        static async Task ReadOfflineProgrammeTurf()
        {
            var ctx = new ETLContext();
            ctx.ConfigureService(cfg => cfg.AddEntityFrameworkElastic());
            var job = ctx.CreateJob();
            var fileInput = ctx.GetService<TInputFile>();
            fileInput.Filename = Path.Combine(ctx.ExecutionPath, "14112020.json");
            fileInput.AddToJob(job);

            var strToJson = ctx.GetService<TStringToJson<ProgrammeSchema>>();
            strToJson.AddInput(job, fileInput.OnOutput);

            strToJson.OnOutput.Subscribe(p =>
            {
                Console.WriteLine(p.Programme.Date.ToString());

            });
            strToJson.OnError.Subscribe(ex => Console.Error.WriteLine(ex.InnerException.Message));
            await job.Start();
        }
        static async Task ReadOnlineProgrammeTurf()
        {
            var date = DateTime.Now.Subtract(TimeSpan.FromDays(1)).ToString("ddMMyyyy");

            var ctx = new ETLContext();
            ctx.ConfigureService(cfg =>
            {
                cfg.UseElastic<ElasticDbTurfSettings>(ctx.Configuration, s => s.OnCreate(settings => { settings.DefaultIndex("turf"); }), typeof(Programme));
            });

            var job = ctx.CreateJob();
            job.CreateBag();
            var forReunion = new TForEach<ProgrammeSchema, Reunion>();
            var forCourse = new TForEach<(Reunion, int), Course>();
            var action1 = new TAction<ProgrammeSchema>();
            var restProgramme = ctx.GetService<TRest<ProgrammeSchema>>();
            var log = ctx.GetService<TLogRow<Programme>>();
            log.AddInput(job, restProgramme.OnOutput.Select(x => x.Programme));
            log.Mode = TLogRowMode.Table;
            log.ShowItemNumber = true;
            restProgramme.RequestUri = $"https://online.turfinfo.api.pmu.fr/rest/client/1/programme/{date}?meteo=true&specialisation=INTERNET";
            restProgramme.AddToJob(job);
            //restProgramme.OnBeforeTransform
            ProgrammeSchema programme = new ProgrammeSchema();
            //programme.Programme.Reunions.WithIndex(e=>e.NumeroOfficiel/* (item, index) => (item,index,item.NumeroOfficiel)*/);
            action1.AddInput(job, restProgramme.OnOutput);
            action1.Set((Job job, ProgrammeSchema prg) =>
            {
                dynamic bag = job.Bag(job.Id.ToString());
                bag.Programme = prg;
            });
            forReunion.AddIteration(job, restProgramme.OnOutput, (ProgrammeSchema prg) => prg.Programme.Reunions.Select(x => (x, x.NumeroOfficiel)));
            forCourse.AddIteration(job, forReunion.OnOutput, (r) => r.Item1.Courses.Select(x => (x, r.Item2)));

            var action2 = new TAction<(Course, int)>();
            action2.AddInput(job, forCourse.OnOutput);
            action2.Set((job, cc) =>
            {
                Console.WriteLine(cc.Item1.LibelleCourt);
                var restParticipant = ctx.GetService<TRest<ListeParticipants>>();
                restParticipant.AddToJob(job);
                var r = cc.Item2;
                var c = cc.Item1.Numero;
                restParticipant.RequestUri = $"https://online.turfinfo.api.pmu.fr/rest/client/1/programme/{date}/R{r}/C{c}/participants?specialisation=INTERNET";
                restParticipant.AddToJob(job);
            });


            var restParticipant = ctx.GetService<TRest<ListeParticipants>>();
            //   restParticipant.AddInput()
            /* restProgramme.OnOutput.Subscribe(item =>
             {
                 Console.WriteLine("receiving programme");
                 programme = item;
                 var row = item;

                 foreach (var reunion in row.Programme.Reunions)
                 {
                     var r = reunion.NumeroOfficiel;
                     foreach (var course in reunion.Courses)
                     {
                         var c = course.Numero;
                         var restParticipant = ctx.GetService<TRest<ListeParticipants>>();
                         restParticipant.RequestUri = $"https://online.turfinfo.api.pmu.fr/rest/client/1/programme/{date}/R{r}/C{c}/participants?specialisation=INTERNET";


                         restParticipant.OnOutput.Subscribe(ps =>
                         {
                             foreach (var p in ps.Participants)
                             {
                                 Console.WriteLine($"{course.Libelle} {p.Numero}->{p.Nom}");

                             }
                             course.Participants = ps.Participants;
                         });
                         restParticipant.OnError.Subscribe(ex => Console.Error.WriteLine(ex.InnerException));
                     }
                 }


             });*/

            restProgramme.OnCompleted.Subscribe(j =>
            {
                Console.WriteLine("programme received");
            });
            restProgramme.OnError.Subscribe(ex =>
            {
                Console.Error.WriteLine(ex.InnerException.Message);
            });

            bool usedb = false;
            if (usedb)
            {
                var dbout = ctx.GetService<TOutputDb<Programme, ObjectId>>();
                dbout.AddInput(job, restProgramme.OnOutput.Select(x => x.Programme));

            }
            job.OnCompleted.Subscribe(job =>
            {
                Console.WriteLine("Job is completed");

            });


            await job.Start();
            while (!job.IsCompleted)
            {
                Thread.Sleep(200);
            }
        }
        /*  static async Task TestJob() {
              string jsonFileName= @"d:\test.json";
              Job job = new Job();
              job.OnCompleted.Subscribe(job => {
                  Console.WriteLine($"Elapsed Time:{job.TimeElapsed.TotalSeconds}");
              });



              TRowGenerator<TestSchema1> rowgen = new TRowGenerator<TestSchema1>();
              //rowgen.NumberOfRowToGenerate = 100;
              rowgen
              .Strict(true)
              .GeneratorFor(e => e.UniqueId, e => Guid.NewGuid())
              .GeneratorFor(e => e.TextColumn1, ETLString.GetToto)
              .GeneratorFor(e => e.TextColumn2, e => ETLString.GetAsciiRandomString(8, StringRandomStyle.UpperCase))
              .GeneratorFor(e => e.TextColumn3, row => ETLString.GetIntRandom(1, 100).ToString())

              .AddToJob(job);

              TRowGenerator<TestSchema2> lookupgen = new TRowGenerator<TestSchema2>();
              lookupgen
               .Strict(true)
              .GeneratorFor(e => e.UniqueId, e => Guid.NewGuid())
              .GeneratorFor(e => e.TextColumn3, row => ETLString.GetIntRandom(1, 100).ToString())
              .GeneratorFor(r => r.TextColumn4, ETLString.GetToto)
              .AddToJob(job);

              TLogRow<TestSchema1> rowlog = new TLogRow<TestSchema1>();
              rowlog.ShowHeader(true);
              rowlog.SetInput(rowgen.OnOutput);
              rowlog.Enabled = false;

              TSortRow<TestSchema1, TestSchema1> sort = new TSortRow<TestSchema1, TestSchema1>();
              sort.AddOrderBy(s => s.TextColumn3,SortOrder.Ascending);
              sort.SetInput(rowlog.OnOutput);

              TOutputFileJson<TestSchema1, TestSchema2> jsonfile = new TOutputFileJson<TestSchema1, TestSchema2>();
              jsonfile.Filename = jsonFileName;
              jsonfile.DeleteFileIfExist = true;
              jsonfile.SetInput(sort.OnOutput);

              jsonfile.Transform(row => { row.TextColumn4 = row.TextColumn3 + " hacked"; });
              jsonfile.OnError.Subscribe(ex =>
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  Console.Error.Write("OUPUT JSON".PadLeft(10, '#').PadRight(10, '#'));
                  Console.Error.WriteLine(ex.Message);
                  Console.Error.WriteLine(ex.InnerException.Message);
                  Console.ResetColor();
              });

              TMap<TestSchema1, TestSchema3, TestSchema2,Guid> map = new TMap<TestSchema1, TestSchema3, TestSchema2,Guid>();
              map.Join(TMapJoin.LeftOuter);
              map.SetInput(rowgen.OnOutput);
              map.SetLookup(lookupgen.OnOutput);
              map.SetMapping(cfg => {
                  cfg.CreateMap<TestSchema1, TestSchema3>().ForMember(dest => dest.TextColumn5, opt => opt.MapFrom(src => src.TextColumn1 + " " + src.TextColumn2));
                  cfg.CreateMap<TestSchema2, TestSchema3>()
                  .ForMember(dest => dest.TextColumn5, opt => opt.MapFrom((src, dest) => src.TextColumn4 + " " + dest.TextColumn5))
                  .ForAllOtherMembers(m => m.Ignore());
              }
              );
              map.OnError.Subscribe(ex =>
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  Console.Error.WriteLine(ex.Message);
                  Console.ResetColor();
              });
              TLogRow<TestSchema3> rowlog2 = new TLogRow<TestSchema3>();
              rowlog2.ShowHeader(true);
              rowlog2.Mode(TLogRowMode.Basic);
              rowlog2.SetInput(map.OnOutput);
              rowlog2.Enabled = false;



              TInputFileJson<TestSchema2, TestSchema2> inputJson = new TInputFileJson<TestSchema2, TestSchema2>();
              inputJson.Filename = jsonFileName;
              inputJson.AddToJob(job);
              inputJson.OnError.Subscribe(ex =>
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  Console.Error.Write("INPUT JSON".PadLeft(10,'#').PadRight(10,'#'));
                  Console.Error.WriteLine(ex.Message);
                  Console.Error.WriteLine(ex.InnerException.Message);
                  Console.ResetColor();
              });

              TLogRow<TestSchema2> logInputJson = new TLogRow<TestSchema2>();
              logInputJson.SetInput(inputJson.OnOutput);



              TWaitForFile waitForFile = new TWaitForFile();
              waitForFile.Path = @"d:\";
              waitForFile.Filter = "test.json";
              waitForFile.Deleted.Subscribe(f => Console.WriteLine($"{f.EventArgs.FullPath} has been deleted"));
              waitForFile.AddToJob(job);




              await job.Start();

              job.Dispose();
          }*/

        /* static void TestAutoMap()
         {
             var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<TestSchema1, TestSchema3>().ForMember(dest => dest.TextColumn5, opt => opt.MapFrom(src => src.TextColumn1 + " " + src.TextColumn2));
                    cfg.CreateMap<TestSchema2, TestSchema3>()
                    .ForMember(dest => dest.TextColumn5, opt => opt.MapFrom((src, dest) => src.TextColumn4 + " " + dest.TextColumn5))
                    .ForAllOtherMembers(m => m.Ignore());
                });
             var mapper = config.CreateMapper();
             var t1 = new TestSchema1() { UniqueId = Guid.NewGuid(), TextColumn1 = "T1", TextColumn2 = "T2", TextColumn3 = "T3" };
             var t2 = new TestSchema2() { UniqueId = t1.UniqueId, TextColumn4 = "T4" };
             var t3 = mapper.Map<TestSchema3>(t1, t2);
         }*/

        /* static void TestInnerJoin()
         {
             List<TestSchema1> lt1 = new List<TestSchema1>() { new TestSchema1() { TextColumn1 = "T1", TextColumn2 = "T2", TextColumn3 = "T3" } , new TestSchema1() { TextColumn1 = "T100", TextColumn2 = "T200", TextColumn3 = "T300" } };
             List<TestSchema2> lt2 = new List<TestSchema2>() { new TestSchema2() { TextColumn4 = "T1"}, new TestSchema2() { TextColumn4 = "T10" } };

             var res = lt1.InnerJoin(lt2, l => l.TextColumn1, r => r.TextColumn4, (r, l) => (r, l)).ToList();
             var res1 = lt2.InnerJoin(lt1, l => l.TextColumn4, r => r.TextColumn1, (r, l) => (r, l)).ToList();
             var res2 = lt2.LeftOuterJoin(lt1, l => l.TextColumn4, r => r.TextColumn1, (r, l) => (r, l)).ToList();
         }*/

        /* static async Task TestListDirectory()
         {
              Job job = new Job();
             TFileList<FileDeleteSchema> liste = new TFileList<FileDeleteSchema>() { Path=@"x:\"};
             liste.OnError.Subscribe(e => Console.Error.WriteLine(e.Message+"\n"+e.InnerException.Message));

             TFileDelete<FileDeleteSchema, FileDeleteSchema> deletePipe = new TFileDelete<FileDeleteSchema, FileDeleteSchema>();
             deletePipe.Enabled = false;
             deletePipe.SetInput(liste.OnOutput);
             liste.AddToJob(job);

             await job.Start();
         }*/

        static void CalculAngle()
        {
            var offset = new Vector2(0, 0);
            var zero = Vector2.UnitX;
            var from = new Vector2(665.5, 26.4) + offset;
            var to = new Vector2(661.5, 22.4) + offset;
            var center = new Vector2(665.5, 22.4) + offset;
            var orientation = AngleDirection.CCW;
            Console.WriteLine(orientation.ToString());
            var o = Orient(NormalizeRadianToDegree(Vector2.AngleBetween(from - center, zero)), NormalizeRadianToDegree(Vector2.AngleBetween(to - center, zero)), orientation);
            Console.WriteLine($"Start:{o.Item1} - End:{o.Item2}");

            orientation = AngleDirection.CW;
            Console.WriteLine(orientation.ToString());
            o = Orient(NormalizeRadianToDegree(Vector2.AngleBetween(from - center, zero)), NormalizeRadianToDegree(Vector2.AngleBetween(to - center, zero)), orientation);
            Console.WriteLine($"Start:{o.Item1} - End:{o.Item2}");

            orientation = AngleDirection.CCW;
            from = new Vector2(4, 4);
            to = new Vector2(0, 0);
            center = new Vector2(0, 4);
            Console.WriteLine(orientation);
            o = Orient(NormalizeRadianToDegree(Vector2.AngleBetween(from - center, zero)), NormalizeRadianToDegree(Vector2.AngleBetween(to - center, zero)), orientation);
            Console.WriteLine($"Start:{o.Item1} - End:{o.Item2}");

            orientation = AngleDirection.CW;
            Console.WriteLine(orientation);
            o = Orient(NormalizeRadianToDegree(Vector2.AngleBetween(from - center, zero)), NormalizeRadianToDegree(Vector2.AngleBetween(to - center, zero)), orientation);
            Console.WriteLine($"Start:{o.Item1} - End:{o.Item2}");
        }

        static double NormalizeRadianToDegree(double value) => ((value * 180 / Math.PI) + 360) % 360.0;

        static (double, double) Orient(double start, double end, AngleDirection dir)
        => dir switch
        {
            AngleDirection.CCW => start == 0 ? (end, start) : (Math.Min(start, end), Math.Max(start, end)),
            _ => (start == 0) ? (start, end) : (Math.Max(start, end), Math.Min(start, end)),
        };

        static ((double, double), (double, double)) CalculateCenterX(Vector2 x, Vector2 y, double radius)
        {
            double k = (x.X - y.X) / (y.Y - x.Y);
            double h = (Math.Pow(y.X, 2) - Math.Pow(x.X, 2) + Math.Pow(y.Y, 2) - Math.Pow(x.Y, 2)) / (2 * (y.Y - x.Y));
            double m = Math.Pow(k, 2) + 1;
            double n = (-2 * x.X) + (2 * k * h) - (2 * k * x.Y);
            double p = Math.Pow(x.X, 2) + Math.Pow(x.Y, 2) - (2 * h * x.Y) + Math.Pow(h, 2) - Math.Pow(radius, 2);
            double x1 = (-n + Math.Sqrt(Math.Pow(n, 2) - (4 * m * p))) / (2 * m);//0.863803
            double x2 = (-n - Math.Sqrt(Math.Pow(n, 2) - (4 * m * p))) / (2 * m); //0.136196

            double y1 = k * x1 + h;//-0.606339
            double y2 = k * x2 + h;//0.606339
            return ((x1, y1), (x2, y2));
        }
    }
}
