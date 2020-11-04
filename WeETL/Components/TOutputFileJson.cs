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

        public string Filename { get; set; }
        public bool DeleteFileIfExist { get; set; } = true;

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
            Task.Run(async () =>
            {
                try
                {
                    if(DeleteFileIfExist)
                        File.Delete(Filename);
                    using (FileStream fs = File.Create(Filename))
                    {
                        await JsonSerializer.SerializeAsync(fs, _buffer);
                    }
                }catch(Exception e)
                {
                    Error.OnNext(new ConnectorException( "An error occurs while reading json file. See InnerException", e));
                }
            }).Wait();

        }

        
    }
}
