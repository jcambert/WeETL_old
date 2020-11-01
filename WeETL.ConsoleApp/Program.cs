using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WeETL.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            Console.WriteLine("Hello World!");

            Console.WriteLine(ETLString.GetAsciiRandomString());
            Console.WriteLine(ETLString.GetAsciiRandomString());
            Console.WriteLine(ETLString.GetAsciiRandomString(7, StringRandomStyle.LowerCase));
            Console.WriteLine(ETLString.GetAsciiRandomString(8, StringRandomStyle.UpperCase));

            TRowGenerator<TestSchema> rowgen = new TRowGenerator<TestSchema>();
            rowgen.NumberOfRowToGenerate = 100;
            rowgen
            .Strict(true)
            .GeneratorFor(e => e.UniqueId, e => Guid.NewGuid())
            .GeneratorFor(e => e.TextColumn1, ETLString.GetToto)
            .GeneratorFor(e => e.TextColumn2, e => ETLString.GetAsciiRandomString(8, StringRandomStyle.UpperCase))
            .GeneratorFor(e => e.TextColumn3, row => ETLString.GetIntRandom(1, 100).ToString());
            //rowgen.OnRow.Subscribe(row => Console.WriteLine($"{row.Item1.ToString().PadRight(3)}|{row.Item2}"),ex=>Console.Error.WriteLine(ex.Message),()=>Console.WriteLine("Completed"));

            TLogRow<TestSchema> rowlog = new TLogRow<TestSchema>();
            rowlog.ShowHeader(true);
            rowlog.SetInput(rowgen);

            TOutputFileJson<TestSchema> jsonfile = new TOutputFileJson<TestSchema>();
            jsonfile.SetInput(rowlog);

            await rowgen.Start();
            watcher.Stop();
            Console.WriteLine($"Elapsed Time:{watcher.Elapsed.TotalSeconds}");
           // Console.WriteLine(rowgen.Generate());
           // Console.WriteLine(rowgen.Generate());
        }
    }
}
