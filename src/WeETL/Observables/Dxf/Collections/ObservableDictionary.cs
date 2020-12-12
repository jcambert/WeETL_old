using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf.Collections
{
    public sealed class ObservableDictionary<TKey, TValue> :
        IDictionary<TKey, TValue>
    {
        #region private fields

        private readonly Dictionary<TKey, TValue> _inner;
        private readonly ISubject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> _onBeforeAdd = new Subject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>>();
        private readonly ISubject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> _onAfterAdd = new Subject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>>();
        private readonly ISubject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> _onBeforeRemove = new Subject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>>();
        private readonly ISubject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> _onAfterRemove = new Subject<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>>();
        #endregion

        #region ctor
        public ObservableDictionary()
        {
            _inner = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(int capacity)
        {
            _inner = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        #endregion

        #region public properties
        public IObservable<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> OnBeforeAdd => _onBeforeAdd.AsObservable();
        public IObservable<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> OnAfterAdd => _onAfterAdd.AsObservable();
        public IObservable<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> OnBeforeRemove => _onBeforeRemove.AsObservable();
        public IObservable<ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>> OnAfterRemove => _onAfterRemove.AsObservable();
        #endregion

        #region IDictionary<>

        public TValue this[TKey key]
        {
            get => _inner[key];
            set
            {
                KeyValuePair<TKey, TValue> remove = new KeyValuePair<TKey, TValue>(key, _inner[key]);
                KeyValuePair<TKey, TValue> add = new KeyValuePair<TKey, TValue>(key, value);
                _onBeforeRemove.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, remove));
                _onBeforeAdd.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, add));
                _inner[key] = value;
                _onAfterAdd.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, add));
                _onAfterRemove.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, remove));
            }
        }

        public ICollection<TKey> Keys => _inner.Keys;

        public ICollection<TValue> Values => _inner.Values;

        public int Count => _inner.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> add = new KeyValuePair<TKey, TValue>(key, value);
            _onBeforeAdd.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, add));
            _inner.Add(key, value);
            _onAfterAdd.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, add));
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            TKey[] keys = new TKey[_inner.Count];
            _inner.Keys.CopyTo(keys, 0);
            foreach (TKey key in keys)
            {
                this.Remove(key);
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => _inner.ContainsKey(item.Key);

        public bool ContainsKey(TKey key) => _inner.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Array.Copy(_inner.ToArray(), array, Count);


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _inner.GetEnumerator();

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key)) return false;

            KeyValuePair<TKey, TValue> remove = new KeyValuePair<TKey, TValue>(key, _inner[key]);
            _onBeforeRemove.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this, remove));
            _inner.Remove(key);
            _onAfterRemove.OnNext(new ObservableDictionaryEventArgs<ObservableDictionary<TKey, TValue>, TKey, TValue>(this,remove));
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!ReferenceEquals(item.Value, _inner[item.Key]))
            {
                return false;
            }

            return this.Remove(item.Key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => _inner.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
        #endregion
    }
}
