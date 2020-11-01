using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WeETL.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
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
            .GeneratorFor(e => e.TextColumn3, row => ETLString.GetIntRandom(1, 100).ToString());
            //rowgen.OnRow.Subscribe(row => Console.WriteLine($"{row.Item1.ToString().PadRight(3)}|{row.Item2}"),ex=>Console.Error.WriteLine(ex.Message),()=>Console.WriteLine("Completed"));
            job.Add(rowgen);

            TLogRow<TestSchema1> rowlog = new TLogRow<TestSchema1>();
            rowlog.ShowHeader(true);
            rowlog.SetInput(rowgen.OnOutput);

            TOutputFileJson<TestSchema1, TestSchema2> jsonfile = new TOutputFileJson<TestSchema1, TestSchema2>();
            jsonfile.SetInput(rowlog.OnOutput);
            jsonfile.Transform(row => { row.TextColumn3 = row.TextColumn3 + " hacked"; });
            TLogRow<TestSchema2> rowlog2 = new TLogRow<TestSchema2>();
            rowlog2.ShowHeader(true);
            rowlog2.Mode(TLogRowMode.Basic);
            rowlog2.SetInput(jsonfile.OnOutput);

            

            await job.Start();

            job.Dispose();
           // Console.WriteLine(rowgen.Generate());
           // Console.WriteLine(rowgen.Generate());
        }
    }
}
