using System.Reflection;
using HarmonyLib;

namespace StaticMock.Hooks.Implementation;

internal class HarmonyHookManager : IHookManager
{
    //private static readonly SemaphoreSlim Semaphore = new(1);

    private readonly string _harmonyId = $"{nameof(HarmonyHookManager)}{Guid.NewGuid()}";
    private readonly PatchProcessor _patchProcessor;

    public HarmonyHookManager(MethodBase originalMethod)
    {
        var harmony = new Harmony(_harmonyId);
        _patchProcessor = harmony.CreateProcessor(originalMethod);
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        //Semaphore.Wait();

        _patchProcessor.AddTranspiler(new HarmonyMethod(transpiler));
        _patchProcessor.Patch();

        return this;
    }

    public void Return()
    {
        _patchProcessor.Unpatch(HarmonyPatchType.Transpiler, _harmonyId);

        //Semaphore.Release();
    }

    public void Dispose()
    {
        Return();
    }
}