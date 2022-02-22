using System.Reflection;
using System.Reflection.Emit;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Helpers;

internal static class HookBuilder
{
    public static MethodInfo CreateReturnHook<TReturn>(TReturn value, HookParameter[] parameterHooks)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = "ReturnHookAssembly"
        }, AssemblyBuilderAccess.Run);
        var hookModule = hookAssembly.DefineDynamicModule("ReturnHookModule");
        var hookType = hookModule.DefineType("ReturnHookType", TypeAttributes.Public);
        var hookStaticField = hookType.DefineField("ReturnHookStaticField", typeof(TReturn), FieldAttributes.Private | FieldAttributes.Static);
        var hookMethod = hookType.DefineMethod("ReturnHookMethod", MethodAttributes.Public | MethodAttributes.Static,
            typeof(TReturn), parameterHooks.Select(x => x.Type).ToArray());
        var hookMethodIl = hookMethod.GetILGenerator();

        for (var i = 0; i < parameterHooks.Length; i++)
        {
            var localVariable = hookMethodIl.DeclareLocal(parameterHooks[i].Type);
            hookMethodIl.Emit(OpCodes.Ldarg, i);
            hookMethodIl.Emit(OpCodes.Stloc, localVariable);
            hookMethodIl.EmitWriteLine($"Local variable value {localVariable}");
            var parameterHook = parameterHooks[i].Hook;
            if (parameterHook != null)
            {
                hookMethodIl.Emit(OpCodes.Ldarg, i);
                hookMethodIl.Emit(OpCodes.Call, parameterHook);
            }
        }

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var field = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                    throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");
        field.SetValue(null, value);

        return type.GetMethod(hookMethod.Name, BindingFlags.Static | BindingFlags.Public) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }

    public static MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new()
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = "ExceptionHookAssembly"
        }, AssemblyBuilderAccess.Run);
        var hookModule = hookAssembly.DefineDynamicModule("ExceptionHookModule");
        var hookType = hookModule.DefineType("ExceptionHookType", TypeAttributes.Public);
        var hookStaticField = hookType.DefineField("ExceptionHookStaticField", typeof(TException), FieldAttributes.Private | FieldAttributes.Static);
        var hookMethod = hookType.DefineMethod("ExceptionHookMethod", MethodAttributes.Public | MethodAttributes.Static);
        var hookMethodIl = hookMethod.GetILGenerator();

        hookMethodIl.Emit(OpCodes.Ldsfld, hookStaticField);
        hookMethodIl.Emit(OpCodes.Throw);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var field = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
                    throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");
        field.SetValue(null, exception);

        return type.GetMethod("ExceptionHookMethod", BindingFlags.Static | BindingFlags.Public) ??
               throw new Exception($"{hookMethod.Name} not found in type {hookType.Name}");
    }
}