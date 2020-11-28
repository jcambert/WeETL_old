using System;
using System.IO;
using System.Reactive.Linq;
using WeETL.Utilities;
#if DEBUG
using System.Diagnostics;
#endif

namespace WeETL.Observables
{
    public class DirectoryFile : AbstractObservable<string>
    {
    
        public DirectoryFile(string path,string searchPattern="*.*",SearchOption searchOption=SearchOption.TopDirectoryOnly)
        {
            Check.NotNull(path, nameof(Path));
            Check.NotNull(searchPattern, nameof(SearchPattern));
            this.Path = path;
            this.SearchPattern = searchPattern;
            this.SearchOption = searchOption;
        }

        public string Path { get; }
        public string SearchPattern { get; }
        public SearchOption SearchOption { get; }

        protected override IObservable<string> CreateOutputObservable()
       => Observable.Defer(() =>
       {
#if DEBUG
           Debug.WriteLine($"Constructing {nameof(DirectoryFile)} stream");
#endif
           var files = Directory.EnumerateFiles(Path, SearchPattern, SearchOption);
           return files.ToObservable();


       });
    }
}
