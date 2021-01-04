using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.IO;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.IO
{
    [Flags]
    public enum DxfSection
    {
        None = 0,
        Header = 1,
        Classes = 2,
        Tables = 4,
        Blocks = 8,
        Entities = 16,
        Objects = 32,
        Thumbnails = 64,
        All = 127

    }
    public interface IDxfReader:IFileReader<IDxfDocument>
    {
        /* void Load(string filename, DxfSection DxfSection = DxfSection.All);*/
        public DxfSection DxfSection { get; set; }
    }




    public class DxfReader : IDxfReader
    {
        public const string EndSection = "ENDSEC";
        ISubject<IDxfDocument> _onLoaded = new Subject<IDxfDocument>();
        DxfSection _headerLoaded=DxfSection.None, _classesLoaded=DxfSection.None, _tablesLoaded=DxfSection.None, _blocksLoaded=DxfSection.None , _entitiesLoaded=DxfSection.None, _objectsLoaded=DxfSection.None, _thumbnailLoaded=DxfSection.None;
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

        public DxfSection DxfSection { get; set; } = DxfSection.All;

        
        private void SendLoaded()
        {
            if (DxfSection == (_headerLoaded | _classesLoaded | _tablesLoaded | _blocksLoaded | _entitiesLoaded | _objectsLoaded | _thumbnailLoaded))
                _onLoaded.OnNext(Document);
        }
        public void Load(string filename)
        {
            var section= DxfSection;
            ISubject<string> lines = new ReplaySubject<string>();
            LineReader.Filename = filename;
            LineReader.Output.Subscribe(lines);

            #region Read Header
            if (section == DxfSection.Header)
                LoadSection<DxfHeaderTypeAttribute>(lines, DxfHeaderCode.HeaderSection, () => _headerLoaded = DxfSection.Header);
            #endregion
            #region Read CLASSES
            if (section == DxfSection.Classes)
                LoadSection<DxfClassTypeAttribute>(lines, DxfClasseCode.ClasseSection, () => _classesLoaded = DxfSection.Classes);
            #endregion
            #region Read TABLES
            if (section == DxfSection.Tables)
                LoadSection<DxfTableTypeAttribute>(lines, DxfTableCode.TableSection, () => _tablesLoaded = DxfSection.Tables);
            #endregion
            #region Read BLOCKS
            if (section == DxfSection.Blocks)
                LoadSection<DxfBlockTypeAttribute>(lines, DxfBlockCode.BlockSection, () => _blocksLoaded = DxfSection.Blocks);
            #endregion
            #region Read OBJECTS
            if (section == DxfSection.Objects)
                LoadSection<DxfObjectTypeAttribute>(lines, DxfObjectCode.ObjectsSection, () => _objectsLoaded = DxfSection.Objects);
            #endregion
            #region Read ENTITIES
            if (section == DxfSection.Entities)
                LoadSection<DxfEntityTypeAttribute>(lines, DxfEntityCode.EntitiesSection, () => _entitiesLoaded = DxfSection.Entities);
            #endregion
            if (section == DxfSection.Thumbnails)
                #region Read THUMNAILIMAGE
                LoadSection<DxfThumbnailTypeAttribute>(lines, DxfThumbnailCode.ThumbnailImageSection, () => _thumbnailLoaded = DxfSection.Thumbnails);
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
