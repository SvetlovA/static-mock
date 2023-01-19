using System.Reflection;
using HarmonyLib;

namespace StaticMock.Hooks.Implementation;

internal class HarmonyHookManager : IHookManager
{
    private readonly string _harmonyId = $"{nameof(HarmonyHookManager)}{Guid.NewGuid()}";
    private readonly PatchProcessor _patchProcessor;

    public HarmonyHookManager(MethodBase originalMethod)
    {
        var harmony = new Harmony(_harmonyId);
        _patchProcessor = harmony.CreateProcessor(originalMethod);
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        _patchProcessor.AddTranspiler(new HarmonyMethod(transpiler));
        _patchProcessor.Patch();

        return this;
    }

    public void Return()
    {
        _patchProcessor.Unpatch(HarmonyPatchType.Transpiler, _harmonyId);
    }

    public void Dispose()
    {
        Return();
    }

    //public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    //{
    //    var hookAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
    //    {
    //        Name = DynamicTypeNames.ReturnHookAssemblyName
    //    }, AssemblyBuilderAccess.Run);

    //    var hookModule = hookAssembly.DefineDynamicModule(DynamicTypeNames.ReturnHookModuleName);
    //    var hookType = hookModule.DefineType(DynamicTypeNames.ReturnHookTypeName, TypeAttributes.Public);
    //    var hookStaticField = hookType.DefineField(
    //        DynamicTypeNames.ReturnHookStaticFieldName,
    //        typeof(int),
    //        FieldAttributes.Private | FieldAttributes.Static);

    //    //var hookMethod = hookType.DefineMethod(
    //    //    DynamicTypeNames.ReturnHookMethodName,
    //    //    MethodAttributes.Public | MethodAttributes.Static,
    //    //    typeof(int),
    //    //    Array.Empty<Type>());

    //    yield return new CodeInstruction(OpCodes.Ldsfld, hookStaticField);
    //    yield return new CodeInstruction(OpCodes.Ret);

    //    var type = hookType.CreateType();
    //    var hookFieldInfo = type.GetField(hookStaticField.Name, BindingFlags.Static | BindingFlags.NonPublic) ??
    //                        throw new Exception($"{hookStaticField.Name} not found in type {hookType.Name}");

    //    hookFieldInfo.SetValue(null, 2);
    //}
}