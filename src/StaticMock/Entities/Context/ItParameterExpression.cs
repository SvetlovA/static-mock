using System;
using System.Linq.Expressions;

namespace StaticMock.Entities.Context;

public class ItParameterExpression
{
    public Type ParameterType { get; set; } = null!;
    public LambdaExpression? ParameterExpression { get; set; }
}