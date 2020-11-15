using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Exceptions;

namespace WeETL
{
    public class TRowGenerator<TSchema> : ETLStartableComponent<TSchema, TSchema>
        where TSchema : class, new()
    {

        protected internal readonly Dictionary<string, PopulateAction<TRowGenerator<TSchema>, TSchema>> Actions = new Dictionary<string, PopulateAction<TRowGenerator<TSchema>, TSchema>>(StringComparer.OrdinalIgnoreCase);
        private bool _strict = false;
        private readonly IDisposable _onGeneratedDisposable;
        private readonly ISubject<(TSchema, int)> _onGenerated = new Subject<(TSchema, int)>();
        private TSchema _lastRow;
        private int _lastIter;
        private Action<TSchema> _initSchemaFunction;
         
        public TRowGenerator() : base()
        {
            _onGeneratedDisposable= OnGenerated.Subscribe(iter => { _lastRow = iter.Item1;_lastIter = iter.Item2; });
        }

        public ISubject<(TSchema, int)> GeneratedHandler => _onGenerated;
        public IObservable<(TSchema, int)> OnGenerated => GeneratedHandler.AsObservable();

        public TRowGenerator<TSchema> Strict(bool strict)
        {
            this._strict = strict;
            return this;
        }
        public TRowGenerator<TSchema> SchemaInitilialization(Action<TSchema> initFn)
        {
            this._initSchemaFunction = initFn;
            return this;
        }

        public bool Retain { get; set; } = false;

        [Description("Nombre de Ligne a générer")]
        public int NumberOfRowToGenerate { get; set; } = 10;

        public int LastIteration => _lastIter ;

        protected override Task InternalStart()
        {

            if (_strict && !(typeof(TSchema).GetProperties().Select(p => p.Name).All(p => Actions.ContainsKey(p))))
            {
                OutputHandler.OnError(new ValidationException("In Strict mode, you must have a generator for each property"));

            }
            for (int i = 0; i < NumberOfRowToGenerate; i++)
            {
                var res = Generate();
                OutputHandler.OnNext(res);
                GeneratedHandler.OnNext((res, i));
            }
            return Task.CompletedTask;
        }

        public virtual TSchema Generate()
        {
            TSchema schema = CreateNewRow();
            foreach (var property in Actions.Keys)
            {
                var value = Actions[property].Action(this, schema);
                schema.SetPropertyValue(property, value);
            }
            return schema;
        }
        protected virtual TSchema CreateNewRow() {
            Func<TSchema> CreateRow = () =>
            {
                var res = new TSchema();
                _initSchemaFunction?.Invoke(res);
                return res;
            };
            return   Retain? Mapper.Map(_lastRow ?? CreateRow()): CreateRow();
        }

        
        protected override void InternalDispose()
        {
            base.InternalDispose();
            _onGeneratedDisposable?.Dispose();
        }

        public TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property, TProperty value)
        {
            var propertyName = property.GetProperty()?.Name ?? null;

            return AddRule(propertyName, (f, t) => value);
        }
        public TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property,Func<TRowGenerator<TSchema>, TSchema,TProperty> valueFunction)
        {
            var propertyName = property.GetProperty()?.Name ?? null;
            return AddRule(propertyName, (f, t) => valueFunction(f,t));
        }
        public TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property, Func< TProperty> valueFunction)
        {
            var propertyName = property.GetProperty()?.Name ?? null;
            return AddRule(propertyName, (f, t) => valueFunction());
        }
        public virtual TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property, Func<TRowGenerator<TSchema>, TProperty> setter)
        {
            var propertyName = property.GetProperty()?.Name ?? null;

            return AddRule(propertyName, (f, t) => setter(f));
        }
        protected virtual TRowGenerator<TSchema> AddRule(string propertyOrField, Func<TRowGenerator<TSchema>, TSchema, object> invoker)
        {
            Contract.Requires(propertyOrField != null, "Cannot add rule with null property");
            Contract.Ensures(Contract.Result<TRowGenerator<TSchema>>() != null);
            var rule = new PopulateAction<TRowGenerator<TSchema>, TSchema>
            {
                Action = invoker,
                PropertyName = propertyOrField,
            };
            this.Actions[propertyOrField] = rule;
            return this;
        }

       
    }
}
