using System.Linq.Expressions;

namespace StaticMock.Entities.Context;

public class It
{
    private readonly SetupContextState _setupContextState;

    public It(SetupContextState setupContextState)
    {
        _setupContextState = setupContextState;
    }

    public TValue? IsAny<TValue>() => Is<TValue>(x => true);

    public TValue? Is<TValue>(Expression<Func<TValue, bool>> predicate)
    {
        var predicateParameterExpression = predicate.Parameters[0];
        var conditionExpression = Expression.IfThen(
            Expression.Not(predicate.Body),
            Expression.Throw(Expression.Constant(new Exception($"Condition {predicate} failed."))));

        _setupContextState.ItParameterExpressions.Add(new ItParameterExpression
        {
            ParameterType = predicateParameterExpression.Type,
            ParameterExpression = Expression.Lambda<Action<TValue>>(conditionExpression, predicateParameterExpression)
        });

        return default;
    }
}