using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using WeETL.Core;
using WeETL.Schemas;

namespace WeETL
{
    public static class Extensions
    {
        public static void AddToJob(this IStartable cmp, Job job)
        {
            Contract.Requires(cmp != null, "Startable cannot be null in order to add to job");
            Contract.Requires(job != null, "Job cannot be null in order to add a Startable");
            job.Add(cmp);
        }
        public static void RemoveFromJob(this IStartable cmp, Job job)
        {
            Contract.Requires(cmp != null, "Startable cannot be null in order to add to job");
            Contract.Requires(job != null, "Job cannot be null in order to add a Startable");
            job.Remove(cmp);
        }
        internal static void LoadDefaultComponents(this IServiceCollection service)
        {
            var types = typeof(ETLContext).Assembly.GetTypes().Where(t =>!t.IsAbstract && t.IsPublic && typeof(IETLCoreComponent).IsAssignableFrom(t) /* && t.IsTypeof< ETLComponent <NoneSchema,NoneSchema> > ( )*/);
            foreach (var type in types)
            {
               // Debug.WriteLine(type.Name);
                service.TryAddTransient(@type);
            }
        }
        internal static bool IsTypeof<T>(this object t)
        {
            return (t is T);
        }
        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)=>
            source.Select((item, index) => (item, index));
        

        public static PropertyInfo GetProperty<T, TValue>(this Expression<Func<T, TValue>> property)
        {
            var memberSelectorExpression = property.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                return memberSelectorExpression.Member as PropertyInfo;
            }
            return null;
        }
        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda, TValue value)
        {

            var property = memberLamda.GetProperty();
            if (property != null)
            {
                property.SetValue(target, value, null);
            }

        }

        public static void SetPropertyValue<T, TValue>(this T target, string propertyName, TValue value)

        {
            typeof(T).GetProperty(propertyName)?.GetSetMethod()?.Invoke(target, new[] { (object)value });
        }
        public static void SetPropertyValueAndNotify<T, TValue>(this T target, Expression<Func<T, TValue>> property, ref TValue current, TValue @new, IObserver<string> handler)
        {
            if (!current.Equals(@new))
            {
                current = @new;
                handler?.OnNext(property.GetPropertyName());
            }

        }

        public static void SetPropertyValueAndNotify<T, TValue>(this T target, Expression<Func<T, TValue>> property, ref TValue current, TValue @new, PropertyChangedEventHandler handler)
        where T : INotifyPropertyChanged
        {
            if (!current.Equals(@new))
            {
                current = @new;
                target.NotifyPropertyChanged(handler, property.GetPropertyName());
            }

        }

        internal static void NotifyPropertyChanged<T>(this T target, PropertyChangedEventHandler handler, [CallerMemberName] String propertyName = "")
            where T : INotifyPropertyChanged
        {
            handler?.Invoke(target, new PropertyChangedEventArgs(propertyName));
        }

        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey,
        Func<TLeft, TRight, TResult> result)
        {
            return left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                 .SelectMany(
                     o => o.r.DefaultIfEmpty(),
                     (l, r) => new { lft = l.l, rght = r })
                 .Select(o => result.Invoke(o.lft, o.rght));
        }
        public static IEnumerable<TResult> InnerJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey, Func<TLeft, TRight, TResult> result)
        {
            return left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                .Where(o => o.r.DefaultIfEmpty().Any(oo => oo != null))
                .SelectMany(o => o.r, (l, r) => new { left = l.l, rigth = r })
                .Select(o => result.Invoke(o.left, o.rigth));

        }

        public static PropertyInfo GetPropertyKey(this Type type) => type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => Attribute.IsDefined(p, typeof(KeyAttribute))).FirstOrDefault();

        public static TKey GetPropertyKeyValue<T, TKey>(this T value) => (TKey)(typeof(T).GetPropertyKey()?.GetGetMethod().Invoke(value, null)) ?? default(TKey);

        public static TDestination Map<TDestination>(this IMapper mapper, params object[] sources) where TDestination : new()
        {
            return Map(mapper, new TDestination(), sources);
        }

        public static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, params object[] sources) where TDestination : new()
        {
            if (!sources.Any())
                return destination;

            foreach (var src in sources)
                destination = mapper.Map(src, destination);

            return destination;
        }

        /// <summary>
        /// Gets property information for the specified <paramref name="property"/> expression.
        /// </summary>
        /// <typeparam name="TSource">Type of the parameter in the <paramref name="property"/> expression.</typeparam>
        /// <typeparam name="TValue">Type of the property's value.</typeparam>
        /// <param name="property">The expression from which to retrieve the property information.</param>
        /// <returns>Property information for the specified expression.</returns>
        /// <exception cref="ArgumentException">The expression is not understood.</exception>
        public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            var body = property.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Expression is not a property", "property");
            }

            var propertyInfo = body.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expression is not a property", "property");
            }

            return propertyInfo;
        }
        public static string GetPropertyName<TSource, TValue>(this Expression<Func<TSource, TValue>> property) => property.GetPropertyInfo().Name;


        /// <summary>
        /// Returns an observable sequence of the source any time the <c>PropertyChanged</c> event is raised.
        /// </summary>
        /// <typeparam name="T">The type of the source object. Type must implement <seealso cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="source">The object to observe property changes on.</param>
        /// <returns>Returns an observable sequence of the value of the source when ever the <c>PropertyChanged</c> event is raised.</returns>
        public static IObservable<T> OnAnyPropertyChanges<T>(this T source)
            where T : INotifyPropertyChanged
        {
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                               handler => handler.Invoke,
                               h => source.PropertyChanged += h,
                               h => source.PropertyChanged -= h)
                           .Select(_ => source);
        }

        /// <summary>
        /// Returns an observable sequence of the value of a property when <paramref name="source"/> raises <seealso cref="INotifyPropertyChanged.PropertyChanged"/> for the given property.
        /// </summary>
        /// <typeparam name="T">The type of the source object. Type must implement <seealso cref="INotifyPropertyChanged"/>.</typeparam>
        /// <typeparam name="TProperty">The type of the property that is being observed.</typeparam>
        /// <param name="source">The object to observe property changes on.</param>
        /// <param name="property">An expression that describes which property to observe.</param>
        /// <returns>Returns an observable sequence of the property values as they change.</returns>
        public static IObservable<TProperty> OnPropertyChanges<T, TProperty>(this T source, Expression<Func<T, TProperty>> property)
            where T : INotifyPropertyChanged
        {
            return Observable.Create<TProperty>(o =>
            {
                var propertyName = property.GetPropertyInfo().Name;
                var propertySelector = property.Compile();

                return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                                handler => handler.Invoke,
                                h => source.PropertyChanged += h,
                                h => source.PropertyChanged -= h)
                            .Where(e => e.EventArgs.PropertyName == propertyName)
                            .Select(e => propertySelector(source))
                            .Subscribe(o);
            });
        }

        public static List<TComponent> Sorting<TComponent, TKey>(this List<TComponent> list, Func<TComponent, TKey> sortKey, SortOrder sortOrder=SortOrder.Ascending)
        => (sortOrder == SortOrder.Ascending) ? list.OrderBy(sortKey).ToList() : list.OrderByDescending(sortKey).ToList();

    }
}
