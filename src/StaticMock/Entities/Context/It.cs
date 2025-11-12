using System;
using System.Linq.Expressions;

namespace StaticMock.Entities.Context;

/// <summary>
/// Provides parameter matching capabilities for method arguments in mock setups.
/// Allows flexible matching using predicates and the common "IsAny" pattern.
/// </summary>
public class It
{
    private readonly SetupContextState _setupContextState;

    /// <summary>
    /// Initializes a new instance of the <see cref="It"/> class with the specified setup context state.
    /// </summary>
    /// <param name="setupContextState">The setup context state that tracks parameter expressions.</param>
    public It(SetupContextState setupContextState)
    {
        _setupContextState = setupContextState;
    }

    /// <summary>
    /// Matches any argument of the specified type. This is equivalent to calling <see cref="Is{TValue}(Expression{Func{TValue, bool}})"/>
    /// with a predicate that always returns true.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <returns>The default value of type <typeparamref name="TValue"/>. This value is not used at runtime.</returns>
    /// <example>
    /// <code>
    /// // Match any string argument
    /// Mock.Setup(() => MyClass.ProcessString(It.IsAny&lt;string&gt;()));
    ///
    /// // Match any integer argument
    /// Mock.Setup(() => MyClass.ProcessInt(It.IsAny&lt;int&gt;()));
    /// </code>
    /// </example>
    public TValue? IsAny<TValue>() => Is<TValue>(x => true);

    /// <summary>
    /// Matches arguments that satisfy the specified predicate condition.
    /// </summary>
    /// <typeparam name="TValue">The type of the argument to match.</typeparam>
    /// <param name="predicate">The predicate expression that defines the matching condition.
    /// The argument must satisfy this condition for the mock to be triggered.</param>
    /// <returns>The default value of type <typeparamref name="TValue"/>. This value is not used at runtime.</returns>
    /// <exception cref="Exception">Thrown during mock execution if the actual argument does not satisfy the predicate.</exception>
    /// <example>
    /// <code>
    /// // Match positive integers only
    /// Mock.Setup(() => MyClass.ProcessInt(It.Is&lt;int&gt;(x => x > 0)));
    ///
    /// // Match strings with specific length
    /// Mock.Setup(() => MyClass.ProcessString(It.Is&lt;string&gt;(s => s.Length > 5)));
    ///
    /// // Match objects with specific properties
    /// Mock.Setup(() => MyClass.ProcessUser(It.Is&lt;User&gt;(u => u.IsActive && u.Age >= 18)));
    /// </code>
    /// </example>
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