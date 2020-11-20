using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Schemas;

namespace WeETL.Components
{
    public class TFileList<TOutputSchema> : ETLStartableComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, IFilenameSchema, new()
    {
        public string Path { get; set; }
        public string SearchPattern { get; set; } = "*.*";

        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;

        protected override Task InternalStart()
        {
           
           var files= Directory.EnumerateFiles(Path, SearchPattern, SearchOption).Select(s => new TOutputSchema() {Filename=s });
            files.ToList().ForEach(f => OutputHandler.OnNext(f));
            return Task.CompletedTask;
        }
    }
}
