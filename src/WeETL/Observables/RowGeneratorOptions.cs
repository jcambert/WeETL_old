using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Utilities;

namespace WeETL.Observables
{
    public class RowGeneratorOptions<T>
        where T:class,new()
    {
        protected internal readonly Dictionary<string, PopulateAction<RowGenerator<T>, T>> Actions = new Dictionary<string, PopulateAction<RowGenerator<T>, T>>(StringComparer.OrdinalIgnoreCase);
        public bool Retain { get;  set; }
        public Action<T> InitFunction { get; set; }

        public RowGeneratorOptions<T> GeneratorFor<TProperty>(Expression<Func<T, TProperty>> property, TProperty value)
        {
            var propertyName = property.GetProperty()?.Name ?? null;

            return AddRule(propertyName, (f, t) => value);
        }
        public RowGeneratorOptions<T> GeneratorFor<TProperty>(Expression<Func<T, TProperty>> property, Func<RowGenerator<T>, T, TProperty> valueFunction)
        {
            var propertyName = property.GetProperty()?.Name ?? null;
            return AddRule(propertyName, (f, t) => valueFunction(f, t));
        }
        public RowGeneratorOptions<T> GeneratorFor<TProperty>(Expression<Func<T, TProperty>> property, Func<TProperty> valueFunction)
        {
            var propertyName = property.GetProperty()?.Name ?? null;
            return AddRule(propertyName, (f, t) => valueFunction());
        }
        public virtual RowGeneratorOptions<T> GeneratorFor<TProperty>(Expression<Func<T, TProperty>> property, Func<RowGenerator<T>, TProperty> setter)
        {
            var propertyName = property.GetProperty()?.Name ?? null;

            return AddRule(propertyName, (f, t) => setter(f));
        }
        protected virtual RowGeneratorOptions<T> AddRule(string propertyOrField, Func<RowGenerator<T>, T, object> invoker)
        {
            Check.NotNull(propertyOrField, nameof(propertyOrField));
            
            var rule = new PopulateAction<RowGenerator<T>, T>
            {
                Action = invoker,
                PropertyName = propertyOrField,
            };
            this.Actions[propertyOrField] = rule;
            return this;
        }
    }
}
