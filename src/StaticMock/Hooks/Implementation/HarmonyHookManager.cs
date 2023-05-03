using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using HarmonyLib;
using Patch = StaticMock.Hooks.Entities.Patch;

namespace StaticMock.Hooks.Implementation;

internal class HarmonyHookManager : IHookManager
{
    private static readonly IDictionary<MethodBase, Stack<Patch>> PatchMap = new ConcurrentDictionary<MethodBase, Stack<Patch>>();

    private readonly string _harmonyId = $"{nameof(HarmonyHookManager)}{Guid.NewGuid()}";
    private readonly MethodBase _originalMethod;
    private readonly Patch _patch;

    public HarmonyHookManager(MethodBase originalMethod)
    {
        _originalMethod = originalMethod;
        _patch = new Patch
        {
            PatchProcessor = new Harmony(_harmonyId).CreateProcessor(originalMethod),
            HarmonyId = _harmonyId
        };
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        if (_patch.PatchProcessor == null)
        {
            throw new ArgumentNullException(nameof(_patch.PatchProcessor));
        }

        Monitor.Enter(_originalMethod);
        if (PatchMap.TryGetValue(_originalMethod, out var patchStack))
        {
            if (patchStack.Count > 0)
            {
                var patch = patchStack.Peek();
                patch.PatchProcessor?.Unpatch(HarmonyPatchType.Transpiler, patch.HarmonyId);
            }
        }

        _patch.PatchProcessor.AddTranspiler(new HarmonyMethod(transpiler));
        _patch.PatchProcessor.Patch();
        _patch.Transpiler = transpiler;

        if (PatchMap.TryGetValue(_originalMethod, out var patchesStack))
        {
            patchesStack.Push(_patch);
        }
        else
        {
            PatchMap[_originalMethod] = new Stack<Patch>(new [] { _patch });
        }

        return this;
    }

    public void Return()
    {
        if (PatchMap.TryGetValue(_originalMethod, out var patchStack))
        {
            if (patchStack.Count > 0)
            {
                var patchToReturn = patchStack.Pop();
                patchToReturn.PatchProcessor?.Unpatch(HarmonyPatchType.Transpiler, patchToReturn.HarmonyId);
                if (patchStack.Count > 0)
                {
                    var patchToApply = patchStack.Peek();
                    patchToApply.PatchProcessor?.AddTranspiler(new HarmonyMethod(patchToApply.Transpiler));
                    patchToApply.PatchProcessor?.Patch();
                }
            }
            else
            {
                PatchMap.Remove(_originalMethod);
            }
        }

        Monitor.Exit(_originalMethod);
    }

    public void Dispose()
    {
        Return();
    }
}