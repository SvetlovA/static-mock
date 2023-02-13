using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;

namespace StaticMock.Hooks.HookBuilders.Helpers;

internal static class TranspilerBuilderHelper
{
    public static IEnumerable<CodeInstruction> GetCallbackHookInstructions(
        MethodInfo originalMethodInfo,
        object callback,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        GetHookCodeInstructions(
            originalMethodInfo,
            callback,
            hookMethodType,
            itParameterExpressions);

    public static IEnumerable<CodeInstruction> GetReturnHookCodeInstructions<TReturn>(
        TReturn value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        GetHookCodeInstructions(
            value,
            hookMethodType,
            itParameterExpressions,
            typeof(TReturn));

    public static IEnumerable<CodeInstruction> GetReturnAsyncHookCodeInstructions<TReturn>(
        TReturn value,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        GetHookCodeInstructions(
            Task.FromResult(value),
            hookMethodType,
            itParameterExpressions,
            typeof(Task<TReturn>));

    public static IEnumerable<CodeInstruction> GetReturnHookCodeInstructions(
        MethodInfo originalMethodInfo,
        object getValue,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions) =>
        GetHookCodeInstructions(
            originalMethodInfo,
            getValue,
            hookMethodType,
            itParameterExpressions);

    public static IEnumerable<CodeInstruction> GetThrowsHookCodeInstructions<TException>(
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

        return GetHookedMethodCodeInstructions(
            hookType,
            hookStaticField,
            OpCodes.Throw,
            exception,
            hookMethodType,
            itParameterExpressions);
    }

    private static IEnumerable<CodeInstruction> GetHookCodeInstructions(
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

        return GetHookedMethodCodeInstructions(
            hookType,
            hookStaticField,
            OpCodes.Ret,
            value,
            hookMethodType,
            itParameterExpressions);
    }

    private static IEnumerable<CodeInstruction> GetHookCodeInstructions(
        MethodBase originalMethodInfo,
        object executable,
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
            executable.GetType(),
            FieldAttributes.Private | FieldAttributes.Static);

        var hookMethodParameterTypes = originalMethodInfo.GetParameters().Select(x => x.ParameterType).ToArray();

        return GetHookedMethodCodeInstructions(
            hookType,
            hookStaticField,
            OpCodes.Ret,
            executable,
            hookMethodType,
            itParameterExpressions,
            () =>
            {
                var codeInstructions = new List<CodeInstruction>(2);
                var executableType = executable.GetType();

                if (executableType.IsGenericType)
                {
                    executableType = executableType.GetGenericTypeDefinition();
                }


                if (executableType != typeof(Action) && executableType != typeof(Func<>))
                {
                    for (var i = 0; i < hookMethodParameterTypes.Length; i++)
                    {
                        codeInstructions.Add(
                            new CodeInstruction(OpCodes.Ldarg, hookMethodType == HookMethodType.Static ? i : i + 1));
                    }
                }

                codeInstructions.Add(
                    new CodeInstruction(OpCodes.Callvirt, executable.GetType().GetMethod("Invoke")));

                return codeInstructions;
            });
    }

    private static IEnumerable<CodeInstruction> GetHookedMethodCodeInstructions<THookValue>(
        TypeBuilder hookType,
        FieldInfo hookStaticField,
        OpCode endingOpCode,
        THookValue hookValue,
        HookMethodType hookMethodType,
        IReadOnlyList<ItParameterExpression> itParameterExpressions,
        Func<IEnumerable<CodeInstruction>>? getHookCodeInstructions = null)
    {
        foreach (var codeInstruction in GetExpressionCallCodeInstructions(hookType, hookMethodType, itParameterExpressions))
        {
            yield return codeInstruction;
        }

        yield return new CodeInstruction(OpCodes.Ldsfld, hookStaticField);

        if (getHookCodeInstructions != null)
        {
            foreach (var codeInstruction in getHookCodeInstructions())
            {
                yield return codeInstruction;
            }
        }

        yield return new CodeInstruction(endingOpCode);

        var type = hookType.CreateTypeInfo() ?? throw new Exception($"{hookType} builder can't create dynamic type");;
        var hookFieldInfo = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                            throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");

        hookFieldInfo.SetValue(null, hookValue);

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

    private static IEnumerable<CodeInstruction> GetExpressionCallCodeInstructions(
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

                yield return new CodeInstruction(OpCodes.Ldsfld, expressionStaticFiled);
                yield return new CodeInstruction(
                    OpCodes.Ldarg,
                    hookMethodType == HookMethodType.Static ? i : i + 1);
                yield return new CodeInstruction(OpCodes.Callvirt, parameterExpression.Type.GetMethod("Invoke"));
            }
        }
    }
}