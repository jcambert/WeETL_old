using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using WeETL.Core;

namespace WeETL
{
    public enum TMapJoin
    {
        LeftOuter,
        Inner
    }
    public class TMap<TInputSchema, TOutputSchema, TLookupSchema, TKey> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class, new()
        where TOutputSchema : class, new()
        where TLookupSchema : class, new()
    {
        #region private vars
        private bool _dataCompleted, _lookupCompleted;
        private TMapJoin _join;
        private IDisposable _lookupDisposable=null;
        public bool AcceptLookup => _lookupDisposable == null;

        private List<TInputSchema> _dataRows = new List<TInputSchema>();
        private List<TLookupSchema> _lookupRows = new List<TLookupSchema>();
        private Action<IMapperConfigurationExpression> _mapperConfig;
        private Func<TInputSchema, TKey> _leftKey;
        private Func<TLookupSchema, TKey> _rightKey;
        private readonly ISubject<TMap<TInputSchema, TOutputSchema, TLookupSchema, TKey>> _onSetLookup = new Subject<TMap<TInputSchema, TOutputSchema, TLookupSchema, TKey>>();
        #endregion
        #region ctor
        public TMap()
        {

        }
        #endregion
        #region public methods
        public bool SetLookup(IObservable<TLookupSchema> lookup)
        {
            var result= SetObservable<TLookupSchema, TOutputSchema>(
                lookup,
                AcceptLookup,
                $"a Lookup Schema has already been connected",
                ref _lookupDisposable,
                InternalOnException,
                InternalOnLookupCompleted
                );
            if (result)_onSetLookup.OnNext(this);
            return result;

        }
        public void Join(TMapJoin join)
        {
            this._join = join;
        }
        public void SetMapping(Action<IMapperConfigurationExpression> config)
        {
            this._mapperConfig = config;
        }
        public void SetLeftKey(Func<TInputSchema, TKey> leftKey)
        {
            this._leftKey = leftKey;
        }

        public void SetRightKey(Func<TLookupSchema, TKey> rightKey)
        {
            this._rightKey = rightKey;
        }
        #endregion
        #region protected methods
        protected override void InternalOnInputBeforeTransform(int index, TInputSchema row)
        {
            base.InternalOnInputBeforeTransform(index, row);
            _dataRows.Add(row);
        }


        protected virtual void InternalOnLookupCompleted()
        {
            _lookupCompleted = true;
            PerformJoin();
        }
        protected override void InternalOnInputCompleted()
        {
            _dataCompleted = true;
            PerformJoin();
        }
        protected override void InternalSendOutput(int index,TOutputSchema row)
        {
            //DO NOTHING UNLESS ALL DATAS ARE IN MEMORY
            //AND THE JOIN FUNCTION HAS BEEN DONE

        }
        protected override void CreateMapper()
        {

            var config = _mapperConfig == null ? new MapperConfiguration(cfg => { cfg.CreateMap<TInputSchema, TOutputSchema>(); cfg.CreateMap<TLookupSchema, TOutputSchema>(); }) : new MapperConfiguration(this._mapperConfig);
            Mapper = config.CreateMapper();

        }
        protected override void InternalDispose()
        {
            base.InternalDispose();
            _lookupDisposable?.Dispose();
        }

        #endregion
        #region private methods
        private void PerformJoin()
        {
            if (!_dataCompleted || !_lookupCompleted) return;
            try
            {
#pragma warning disable CS8509 // L'expression switch ne prend pas en charge toutes les valeurs possibles de son type d'entrée (elle n'est pas exhaustive).
                var joinedRows = _join switch
#pragma warning restore CS8509 // L'expression switch ne prend pas en charge toutes les valeurs possibles de son type d'entrée (elle n'est pas exhaustive).
                {
                    TMapJoin.LeftOuter => _dataRows.LeftOuterJoin(_lookupRows, GetLeftKey, GetRightKey, (r, l) => (r, l)).ToList(),
                    TMapJoin.Inner => _dataRows.InnerJoin(_lookupRows, GetLeftKey,GetRightKey, (r, l) => (r, l)).ToList(),
                };
                foreach (var item in joinedRows)
                {
                    OutputHandler.OnNext(Mapper.Map<TOutputSchema>(item.l, item.r));
                }
                OutputHandler.OnCompleted();

            }

            catch (Exception e)
            {
                ErrorHandler.OnNext(new ConnectorException("An error occure while Performing Mapping", e));
            }
        }
        #endregion
        protected  TKey GetLeftKey(TInputSchema d)=>this._leftKey!=null? this._leftKey.Invoke(d) :  d.GetPropertyKeyValue<TInputSchema, TKey>();
        
        protected TKey GetRightKey(TLookupSchema d)=>this._rightKey!=null? this._rightKey.Invoke(d) : d.GetPropertyKeyValue<TLookupSchema, TKey>();
        

       
    }
}
