using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;
using StaticMock.Hooks.HookBuilders.Transpilers;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class StaticTranspilerHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;
    private readonly MethodInfo _originalMethodInfo;

    public StaticTranspilerHookBuilder(MethodInfo originalMethodInfo, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
        _originalMethodInfo = originalMethodInfo;
    }

    public MethodInfo CreateCallbackHook(Action callback)
    {
        ValidationHelper.ValidateReturnType(_originalMethodInfo.ReturnType, callback.Method.ReturnType);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg>(Action<TArg> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2>(Action<TArg1, TArg2> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback)
    {
        ValidationHelper.Validate(_originalMethodInfo, callback.Method);
        return CreateCallbackHookInternal(callback);
    }

    public MethodInfo CreateReturnHook<TReturn>(TReturn value)
    {
        ReturnHookTranspiler<TReturn>.ReturnValue = value;
        ReturnHookTranspiler<TReturn>.HookMethodType = HookMethodType.Static;
        ReturnHookTranspiler<TReturn>.ItParameterExpressions = _itParameterExpressions;

        return AccessTools.Method(typeof(ReturnHookTranspiler<TReturn>), nameof(ReturnHookTranspiler<TReturn>.Transpiler));
    }

    public MethodInfo CreateReturnHook<TReturn>(Func<TReturn> getValue)
    {
        ValidationHelper.ValidateReturnType(_originalMethodInfo.ReturnType, getValue.Method.ReturnType);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TReturn>(Func<TArg1, TArg2, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TReturn>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        ValidationHelper.Validate(_originalMethodInfo, getValue.Method);
        return CreateReturnHookInternal(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TReturn>(TReturn value)
    {
        ReturnAsyncHookTranspiler<TReturn>.ReturnValue = value;
        ReturnAsyncHookTranspiler<TReturn>.HookMethodType = HookMethodType.Static;
        ReturnAsyncHookTranspiler<TReturn>.ItParameterExpressions = _itParameterExpressions;

        return AccessTools.Method(typeof(ReturnAsyncHookTranspiler<TReturn>), nameof(ReturnAsyncHookTranspiler<TReturn>.Transpiler));
    }

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new()
    {
        ThrowsHookTranspiler<TException>.Exception = exception;
        ThrowsHookTranspiler<TException>.HookMethodType = HookMethodType.Static;
        ThrowsHookTranspiler<TException>.ItParameterExpressions = _itParameterExpressions;

        return AccessTools.Method(typeof(ThrowsHookTranspiler<TException>), nameof(ThrowsHookTranspiler<TException>.Transpiler));
    }

    private MethodInfo CreateReturnHookInternal(object getValue)
    {
        ReturnHookTranspiler.OriginalMethodInfo = _originalMethodInfo;
        ReturnHookTranspiler.GetValue = getValue;
        ReturnHookTranspiler.HookMethodType = HookMethodType.Static;
        ReturnHookTranspiler.ItParameterExpressions = _itParameterExpressions;

        return AccessTools.Method(typeof(ReturnHookTranspiler), nameof(ReturnHookTranspiler.Transpiler));
    }

    private MethodInfo CreateCallbackHookInternal(object callback)
    {
        CallbackHookTranspiler.OriginalMethodInfo = _originalMethodInfo;
        CallbackHookTranspiler.Callback = callback;
        CallbackHookTranspiler.HookMethodType = HookMethodType.Static;
        CallbackHookTranspiler.ItParameterExpressions = _itParameterExpressions;

        return AccessTools.Method(typeof(CallbackHookTranspiler), nameof(CallbackHookTranspiler.Transpiler));
    }
}