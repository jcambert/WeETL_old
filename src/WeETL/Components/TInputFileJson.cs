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
        private TInputSchema row;


        public TInputFileJson() : base()
        {

        }

        protected override Task InternalStart(CancellationTokenSource token)
        {
            int counter = 0;
            var jsonString = File.ReadAllText(Filename);
            row = JsonSerializer.Deserialize<TInputSchema>(jsonString);


            var transformed = InternalInputTransform(row);
            InternalSendOutput(counter++, transformed);


            return Task.CompletedTask;
        }


        public string Filename { get; set; }
    }
}
