using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.Observables.Dxf.IO;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public interface IDxfReader
    {
        void Load(string filename);
        IDxfDocument Document { get; set; }

        IObservable<IDxfDocument> OnLoaded { get; }
    }




    public class DxfReader : IDxfReader
    {
        public const string EndSection = "ENDSEC";
        ISubject<IDxfDocument> _onLoaded = new Subject<IDxfDocument>();
        bool _headerLoaded, _classesLoaded, _tablesLoaded, _blocksLoaded, _entitiesLoaded, _objectsLoaded, _thumbnailLoaded;
        public DxfReader(IFileReadLine lineReader, IDxfDocument document, IReaderFactory readerFactory)
        {
            Check.NotNull(lineReader, nameof(IFileReadLine));
            Check.NotNull(document, nameof(IDxfDocument));
            Check.NotNull(readerFactory, nameof(IServiceProvider));
            this.LineReader = lineReader;
            this.Document = document;
            ReaderFactory = readerFactory;
        }
        protected IFileReadLine LineReader { get; }
        public IDxfDocument Document { get; set; }
        public IReaderFactory ReaderFactory { get; }

        public IObservable<IDxfDocument> OnLoaded => _onLoaded.AsObservable();

        private void SendLoaded()
        {
            if (_headerLoaded && _classesLoaded && _tablesLoaded && _blocksLoaded && _entitiesLoaded && _objectsLoaded && _thumbnailLoaded)
                _onLoaded.OnNext(Document);
        }
        public void Load(string filename)
        {
            ISubject<string> lines = new ReplaySubject<string>();
            LineReader.Filename = filename;
            LineReader.Output.Subscribe(lines);

            #region Read Header
            LoadSection<DxfHeaderTypeAttribute>(lines, DxfHeaderCode.HeaderSection, () => _headerLoaded = true);
            #endregion
            #region Read CLASSES
            LoadSection<DxfClassTypeAttribute>(lines, DxfClasseCode.ClasseSection, () => _classesLoaded = true);
            #endregion
            #region Read TABLES
            LoadSection<DxfTableTypeAttribute>(lines, DxfTableCode.TableSection, () => _tablesLoaded = true);
            #endregion
            #region Read BLOCKS
            LoadSection<DxfBlockTypeAttribute>(lines, DxfBlockCode.BlockSection, () => _blocksLoaded = true);
            #endregion
            #region Read OBJECTS
            LoadSection<DxfObjectTypeAttribute>(lines, DxfObjectCode.ObjectsSection, () => _objectsLoaded = true);
            #endregion
            #region Read ENTITIES
            LoadSection<DxfEntityTypeAttribute>(lines, DxfEntityCode.EntitiesSection, () => _entitiesLoaded = true);
            #endregion
            #region Read THUMNAILIMAGE
            LoadSection<DxfThumbnailTypeAttribute>(lines, DxfThumbnailCode.ThumbnailImageSection, () => _thumbnailLoaded = true);
            #endregion
        }

        private void LoadSection<TTYpe>(IObservable<string> lines, string section, Action onCompleted, Action<IReader> onSubscribe = null)
            where TTYpe : DxfBaseTypeAttribute
        {
            var obs = lines.SkipWhile(s => s != section).TakeWhile(s => s != EndSection);
            obs.Scan(("", ""), (acc, curent) => (acc.Item2, curent.Trim()))
                .Skip(2)
                .Where((e, i) => i % 2 == 0)
                .Aggregate(ReaderFactory.CreateReaderSection(section, Document),
               (buffer, value) =>
               {
                   if (Int32.TryParse(value.Item1, out var code))
                       buffer.Read<TTYpe>((code, value.Item2));
                   else
                       throw new Exception("Malformed DXF");
                   return buffer;
               })
                .Subscribe(l =>
                {
                    onSubscribe?.Invoke(l);
                }, () =>
                {
                    onCompleted();
                    SendLoaded();
                });
        }

    }
}
