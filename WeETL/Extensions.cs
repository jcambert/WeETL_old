using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WeETL
{
    public static class Extensions
    {
        public static void AddToJob(this IStartable cmp,Job job)
        {
            Contract.Requires(cmp != null, "Startable cannot be null in order to add to job");
            Contract.Requires(job != null, "Job cannot be null in order to add a Startable");
            job.Add(cmp);
        }
        public static PropertyInfo GetProperty<T, TValue>(this Expression<Func<T, TValue>> memberLamda)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
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

        public static void SetPropertyValue<T,TValue>(this T target,string propertyName,TValue value)
            
        {
            typeof(T).GetProperty(propertyName)?.GetSetMethod()?.Invoke(target, new[] { (object)value });
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
        public static IEnumerable<TResult> InnerJoin<TLeft,TRight,TKey,TResult>(this IEnumerable<TLeft> left,IEnumerable<TRight> right, Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey, Func<TLeft, TRight, TResult> result)
        {
            return left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                .Where(o=>o.r.DefaultIfEmpty().Any(oo=>oo!=null))
                .SelectMany(o=>o.r,(l,r)=>new {left=l.l,rigth=r })
                .Select(o => result.Invoke(o.left, o.rigth));

        }

        public static PropertyInfo GetPropertyKey(this Type type)=>type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => Attribute.IsDefined(p, typeof(KeyAttribute))).FirstOrDefault();

        public static TKey GetPropertyKeyValue<T,TKey>(this T value)=>(TKey)(typeof(T).GetPropertyKey()?.GetGetMethod().Invoke(value, null))   ?? default(TKey);

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

        /* public static Expression<TValue> GetPropertyKeyExpression<TValue>(this Type type)
         {

         }*/

    }
}
