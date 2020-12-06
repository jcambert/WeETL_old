using System.Collections.Generic;

namespace WeETL.Observables.BySpeed
{
    public interface IGProgramm
    {
        List<GCommand> Commands { get; }
    }
    public class GProgram : IGProgramm
    {
        public List<GCommand> Commands { get; } = new List<GCommand>();
    }

}
