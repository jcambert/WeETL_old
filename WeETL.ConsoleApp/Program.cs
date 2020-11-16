using Nest;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WeETL.Components;
using WeEFLastic.Extensions.DependencyInjection;
namespace WeETL.ConsoleApp
{
    class Program
    {
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        static async Task Main(string[] args)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            var dt = DateTime.UnixEpoch.AddMilliseconds(1605222000000);
            await ReadOfflineProgrammeTurf();
            // await TestJob();

            //await TestListDirectory();
            //TestInnerJoin();

            // Console.ReadKey();
            // Console.WriteLine(rowgen.Generate());
            // Console.WriteLine(rowgen.Generate());
        }
        static async Task ReadOfflineProgrammeTurf()
        {
            var ctx = new ETLContext();
            ctx.ConfigureService(cfg => cfg.AddEntityFrameworkElastic());
            var job = ctx.CreateJob();
            var fileInput = ctx.GetService<TInputFile>();
            fileInput.Filename= Path.Combine(ctx.ExecutionPath, "14112020.json");
            fileInput.AddToJob(job);

            var strToJson = ctx.GetService<TStringToJson<ProgrammeSchema>>();
            strToJson.AddInput(fileInput.OnOutput);

            strToJson.OnOutput.Subscribe(p => {
                Console.WriteLine(p.Programme.Date.ToString());
            
            });
            strToJson.OnError.Subscribe(ex => Console.Error.WriteLine(ex.InnerException.Message));
            await job.Start();
        }
        static async Task ReadOnlineProgrammeTurf()
        {
            var date = DateTime.Now.Subtract(TimeSpan.FromDays(1)).ToString("ddMMyyyy");
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultMappingFor<ProgrammeSchema>(s => s.IndexName("turf_programme"));
            var client = new ElasticClient(settings);
            //await client.(new DeleteIndexRequest( "turf_programme"));
            var projectSearchResponse = await client.SearchAsync<ProgrammeSchema>();
            var ctx = new ETLContext();
            var job = ctx.CreateJob();
            
            var restProgramme = ctx.GetService<TRest<ProgrammeSchema>>();
            restProgramme.RequestUri = $"https://online.turfinfo.api.pmu.fr/rest/client/1/programme/{date}?meteo=true&specialisation=INTERNET";
            restProgramme.Mode = TRestMode.Get;
            restProgramme.AddToJob(job);
            ProgrammeSchema programme = null;
            restProgramme.OnOutput.Subscribe(item =>
            {
                Console.WriteLine("receiving programme");
                programme = item;
                var row= item;
                client.IndexAsync(row, idx => idx.Index("turf_programme"));
                foreach (var reunion in row.Programme.Reunions)
                {
                    var r = reunion.NumeroOfficiel;
                    foreach (var course in reunion.Courses)
                    {
                        var c = course.Numero;
                        var restParticipant = ctx.GetService<TRest<ListeParticipants>>();
                        restParticipant.RequestUri = $"https://online.turfinfo.api.pmu.fr/rest/client/1/programme/{date}/R{r}/C{c}/participants?specialisation=INTERNET";
                        restParticipant.Mode = TRestMode.Get;
                        restParticipant.AddToJob(job);
                        
                        restParticipant.OnOutput.Subscribe(ps =>
                        {
                            foreach (var p in ps.Participants)
                            {
                            Console.WriteLine(p.Nom);

                            }
                            course.Participants = ps.Participants;
                        });
                        restParticipant.OnError.Subscribe(ex => Console.Error.WriteLine(ex.InnerException));
                    }
                }
                //return Task.CompletedTask;
            });
            restProgramme.OnCompleted.Subscribe(j => {
                Console.WriteLine("programme received");
            });
            restProgramme.OnError.Subscribe(ex =>
            {
                Console.Error.WriteLine(ex.InnerException.Message);
            });
            job.OnCompleted.Subscribe(job =>
            {
                Console.WriteLine("Job is completed");
            });
           

            await job.Start();
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
    }
}
