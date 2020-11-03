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
        string filename = @"e:\test.json";
        protected override void InternalOnInputBeforeTransform(int index, TInputSchema row)
        {
            base.InternalOnInputBeforeTransform(index, row);
        }
        protected override void InternalOnInputAfterTransform(int index, TOutputSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            _buffer.Add(row);
          
        }

        protected override void InternalOnInputCompleted()
        {
            base.InternalOnInputCompleted();
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
