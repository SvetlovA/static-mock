using System.Reflection;
using System.Reflection.Emit;

namespace StaticMock.Hooks.Helpers;

internal static class HookBuilder
{
    public static MethodInfo CreateReturnHook<TReturn>(TReturn value)
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = "ReturnHookAssembly"
        }, AssemblyBuilderAccess.Run);
        var hookModule = hookAssembly.DefineDynamicModule("ReturnHookModule");
        var hookType = hookModule.DefineType("ReturnHookType", TypeAttributes.Public);
        var hookField = hookType.DefineField("ReturnHookField", typeof(TReturn), FieldAttributes.Private | FieldAttributes.Static);
        var hookMethod = hookType.DefineMethod("ReturnHookMethod", MethodAttributes.Public | MethodAttributes.Static,
            typeof(TReturn), new [] { typeof(int) });
        var hookMethodIl = hookMethod.GetILGenerator();

        hookMethodIl.EmitWriteLine("Test");
        hookMethodIl.Emit(OpCodes.Ldsfld, hookField);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var field = type.GetField("ReturnHookField", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, value);

        return type.GetMethod("ReturnHookMethod", BindingFlags.Static | BindingFlags.Public);
    }

    public static MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new()
    {
        var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
        {
            Name = "ExceptionHookAssembly"
        }, AssemblyBuilderAccess.Run);
        var hookModule = hookAssembly.DefineDynamicModule("ExceptionHookModule");
        var hookType = hookModule.DefineType("ExceptionHookType", TypeAttributes.Public);
        var hookField = hookType.DefineField("ExceptionHookField", typeof(TException), FieldAttributes.Private | FieldAttributes.Static);
        var hookMethod = hookType.DefineMethod("ExceptionHookMethod", MethodAttributes.Public | MethodAttributes.Static);
        var hookMethodIl = hookMethod.GetILGenerator();

        hookMethodIl.Emit(OpCodes.Ldsfld, hookField);
        hookMethodIl.Emit(OpCodes.Throw);
        hookMethodIl.Emit(OpCodes.Ret);

        var type = hookType.CreateType();
        var field = type.GetField("ExceptionHookField", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, exception);

        return type.GetMethod("ExceptionHookMethod", BindingFlags.Static | BindingFlags.Public);
    }
}