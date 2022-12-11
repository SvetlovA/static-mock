using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class StaticHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;
    private readonly MethodInfo _originalMethodInfo;

    public StaticHookBuilder(MethodInfo originalMethodInfo, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
        _originalMethodInfo = originalMethodInfo;
    }

    public MethodInfo CreateCallbackHook(Action callback)
    {
        HookValidationHelper.ValidateReturnType(_originalMethodInfo.ReturnType, callback.Method.ReturnType);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg>(Action<TArg> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2>(Action<TArg1, TArg2> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback)
    {
        HookValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(Func<TReturn> getValue)
    {
        HookValidationHelper.ValidateReturnType(_originalMethodInfo.ReturnType, getValue.Method.ReturnType);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TReturn>(Func<TArg1, TArg2, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TReturn>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnAsyncHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Static, _itParameterExpressions);

    private MethodInfo CreateReturnHookInternal<TReturn>(object getValue) =>
        HookBuilderHelper.CreateReturnHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Static,
            _itParameterExpressions);

    private MethodInfo CreateCallbackHookInternal(object callback) =>
        HookBuilderHelper.CreateCallbackHook(
            _originalMethodInfo,
            callback,
            HookMethodType.Static,
            _itParameterExpressions);
}