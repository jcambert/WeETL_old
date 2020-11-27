using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Schemas;

namespace WeETL.Components
{
    public class TFileList<TOutputSchema> : ETLStartableComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, IFilenameSchema, new()
    {
        public string Path { get; set; }
        public string SearchPattern { get; set; } = "*.*";

        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;

        protected override Task InternalStart(CancellationTokenSource tokenSource)
        {
            DirectoryFileObservable dfo = new DirectoryFileObservable(Path, SearchPattern, SearchOption);
            dfo.Output.Select(s => new TOutputSchema() { Filename = s }).Subscribe(OutputHandler);

            return Task.CompletedTask;
        }
    }
}
