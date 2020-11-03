using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;

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
        private bool _dataCompleted, _lookupCompleted;
        private TMapJoin _join;
        private IDisposable _lookupDisposable;
        public bool AcceptLookup => _lookupDisposable == null;

        private List<TInputSchema> _dataRows = new List<TInputSchema>();
        private List<TLookupSchema> _lookupRows = new List<TLookupSchema>();
        private Action<IMapperConfigurationExpression> _mapperConfig;
        private Func<TInputSchema, TKey> _leftKey;
        private Func<TLookupSchema, TKey> _rightKey;
        private readonly ISubject<TMap<TInputSchema, TOutputSchema, TLookupSchema, TKey>> _onSetLookup = new Subject<TMap<TInputSchema, TOutputSchema, TLookupSchema, TKey>>();
        public TMap()
        {

        }
        public bool SetLookup(IObservable<TLookupSchema> lookup)
        {
            return SetObservable<TLookupSchema, TOutputSchema>(
                lookup,
                AcceptLookup,
                $"a Lookup Schema has already been connected",
                _lookupDisposable,
                InternalOnLookupBeforeTransform,
                InternalLookupTransform,
                InternalOnLookupAfterTransform,
                InternalSendOutput,
                InternalOnException,
                InternalOnInputCompleted,
                _onSetLookup
                );
           /* if (!AcceptLookup)
            {
                Error.OnNext(new ConnectorException($"a Lookup Schema has already been connected"));
                return false;
            }
            _lookupDisposable = lookup.Subscribe(row =>
            {
                _lookupRows.Add(row);
            },
            InternalOnException,
            InternalOnLookupCompleted);
            return true;*/
        }
        public void Join(TMapJoin join)
        {
            this._join = join;
        }
        protected override void InternalOnInputBeforeTransform(int index, TInputSchema row)
        {
            base.InternalOnInputBeforeTransform(index, row);
            _dataRows.Add(row);
        }

        protected override TOutputSchema InternalTransform(TInputSchema row)
        {
            return base.InternalTransform(row);
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
        protected override void InternalSendOutput(TOutputSchema row)
        {
            //DO NOTHING UNLESS ALL DATAS ARE IN MEMORY
            //AND THE JOIN FUNCTION HAS BEEN DONE

        }
        protected override void CreateMapper()
        {

            var config = _mapperConfig == null ? new MapperConfiguration(cfg => { cfg.CreateMap<TInputSchema, TOutputSchema>(); cfg.CreateMap<TLookupSchema, TOutputSchema>(); }) : new MapperConfiguration(this._mapperConfig);
            Mapper = config.CreateMapper();

        }
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
                    Output.OnNext(Mapper.Map<TOutputSchema>(item.l, item.r));
                }
                Output.OnCompleted();

            }

            catch (Exception e)
            {
                Error.OnNext(new ConnectorException("An error occure while Performing Mapping", e));
            }
        }
        public void SetLeftKey(Func<TInputSchema, TKey> leftKey)
        {
            this._leftKey = leftKey;
        }
        protected  TKey GetLeftKey(TInputSchema d)=>this._leftKey!=null? this._leftKey.Invoke(d) :  d.GetPropertyKeyValue<TInputSchema, TKey>();
        
        public void SetRightKey(Func<TLookupSchema, TKey> rightKey)
        {
            this._rightKey = rightKey;
        }
        protected TKey GetRightKey(TLookupSchema d)=>this._rightKey!=null? this._rightKey.Invoke(d) : d.GetPropertyKeyValue<TLookupSchema, TKey>();
        

        public void SetMapping(Action<IMapperConfigurationExpression> config)
        {
            this._mapperConfig = config;
        }
    }
}
