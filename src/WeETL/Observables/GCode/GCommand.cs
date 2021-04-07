using WeETL.DependencyInjection;

namespace WeETL.Observables.GCode
{
    public interface IGCommand:INamed
    {
        public GCommandLine Command { get;  }
    }
    public abstract class GCommand : IGCommand
    {
        public GCommand()
        {
            this.Name = this.GetType().Name;
        }
        public GCommandLine Command { get; internal set; }

        public string Name { get; }
    }
}
