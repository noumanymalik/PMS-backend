using System.Linq.Expressions;

namespace PMS.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SystemOrderBy<T>(this IQueryable<T> source, string? orderBy = "Id", string? direction = "asc")
        {
            //if (orderBy is null) orderBy = "Id";
            //if (direction is null) direction = "asc";
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");
            MemberExpression property = Expression.Property(parameter, orderBy);
            LambdaExpression lambda = Expression.Lambda(property, parameter);
            var methodName = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
