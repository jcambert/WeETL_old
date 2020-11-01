using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WeETL
{
    public static class Extensions
    {
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
    }
}
