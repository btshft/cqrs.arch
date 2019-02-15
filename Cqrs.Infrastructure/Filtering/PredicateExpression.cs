using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cqrs.Infrastructure.Filtering
{
    public static class PredicateExpression
    {
        public static Expression<Func<T, bool>> True<T>() => param => true;
        public static Expression<Func<T, bool>> False<T>() => param => false;

        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.AndAlso);
        } 
        
        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.OrElse);
        }
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);
            
            var secondBody = ParameterReplacer.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly IReadOnlyDictionary<ParameterExpression, ParameterExpression> _map;

            private ParameterReplacer(IReadOnlyDictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(
                IReadOnlyDictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterReplacer(map).Visit(exp);
            }

            /// <inheritdoc />
            protected override Expression VisitParameter(ParameterExpression p)
            {
                if (_map.TryGetValue(p, out var replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}