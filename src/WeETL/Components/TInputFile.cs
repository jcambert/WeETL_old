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
        protected override Task InternalStart(CancellationTokenSource token)
        {
            FileReadLine fl = new FileReadLine(token) { Filename = Filename };
            if (Mode == InputFileMode.LineByLine)
                fl.Output.Select(s => new ContentSchema<string>() { Content = s }).Subscribe(OutputHandler, TokenSource.Token);
            else
                fl.Output.Aggregate("", (a, b) => $"{a}\n{b}").Select(s => new ContentSchema<string>() { Content = s }).Subscribe(OutputHandler, TokenSource.Token);


            return Task.CompletedTask;
        }
    }
}
