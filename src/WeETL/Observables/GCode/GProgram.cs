using System.Collections.Generic;

namespace WeETL.Observables.GCode
{
    public interface IGProgram
    {
        List<GCommand> Commands { get; }
    }
    public class GProgram : IGProgram
    {
        public List<GCommand> Commands { get; } = new List<GCommand>();
    }

}
