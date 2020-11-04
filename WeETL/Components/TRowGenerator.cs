using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Exceptions;

namespace WeETL
{
    public class TRowGenerator<TSchema> : ETLStartableComponent<TSchema,TSchema>
        where TSchema : class, new()
    {

        protected internal readonly Dictionary<string, PopulateAction<TRowGenerator<TSchema>, TSchema>> Actions = new Dictionary<string, PopulateAction<TRowGenerator<TSchema>, TSchema>>(StringComparer.OrdinalIgnoreCase);
        private bool _strict = false;
        
        public TRowGenerator():base()
        {
        }
        public TRowGenerator<TSchema> Strict(bool strict)
        {
            this._strict = strict;
            return this;
        }



        [Description("Nombre de Ligne a générer")]
        public int NumberOfRowToGenerate { get; set; } = 10;


        protected override void InternalStart()
        {
            
                    if (_strict && !(typeof(TSchema).GetProperties().Select(p => p.Name).All(p => Actions.ContainsKey(p))))
                    {
                        Output.OnError(new ValidationException("In Strict mode, you must have a generator for each property"));

                    }
                    for (int i = 0; i < NumberOfRowToGenerate; i++)
                    {
                        Output.OnNext(Generate());
                    }
                   
        }
        
        public virtual TSchema Generate()
        {
            TSchema schema = new TSchema();
            foreach (var property in Actions.Keys)
            {
                var value = Actions[property].Action(this, schema);
                schema.SetPropertyValue(property, value);
            }
            return schema;
        }
        public TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property, TProperty value)
        {
            var propertyName = property.GetProperty()?.Name ?? null;

            return AddRule(propertyName, (f, t) => value);
        }
        public TRowGenerator<TSchema> GeneratorFor<TProperty>(Expression<Func<TSchema, TProperty>> property, Func<TProperty> valueFunction)
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
                RuleSet = "",
                PropertyName = propertyOrField,
            };
            this.Actions[propertyOrField] = rule;
            return this;
        }
 

    }
}
