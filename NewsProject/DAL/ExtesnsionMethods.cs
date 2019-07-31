using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string columnName, bool keyword)
        {
            try
            {
                var arg = Expression.Parameter(typeof(T), "p");
                var body = Expression.Call(
                    Expression.Property(arg, columnName), "Equals", null,
                    Expression.Constant(keyword));
                var predicate = Expression.Lambda<Func<T, bool>>(body, arg);
                return source.Where(predicate);
            }
            catch { return null; }
        }

        public static IQueryable<T> WithIncludes<T>(this IQueryable<T> source, List<string> associations)
        {
            var query = (DbQuery<T>)source;

            foreach (var assoc in associations)
            {
                query = query.Include(assoc);
            }
            return query;
        }
    }
}
