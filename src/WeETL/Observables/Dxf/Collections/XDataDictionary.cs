using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WeETL.Utilities;
using WeETL.Observables.Dxf.Tables;
namespace WeETL.Observables.Dxf.Collections
{
    public sealed class XDataDictionary : IDictionary<string, XData>
    {
        #region private variables
        private readonly Dictionary<string, XData> _inner = new Dictionary<string, XData>(StringComparer.OrdinalIgnoreCase);
        private IDisposable _onNamedChanged;
        private readonly ISubject<ObservableCollectionEventArgs<ApplicationRegistry>> _onAddAppReg = new Subject<ObservableCollectionEventArgs<ApplicationRegistry>>();
        private readonly ISubject<ObservableCollectionEventArgs<ApplicationRegistry>> _onRemoveAppReg = new Subject<ObservableCollectionEventArgs<ApplicationRegistry>>();
        #endregion
        #region ctor
        public XDataDictionary()
        {

        }
        public XDataDictionary(IEnumerable<XData> items)
        {
            AddRange(items);
        }
        #endregion
        #region public properties
        public IObservable<ObservableCollectionEventArgs<ApplicationRegistry>> OnAddAppReg => _onAddAppReg.AsObservable();
        public IObservable<ObservableCollectionEventArgs<ApplicationRegistry>> OnRemoveAppReg => _onRemoveAppReg.AsObservable();
        public XData this[string appId] { 
            get => _inner[appId];
            set {

                Check.NotNull(value, nameof(value));
                if (!string.Equals(value.ApplicationRegistry.Name, appId, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException(string.Format("The extended data application registry name {0} must be equal to the specified appId {1}.", value.ApplicationRegistry.Name, appId));

                _inner[appId] = value;
            }
        }

        public ICollection<string> Keys => _inner.Keys;

        public ICollection<XData> Values => _inner.Values;

        public int Count => _inner.Count;

        public bool IsReadOnly => false;
        #endregion

        #region public methods
        public void Add(XData item)
        {
            Check.NotNull(item, nameof(item));
            XData xdata;
            if (_inner.TryGetValue(item.ApplicationRegistry.Name, out xdata))
            {
                xdata.Datas.AddRange(item.Datas);
            }
            else
            {
                _inner.Add(item.ApplicationRegistry.Name, item);
               _onNamedChanged= item.ApplicationRegistry.OnNamedChanged.Subscribe(e=>
                {
                    XData xdata = _inner[e.OldValue];
                    _inner.Remove(e.OldValue);
                    _inner.Add(e.NewValue, xdata);
                });
                _onAddAppReg.OnNext(new ObservableCollectionEventArgs<ApplicationRegistry>(item.ApplicationRegistry));
                
            }
        }
        public void Add(string key, XData value) => Add(value);

        public void Add(KeyValuePair<string, XData> item) => Add(item.Value);
        

        /// <summary>
        /// Adds a list of <see cref="XData">extended data</see> to the current dictionary.
        /// </summary>
        /// <param name="items">The list of <see cref="XData">extended data</see> to add.</param>
        public void AddRange(IEnumerable<XData> items)
        {
            Check.NotNull(items, nameof(items));

            foreach (XData data in items)
            {
                this.Add(data);
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(KeyValuePair<string, XData> item)=> _inner.Contains(item);

        public bool ContainsKey(string key) => _inner.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, XData>[] array, int arrayIndex) => Array.Copy(_inner.ToArray(), array, Count);



        public IEnumerator<KeyValuePair<string, XData>> GetEnumerator() => _inner.GetEnumerator();

        public bool Remove(string appId)
        {
            if (!ContainsKey(appId))
                return false;
            XData xdata = this[appId];
            _onNamedChanged?.Dispose();
            _onNamedChanged = null;
            _inner.Remove(appId);
            _onRemoveAppReg.OnNext(new ObservableCollectionEventArgs<ApplicationRegistry>(xdata.ApplicationRegistry));
            return true;
        }

        public bool Remove(KeyValuePair<string, XData> item) => Remove(item.Key);

        public bool TryGetValue(string appId, [MaybeNullWhen(false)] out XData value) => _inner.TryGetValue(appId, out value);

        IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
        #endregion
    }
}
