using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Schemas;

namespace WeETL
{
    public enum InputFileMode
    {
        LineByLine,
        Full
    }
    public class TInputFile : ETLStartableComponent<string, ContentSchema<string>>
    {
        public string Filename { get; set; }
        public InputFileMode Mode { get; set; } = InputFileMode.LineByLine;
        protected override Task InternalStart(CancellationToken token)
        {
            FileReadLine fl = new FileReadLine() { Filename = Filename };
            if (Mode == InputFileMode.LineByLine)
                fl.Output.Select(s => new ContentSchema<string>() { Content = s }).Subscribe(OutputHandler, token);
            else
                fl.Output.Aggregate("", (a, b) => $"{a}\n{b}").Select(s => new ContentSchema<string>() { Content = s }).Subscribe(OutputHandler, token);


            return Task.CompletedTask;
        }
    }
}
