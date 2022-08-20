using System.Reflection;
using System.Reflection.Emit;
using StaticMock.Entities.Context;

namespace StaticMock.Hooks.Helpers;

internal static class HookBuilder
{
    public static MethodInfo CreateReturnHook<TReturn>(
        TReturn value, IReadOnlyList<ItParameterExpression> itParameterExpressions)
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
            MethodAttributes.Public | MethodAttributes.Static,
            typeof(TReturn),
            itParameterExpressions.Select(x => x.ParameterType).ToArray());

        var hookMethodIl = hookMethod.GetILGenerator();

        SetupIlExpressionCall(hookMethodIl, hookType, itParameterExpressions);

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var hookFieldInfo = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                    throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");

        hookFieldInfo.SetValue(null, value);

        SetupExpressionCallImplementation(type, itParameterExpressions);

        return type.GetMethod(hookMethod.Name, BindingFlags.Static | BindingFlags.Public) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }

    public static MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new()
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
            MethodAttributes.Public | MethodAttributes.Static);

        var hookMethodIl = hookMethod.GetILGenerator();

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        hookMethodIl.Emit(OpCodes.Throw);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var field = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                    throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");

        field.SetValue(null, exception);

        return type.GetMethod(hookMethod.Name, BindingFlags.Static | BindingFlags.Public) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }

    private static void SetupIlExpressionCall(
        ILGenerator hookMethodIl, TypeBuilder hookType, IReadOnlyList<ItParameterExpression> itParameterExpressions)
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
                hookMethodIl.Emit(OpCodes.Ldarg, i);
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
}