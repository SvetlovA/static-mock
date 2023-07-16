using System.Collections.Generic;
using StaticMock.Entities.Context;
using StaticMock.Entities.Enums;

namespace StaticMock.Hooks.Entities;

internal class HookSettings
{
    public HookManagerType HookManagerType { get; set; }
    public IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;
    public object? OriginalMethodCallInstance { get; set; }
}