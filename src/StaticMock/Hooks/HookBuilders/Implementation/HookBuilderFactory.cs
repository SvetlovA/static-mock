using System.Reflection;
using StaticMock.Entities.Context;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class HookBuilderFactory : IHookBuilderFactory
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;

    public HookBuilderFactory(MethodInfo originalMethodInfo, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _originalMethodInfo = originalMethodInfo;
        _itParameterExpressions = itParameterExpressions;
    }

    public IHookBuilder CreateHookBuilder()
    {
        if (_originalMethodInfo.IsStatic)
        {
            return new StaticTranspilerHookBuilder(_originalMethodInfo, _itParameterExpressions); //new StaticHookBuilder(_originalMethodInfo, _itParameterExpressions);
        }

        return new InstanceTranspilerHookBuilder(_originalMethodInfo, _itParameterExpressions); //new InstanceHookBuilder(_originalMethodInfo, _itParameterExpressions);
    }
}