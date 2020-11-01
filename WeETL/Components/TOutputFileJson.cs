using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeETL
{
    public class TOutputFileJson<TSchema> : ETLComponent<TSchema>
        where TSchema : class, new()
    {
        List<TSchema> _buffer = new List<TSchema>();
        string filename = @"d:\test.json";
        protected override void InternalOnRow(int index, TSchema row)
        {
            base.InternalOnRow(index, row);
            _buffer.Add(row);
        }
        protected override void InternalOnCompleted()
        {
            base.InternalOnCompleted();
            //Console.WriteLine(JsonSerializer.Serialize<List<TSchema>>(_buffer));
            Task.Run(async () =>
            {

                using (FileStream fs = File.Create(filename))
                {
                    await JsonSerializer.SerializeAsync(fs, _buffer);
                    Console.WriteLine("ENDED");
                }
            }).Wait();

        }
    }
}
