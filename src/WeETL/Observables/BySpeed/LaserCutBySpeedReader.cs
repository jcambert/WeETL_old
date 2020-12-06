using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observers;
using WeETL.Utilities;

namespace WeETL.Observables.BySpeed
{
    public interface ILaserCutBySpeedReader
    {
        Func<string, GCommandStructure> CommandParser { get; }
        IObservable<GCommandStructure> Load(string filename);
    }
    public class LaserCutBySpeedReader : ILaserCutBySpeedReader
    {
        public LaserCutBySpeedReader(IGProgramm program, IFileReadLine lineReader)
        {
            Check.NotNull(program, nameof(IGProgramm));
            Check.NotNull(lineReader, nameof(IFileReadLine));
            this.Program = program;
            this.LineReader = lineReader;
        }
        public Func<string, GCommandStructure> CommandParser => Extensions.CommandParser;

        public IGProgramm Program { get; }
        protected IFileReadLine LineReader { get; }

        public IObservable<GCommandStructure> Load(string filename)
        {
            LineReader.Filename = filename;
            return LineReader.Output.Select(CommandParser).Where(cmdLine => cmdLine.Line != null);
        }
    }
}
