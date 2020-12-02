using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Schemas;
using System;


namespace WeETL.Components
{
    public class TFileList<TOutputSchema> : ETLStartableComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, IFilenameSchema, new()
    {
        public TFileList():base()
        {

        }
        public string Path { get; set; }
        public string SearchPattern { get; set; } = "*.*";

        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;

        protected override Task InternalStart(CancellationToken token)
        {
            DirectoryFile dfo = new DirectoryFile(Path, SearchPattern, SearchOption);
            dfo.Output.Select(s => {
                return new TOutputSchema() { Filename = s }; 
            }).Subscribe(OutputHandler,token);

            return Task.CompletedTask;
        }
    }
}
