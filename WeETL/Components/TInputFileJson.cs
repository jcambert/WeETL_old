using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;

namespace WeETL
{
    public class TInputFileJson<TInputSchema, TOutputSchema> : ETLStartableComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class//, new()
        where TOutputSchema : class, new()
    {
        private List<TInputSchema> inputs;


        public TInputFileJson() : base()
        {

        }

        protected override Task InternalStart()
        {
            int counter = 0;
            var jsonString = File.ReadAllText(Filename);
            inputs = JsonSerializer.Deserialize<List<TInputSchema>>(jsonString);
            foreach (var row in inputs)
            {

                var transformed = InternalInputTransform(row);
                InternalSendOutput(counter++,transformed);

            }
            return Task.CompletedTask;
        }


        public string Filename { get; set; }
    }
}
