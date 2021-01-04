using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using WeETL.IO;
using WeETL.Observables.Dxf.Collections;
using WeETL.Observables.Dxf.Entities;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public interface IDxfDocument : IDxfObject,IDocument
    {
        IDxfHeader Header { get; }
        IDxfTables Tables { get; }
        ObservableCollection<EntityObject> Entities { get; }


        void AddEntity(EntityObject entity, bool addToBlock = false);
    }
    public class DxfDocument : DxfObject, IDxfDocument
    {
        #region private variables
        private long _numHandles;
        internal readonly ObservableDictionary<string, DxfObject> AddedObjects = new ObservableDictionary<string, DxfObject>();
        // internal readonly List<EntityObject> Entities = new List<EntityObject>();
        #endregion
        #region ctor
        public DxfDocument(IDxfHeader header, IDxfTables tables) : base()
        {

            this.Header = Check.NotNull(header, nameof(header));
            this.Tables = Check.NotNull(tables, nameof(tables));
            Entities= new ObservableCollection<EntityObject>();
            Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Entities, "CollectionChanged")
                .Select(e => e.EventArgs)
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .SelectMany(e=>e.NewItems.Cast<EntityObject>())
                .Subscribe(entity => {

                    Check.NotNull(entity,nameof(entity));
                    this.SetOwner(entity, ++NumHandles);
                });
            this.Owner = null;  // NO PARENT BECAUSE ROOT
            this.Handle = "0";  // HANDLE BASE = 0
            this.SetOwner(tables,++NumHandles);
            this.SetOwner(tables.TextStyles,++NumHandles);
            (tables as DxfObject) .Handle = (++NumHandles).ToString();
            tables.TextStyles.OnAdd.Where(h => string.IsNullOrEmpty(h.Handle)).Subscribe(style => {
                tables.SetOwner(style,++NumHandles);
            });

        }

       
        #endregion
        #region public propreties
        public IDxfHeader Header { get; }
        public IDxfTables Tables { get; }
        public override string CodeName => DxfDocumentCode.Document;
        public ObservableCollection<EntityObject> Entities { get; } 

        #endregion
        #region internal properties

        /// <summary>
        /// Gets or sets the number of handles generated, this value is saved as an hexadecimal in the drawing variables HandleSeed property.
        /// </summary>
        internal long NumHandles
        {
            get { return _numHandles; }
            set
            {
                Header.HandleSeed = value.ToString("X");
                _numHandles = value;
            }
        }

        #endregion

        #region public Methods
        public void AddEntity(EntityObject entity, bool addToBlock = false)
        {
           /* Check.NotNull(entity, nameof(entity));
            if (!addToBlock)
            {
                Entities.Add(entity);
                this.SetOwner(entity, ++NumHandles);
            }*/
        }
        #endregion

        #region overrides
        public override string ToString()
        {
            return Header.ToString();
        }


        #endregion
    }
}
