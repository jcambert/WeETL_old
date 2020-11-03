﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WeETL.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
             await TestJob();

           //TestInnerJoin();

           // Console.ReadKey();
            // Console.WriteLine(rowgen.Generate());
            // Console.WriteLine(rowgen.Generate());
        }

        static async Task TestJob() {
            Job job = new Job();
            job.OnCompleted.Subscribe(job => {
                Console.WriteLine($"Elapsed Time:{job.TimeElapsed.TotalSeconds}");
            });

            /* Console.WriteLine(ETLString.GetAsciiRandomString());
             Console.WriteLine(ETLString.GetAsciiRandomString());
             Console.WriteLine(ETLString.GetAsciiRandomString(7, StringRandomStyle.LowerCase));
             Console.WriteLine(ETLString.GetAsciiRandomString(8, StringRandomStyle.UpperCase));*/

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
            .GeneratorFor(r => r.TextColumn4, ETLString.GetToto)
            .AddToJob(job);

            TLogRow<TestSchema1> rowlog = new TLogRow<TestSchema1>();
            rowlog.ShowHeader(true);
            rowlog.SetInput(rowgen.OnOutput);

            TOutputFileJson<TestSchema1, TestSchema2> jsonfile = new TOutputFileJson<TestSchema1, TestSchema2>();
            jsonfile.SetInput(rowlog.OnOutput);
            jsonfile.Transform(row => { row.TextColumn4 = row.TextColumn4 + " hacked"; });

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
           
            TLogRow<TestSchema3> rowlog2 = new TLogRow<TestSchema3>();
            rowlog2.ShowHeader(true);
            rowlog2.Mode(TLogRowMode.Basic);
            rowlog2.SetInput(map.OnOutput);

            await job.Start();

            job.Dispose();
        }

        static void TestAutoMap()
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
        }

        static void TestInnerJoin()
        {
            List<TestSchema1> lt1 = new List<TestSchema1>() { new TestSchema1() { TextColumn1 = "T1", TextColumn2 = "T2", TextColumn3 = "T3" } , new TestSchema1() { TextColumn1 = "T100", TextColumn2 = "T200", TextColumn3 = "T300" } };
            List<TestSchema2> lt2 = new List<TestSchema2>() { new TestSchema2() { TextColumn4 = "T1"}, new TestSchema2() { TextColumn4 = "T10" } };

            var res = lt1.InnerJoin(lt2, l => l.TextColumn1, r => r.TextColumn4, (r, l) => (r, l)).ToList();
            var res1 = lt2.InnerJoin(lt1, l => l.TextColumn4, r => r.TextColumn1, (r, l) => (r, l)).ToList();
            var res2 = lt2.LeftOuterJoin(lt1, l => l.TextColumn4, r => r.TextColumn1, (r, l) => (r, l)).ToList();
        }
    }
}
