using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Schemas;
#if DEBUG
using System;
using System.Diagnostics;
#endif

namespace WeETL.Components
{
    public class TFileList<TOutputSchema> : ETLStartableComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, IFilenameSchema, new()
    {
        public TFileList():base()
        {
#if DEBUG
            OnCompleted.Subscribe(x => {
                if (Debugger.IsAttached)
                    Debugger.Break();
            });
#endif
        }
        public string Path { get; set; }
        public string SearchPattern { get; set; } = "*.*";

        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;

        protected override Task InternalStart(CancellationToken token)
        {
            DirectoryFile dfo = new DirectoryFile(Path, SearchPattern, SearchOption);
            //System.ObservableExtensions.Subscribe( dfo.Output.Select(s => new TOutputSchema() { Filename = s }),token);
            dfo.Output.Select(s => {
                return new TOutputSchema() { Filename = s }; 
            }).Subscribe(OutputHandler,token);

            return Task.CompletedTask;
        }
    }
}
