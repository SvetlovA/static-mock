using System.Collections.Concurrent;
using System.Reflection;
using HarmonyLib;

namespace StaticMock.Hooks.Implementation;

internal class HarmonyHookManager : IHookManager
{
    private static readonly IDictionary<MethodBase, IHookManager> HookManagerMap = new ConcurrentDictionary<MethodBase, IHookManager>();

    private readonly string _harmonyId = $"{nameof(HarmonyHookManager)}{Guid.NewGuid()}";
    private readonly PatchProcessor _patchProcessor;
    private readonly MethodBase _originalMethod;

    private IHookManager? _hookManagerToApply;
    private MethodInfo? _transpiler;

    public HarmonyHookManager(MethodBase originalMethod)
    {
        var harmony = new Harmony(_harmonyId);
        _patchProcessor = harmony.CreateProcessor(originalMethod);
        _originalMethod = originalMethod;
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        if (HookManagerMap.TryGetValue(_originalMethod, out var hookManager))
        {
            hookManager.Return();
            _hookManagerToApply = hookManager;
            _transpiler = transpiler;
        }

        _patchProcessor.AddTranspiler(new HarmonyMethod(transpiler));
        _patchProcessor.Patch();

        HookManagerMap[_originalMethod] = this;

        return this;
    }

    public void Return()
    {
        _patchProcessor.Unpatch(HarmonyPatchType.Transpiler, _harmonyId);
    }

    public void Dispose()
    {
        Return();

        if (_hookManagerToApply == null)
        {
            HookManagerMap.Remove(_originalMethod);
        }
        else
        {
            _hookManagerToApply.ApplyHook(_transpiler);
        }
    }
}