using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.DependencyInjection;

namespace WeETL.Observables.BySpeed
{
    public interface IGCommand:INamed
    {
        public GCommandStructure Command { get;  }
    }
    public abstract class GCommand : IGCommand
    {
        public GCommand()
        {
            this.Name = this.GetType().Name;
        }
        public GCommandStructure Command { get; internal set; }

        public string Name { get; }
    }
}
