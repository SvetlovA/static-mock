using System.Collections.Generic;

namespace StaticMock.Entities.Context;

public class SetupContextState
{
    public List<ItParameterExpression> ItParameterExpressions { get; set; } = new();
}