using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public static class LinqExtensions
{
    public static IQueryable<T> WhereLike<T>(this IQueryable<T> source, string expr)
    {
        if (string.IsNullOrEmpty(expr))
            return source;

        var parameter = Expression.Parameter(typeof(T), "x");
        var properties = typeof(T).GetProperties()
                                  .Where(p => p.PropertyType == typeof(string));

        Expression predicate = null;

        foreach (var property in properties)
        {
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(expr, typeof(string));
            var containsExpression = Expression.Call(propertyAccess, containsMethod, someValue);

            if (predicate == null)
            {
                predicate = containsExpression;
            }
            else
            {
                predicate = Expression.OrElse(predicate, containsExpression);
            }
        }

        if (predicate == null)
            return source;

        var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
        return source.Where(lambda);
    }
}
