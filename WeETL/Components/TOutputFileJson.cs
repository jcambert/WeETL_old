using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeETL
{
    public class TOutputFileJson<TInputSchema,TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class, new()
        where TOutputSchema : class, new()
    {
        List<TOutputSchema> _buffer = new List<TOutputSchema>();
        string filename = @"d:\test.json";
        protected override void InternalOnRowBeforeTransform(int index, TInputSchema row)
        {
            base.InternalOnRowBeforeTransform(index, row);
        }
        protected override void InternalOnRowAfterTransform(int index, TOutputSchema row)
        {
            base.InternalOnRowAfterTransform(index, row);
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
                }
            }).Wait();

        }

        
    }
}
