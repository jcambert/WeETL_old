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




    public class DxfReader : FileLineReader<IDxfDocument>, IDxfReader
    {
        public const string EndSection = "ENDSEC";
        ISubject<IDxfDocument> _onLoaded = new Subject<IDxfDocument>();
        DxfSection _headerLoaded=DxfSection.None, _classesLoaded=DxfSection.None, _tablesLoaded=DxfSection.None, _blocksLoaded=DxfSection.None , _entitiesLoaded=DxfSection.None, _objectsLoaded=DxfSection.None, _thumbnailLoaded=DxfSection.None;
        public DxfReader(IFileReadLine lineReader, IDxfDocument document, IReaderFactory readerFactory)
        :base(document,lineReader)
        {
            Check.NotNull(readerFactory, nameof(IServiceProvider));
            ReaderFactory = readerFactory;

            
        }
        public IReaderFactory ReaderFactory { get; }


        public DxfSection DxfSection { get; set; } = DxfSection.All;

        protected override void SendLoaded(bool condition=true)
        {
            if (condition && DxfSection == (_headerLoaded | _classesLoaded | _tablesLoaded | _blocksLoaded | _entitiesLoaded | _objectsLoaded | _thumbnailLoaded))
                _onLoaded.OnNext(Document);
            
        }
       
        /*public void Load(string filename)
        {
            ISubject<string> lines = new ReplaySubject<string>();
            LineReader.Filename = filename;
            LineReader.Output.Subscribe(lines);

        }*/

        private void ReadDxf()
        {
            var section= DxfSection;

            #region Read Header
            if (section == DxfSection.Header)
                LoadSection<DxfHeaderTypeAttribute>(Lines, DxfHeaderCode.HeaderSection, () => _headerLoaded = DxfSection.Header);
            #endregion
            #region Read CLASSES
            if (section == DxfSection.Classes)
                LoadSection<DxfClassTypeAttribute>(Lines, DxfClasseCode.ClasseSection, () => _classesLoaded = DxfSection.Classes);
            #endregion
            #region Read TABLES
            if (section == DxfSection.Tables)
                LoadSection<DxfTableTypeAttribute>(Lines, DxfTableCode.TableSection, () => _tablesLoaded = DxfSection.Tables);
            #endregion
            #region Read BLOCKS
            if (section == DxfSection.Blocks)
                LoadSection<DxfBlockTypeAttribute>(Lines, DxfBlockCode.BlockSection, () => _blocksLoaded = DxfSection.Blocks);
            #endregion
            #region Read OBJECTS
            if (section == DxfSection.Objects)
                LoadSection<DxfObjectTypeAttribute>(Lines, DxfObjectCode.ObjectsSection, () => _objectsLoaded = DxfSection.Objects);
            #endregion
            #region Read ENTITIES
            if (section == DxfSection.Entities)
                LoadSection<DxfEntityTypeAttribute>(Lines, DxfEntityCode.EntitiesSection, () => _entitiesLoaded = DxfSection.Entities);
            #endregion
            if (section == DxfSection.Thumbnails)
                #region Read THUMNAILIMAGE
                LoadSection<DxfThumbnailTypeAttribute>(Lines, DxfThumbnailCode.ThumbnailImageSection, () => _thumbnailLoaded = DxfSection.Thumbnails);
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
