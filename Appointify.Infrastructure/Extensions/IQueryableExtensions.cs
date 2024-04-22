using System.Linq.Expressions;

namespace Appointify.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ConditionalFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, bool condition)
        {
            return condition ? query.Where(expression) : query;
        }
    }
}
