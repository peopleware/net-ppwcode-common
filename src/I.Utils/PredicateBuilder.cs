using System.Linq.Expressions;

namespace PPWCode.Common.I.Utils;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
        => predicate;

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>>? first, Expression<Func<T, bool>> second)
        => first != null
               ? first.Compose(second, Expression.AndAlso)
               : second;

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>>? first, Expression<Func<T, bool>> second)
        => first != null
               ? first.Compose(second, Expression.OrElse)
               : second;

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        UnaryExpression negated = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
    }

    /// <summary>
    ///     Combines the first expression with the second using the specified merge function.
    /// </summary>
    private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
    {
        // zip parameters (map from parameters of second to parameters of first)
        Dictionary<ParameterExpression, ParameterExpression> map =
            first
                .Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

        // replace parameters in the second lambda expression with the parameters in the first
        Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

        // create a merged lambda expression with parameters from the first expression
        return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
    }

    private class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression>? map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression>? map, Expression exp)
            => new ParameterRebinder(map).Visit(exp);

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out ParameterExpression? replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
