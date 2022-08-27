﻿using System.Reflection;
using System.Reflection.Emit;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;

namespace StaticMock.Hooks.HookBuilders.Helpers;

internal static class HookBuilderHelper
{
    public static MethodInfo CreateVoidHook(
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = DynamicTypeNames.VoidHookAssemblyName
        }, AssemblyBuilderAccess.Run);

        var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.VoidHookModuleName);
        var hookType = hookModule.DefineType(DynamicTypeNames.VoidHookTypeName, TypeAttributes.Public);

        var hookMethod = hookType.DefineMethod(
            DynamicTypeNames.VoidHookMethodName,
            GetMethodAttributes(hookMethodType),
            typeof(void),
            itParameterExpressions.Select(x => x.ParameterType).ToArray());

        var hookMethodIl = hookMethod.GetILGenerator();

        SetupIlExpressionCall(hookMethodIl, hookType, hookMethodType, itParameterExpressions);

        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();

        SetupExpressionCallImplementation(type, itParameterExpressions);

        return type.GetMethod(hookMethod.Name, GetBindingFlags(hookMethodType)) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }

    public static MethodInfo CreateReturnHook<TReturn>(
        TReturn value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = DynamicTypeNames.ReturnHookAssemblyName
        }, AssemblyBuilderAccess.Run);

        var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.ReturnHookModuleName);
        var hookType = hookModule.DefineType(DynamicTypeNames.ReturnHookTypeName, TypeAttributes.Public);
        var hookStaticField = hookType.DefineField(
            DynamicTypeNames.ReturnHookStaticFieldName,
            typeof(TReturn),
            FieldAttributes.Private | FieldAttributes.Static);

        var hookMethod = hookType.DefineMethod(
            DynamicTypeNames.ReturnHookMethodName,
            GetMethodAttributes(hookMethodType),
            typeof(TReturn),
            itParameterExpressions.Select(x => x.ParameterType).ToArray());

        return GetHookedMethod(
            hookType,
            hookMethod,
            hookStaticField,
            OpCodes.Ret,
            value,
            hookMethodType,
            itParameterExpressions);
    }

    public static MethodInfo CreateThrowsHook<TException>(
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
            typeof(void),
            itParameterExpressions.Select(x => x.ParameterType).ToArray());

        return GetHookedMethod(
            hookType,
            hookMethod,
            hookStaticField,
            OpCodes.Throw,
            exception,
            hookMethodType,
            itParameterExpressions);
    }

    private static MethodInfo GetHookedMethod<THookValue>(
        TypeBuilder hookType,
        MethodBuilder hookMethod,
        FieldInfo hookStaticField,
        OpCode endingOpCode,
        THookValue hookValue,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        var hookMethodIl = hookMethod.GetILGenerator();

        SetupIlExpressionCall(hookMethodIl, hookType, hookMethodType, itParameterExpressions);

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        hookMethodIl.Emit(endingOpCode);

        var type = hookType.CreateType();
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
                    hookMethodType == HookMethodType.Static ? i : i + 1);
                hookMethodIl.Emit(OpCodes.Callvirt, parameterExpression.Type.GetMethod("Invoke"));
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
}