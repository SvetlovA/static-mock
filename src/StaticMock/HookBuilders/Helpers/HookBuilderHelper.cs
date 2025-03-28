﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using StaticMock.Entities.Context;
using StaticMock.HookBuilders.Entities;

namespace StaticMock.HookBuilders.Helpers;

internal static class HookBuilderHelper
{
    public static MethodInfo CreateCallbackHook(
        MethodInfo originalMethodInfo,
        object callback,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        CreateExecutableHook(
            originalMethodInfo,
            callback,
            hookMethodType,
            itParameterExpressions,
            typeof(void));

    public static MethodInfo CreateReturnHook<TReturn>(
        MethodBase originalMethod,
        TReturn value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        CreateValueHook(
            originalMethod,
            value,
            hookMethodType,
            itParameterExpressions,
            typeof(TReturn));

    public static MethodInfo CreateReturnAsyncHook<TReturn>(
        MethodBase originalMethod,
        TReturn value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        CreateValueHook(
            originalMethod,
            Task.FromResult(value),
            hookMethodType,
            itParameterExpressions,
            typeof(Task<TReturn>));

    public static MethodInfo CreateReturnHook<TReturn>(
        MethodInfo originalMethodInfo,
        object getValue,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        CreateExecutableHook(
            originalMethodInfo,
            getValue,
            hookMethodType,
            itParameterExpressions,
            typeof(TReturn));

    public static MethodInfo CreateThrowsHook<TException>(
        MethodInfo originalMethod,
        TException exception,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) where TException : Exception, new()
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = DynamicTypeNames.ExceptionHookAssemblyName
        }, AssemblyBuilderAccess.Run);

        var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.ExceptionHookModuleName);
        var hookType = hookModule.DefineType(DynamicTypeNames.ExceptionHookTypeName, TypeAttributes.Public);

        var hookStaticField = hookType.DefineField(
            DynamicTypeNames.ExceptionHookStaticFieldName,
            typeof(TException),
            FieldAttributes.Private | FieldAttributes.Static);

        var hookMethod = hookType.DefineMethod(
            DynamicTypeNames.ExceptionHookMethodName,
            GetMethodAttributes(hookMethodType),
            originalMethod.ReturnType,
            GetParameterTypes(hookMethodType, originalMethod));

        return GetHookedMethod(
            hookType,
            hookMethod,
            hookStaticField,
            OpCodes.Throw,
            exception,
            hookMethodType,
            itParameterExpressions);
    }

    private static MethodInfo CreateValueHook(
        MethodBase originalMethod,
        object? value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions,
        Type hookReturnType)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = DynamicTypeNames.ReturnHookAssemblyName
        }, AssemblyBuilderAccess.Run);

        var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.ReturnHookModuleName);
        var hookType = hookModule.DefineType(DynamicTypeNames.ReturnHookTypeName, TypeAttributes.Public);
        var hookStaticField = hookType.DefineField(
            DynamicTypeNames.ReturnHookStaticFieldName,
            hookReturnType,
            FieldAttributes.Private | FieldAttributes.Static);

        var hookMethod = hookType.DefineMethod(
            DynamicTypeNames.ReturnHookMethodName,
            GetMethodAttributes(hookMethodType),
            hookReturnType,
            GetParameterTypes(hookMethodType, originalMethod));

        return GetHookedMethod(
            hookType,
            hookMethod,
            hookStaticField,
            OpCodes.Ret,
            value,
            hookMethodType,
            itParameterExpressions);
    }

    private static MethodInfo CreateExecutableHook(
        MethodBase originalMethod,
        object executable,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions,
        Type hookReturnType)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = DynamicTypeNames.ReturnHookAssemblyName
        }, AssemblyBuilderAccess.Run);

        var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.ReturnHookModuleName);
        var hookType = hookModule.DefineType(DynamicTypeNames.ReturnHookTypeName, TypeAttributes.Public);
        var hookStaticField = hookType.DefineField(
            DynamicTypeNames.ReturnHookStaticFieldName,
            executable.GetType(),
            FieldAttributes.Private | FieldAttributes.Static);

        var hookMethodParameterTypes = GetParameterTypes(hookMethodType, originalMethod);

        var hookMethod = hookType.DefineMethod(
            DynamicTypeNames.ReturnHookMethodName,
            GetMethodAttributes(hookMethodType),
            hookReturnType,
            hookMethodParameterTypes);

        return GetHookedMethod(
            hookType,
            hookMethod,
            hookStaticField,
            OpCodes.Ret,
            executable,
            hookMethodType,
            itParameterExpressions,
            il =>
            {
                var executableType = executable.GetType();

                if (executableType.IsGenericType)
                {
                    executableType = executableType.GetGenericTypeDefinition();
                }

                if (executableType != typeof(Action) && executableType != typeof(Func<>))
                {
#if NETFRAMEWORK
                    for (var i = hookMethodType == HookMethodType.Static ? 0 : 1; i < hookMethodParameterTypes.Length; i++)
#else
                    for (var i = 0; i < hookMethodParameterTypes.Length; i++)
#endif
                    {
                        il.Emit(OpCodes.Ldarg, hookMethodType == HookMethodType.Static ? i : i + 1);
                    }
                }
                
                il.Emit(OpCodes.Callvirt, executable.GetType().GetMethod("Invoke") ?? throw new Exception($"Method 'Invoke' of type {executableType} not found"));
            });
    }

    private static MethodInfo GetHookedMethod<THookValue>(
        TypeBuilder hookType,
        MethodBuilder hookMethod,
        FieldInfo hookStaticField,
        OpCode endingOpCode,
        THookValue hookValue,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions,
        Action<ILGenerator>? setupHookIl = null)
    {
        var hookMethodIl = hookMethod.GetILGenerator();

        SetupIlExpressionCall(hookMethodIl, hookType, hookMethodType, itParameterExpressions);

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        setupHookIl?.Invoke(hookMethodIl);
        hookMethodIl.Emit(endingOpCode);

        var type = hookType.CreateTypeInfo() ?? throw new Exception($"{hookType} builder can't create dynamic type");
        var hookFieldInfo = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                            throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");

        hookFieldInfo.SetValue(null, hookValue);

        SetupExpressionCallImplementation(type, itParameterExpressions);

        return type.GetMethod(hookMethod.Name, GetBindingFlags(hookMethodType)) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }

    private static void SetupIlExpressionCall(
        ILGenerator hookMethodIl,
        TypeBuilder hookType,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        for (var i = 0; i < itParameterExpressions.Count; i++)
        {
            var parameterExpression = itParameterExpressions[i].ParameterExpression;

            if (parameterExpression != null)
            {
                var expressionStaticFiled = hookType.DefineField(
                    $"{DynamicTypeNames.ExpressionStaticFieldName}{i}",
                    parameterExpression.Type,
                    FieldAttributes.Private | FieldAttributes.Static);

                hookMethodIl.Emit(OpCodes.Ldsfld, expressionStaticFiled);
                hookMethodIl.Emit(
                    OpCodes.Ldarg,
#if NETFRAMEWORK
                    hookMethodType == HookMethodType.Static ? i : i + 2);
#else
                    hookMethodType == HookMethodType.Static ? i : i + 1);
#endif
                hookMethodIl.Emit(OpCodes.Callvirt, parameterExpression.Type.GetMethod("Invoke") ?? throw new Exception($"Method 'Invoke' of type {parameterExpression.Type} not found"));
            }
        }
    }

    private static void SetupExpressionCallImplementation(
        Type type,
        IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        for (var i = 0; i < itParameterExpressions.Count; i++)
        {
            var parameterExpression = itParameterExpressions[i].ParameterExpression;

            if (parameterExpression != null)
            {
                var expressionFieldInfo = type.GetField(
                                              $"{DynamicTypeNames.ExpressionStaticFieldName}{i}",
                                              BindingFlags.Static | BindingFlags.NonPublic) ??
                                          throw new Exception($"{DynamicTypeNames.ExpressionStaticFieldName}{i} not found in type {type.Name}");

                expressionFieldInfo.SetValue(null, parameterExpression.Compile());
            }
        }
    }

    private static MethodAttributes GetMethodAttributes(HookMethodType hookMethodType)
    {
        var methodAttributes = MethodAttributes.Public;

        switch (hookMethodType)
        {
            case HookMethodType.Static:
                methodAttributes |= MethodAttributes.Static;
                break;
            case HookMethodType.Instance:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(hookMethodType), hookMethodType, $"Method type {hookMethodType} isn't exists in {nameof(HookMethodType)}");
        }

        return methodAttributes;
    }

    private static BindingFlags GetBindingFlags(HookMethodType hookMethodType)
    {
        var bindingFlags = BindingFlags.Public;

        switch (hookMethodType)
        {
            case HookMethodType.Static:
                bindingFlags |= BindingFlags.Static;
                break;
            case HookMethodType.Instance:
                bindingFlags |= BindingFlags.Instance;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(hookMethodType), hookMethodType, $"Method type {hookMethodType} isn't exists in {nameof(HookMethodType)}");
        }

        return bindingFlags;
    }

    private static Type[] GetParameterTypes(HookMethodType hookMethodType, MethodBase originalMethod)
    {
        var originalMethodParameters = originalMethod.GetParameters().Select(x => x.ParameterType);

        return hookMethodType switch
        {
            HookMethodType.Static => originalMethodParameters.ToArray(),
#if NETFRAMEWORK
            HookMethodType.Instance when originalMethod.DeclaringType != null => [originalMethod.DeclaringType, .. originalMethodParameters],
#else
            HookMethodType.Instance => originalMethodParameters.ToArray(),
#endif
            
            _ => throw new ArgumentOutOfRangeException(nameof(hookMethodType), hookMethodType, $"Method type {hookMethodType} isn't exists in {nameof(HookMethodType)}")
        };
    }
}