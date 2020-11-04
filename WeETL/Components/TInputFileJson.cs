using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL
{
    public class TInputFileJson<TInputSchema, TOutputSchema> : ETLStartableComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class//, new()
        where TOutputSchema : class, new()
    {
        private List<TInputSchema> inputs;


        public TInputFileJson():base()
        {
          
        }
     
        protected override void InternalStart()
        {
            
                    var jsonString = File.ReadAllText(Filename);
                    inputs = JsonSerializer.Deserialize<List<TInputSchema>>(jsonString);
                    foreach (var row in inputs)
                    {
                        
                            var transformed = InternalInputTransform(row);
                            InternalSendOutput(transformed);
                        
                    }
                
        }
        

        public string  Filename { get; set; }
    }
}
