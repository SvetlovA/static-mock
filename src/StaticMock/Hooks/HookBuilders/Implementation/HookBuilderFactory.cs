using System.Reflection;
using StaticMock.Entities.Context;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class HookBuilderFactory : IHookBuilderFactory
{
    private readonly MethodBase _originalMethod;
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;

    public HookBuilderFactory(MethodBase originalMethod, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _originalMethod = originalMethod;
        _itParameterExpressions = itParameterExpressions;
    }

    public IHookBuilder CreateHookBuilder()
    {
        if (_originalMethod.IsStatic)
        {
            return new StaticHookBuilder(_itParameterExpressions);
        }

        return new InstanceHookBuilder(_itParameterExpressions);
    }
}