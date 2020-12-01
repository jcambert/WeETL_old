using System;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;

namespace WeETL
{
    public class TInputFileJson<TInputSchema, TOutputSchema> : ETLStartableComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class
        where TOutputSchema : class, new()
    {
        //private TInputSchema row;


        public TInputFileJson() : base()
        {

        }

        protected override Task InternalStart(CancellationToken token)
        {
            FileReadFull fl = new FileReadFull() { Filename = Filename };
             fl.Output
                .Select(s => InternalInputTransform(JsonSerializer.Deserialize<TInputSchema>(s)))
                .Subscribe(OutputHandler, token);

        /*    int counter = 0;
            var jsonString = File.ReadAllText(Filename);
            row = JsonSerializer.Deserialize<TInputSchema>(jsonString);


            var transformed = InternalInputTransform(row);
            InternalSendOutput(counter++, transformed);*/


            return Task.CompletedTask;
        }


        public string Filename { get; set; }
    }
}
