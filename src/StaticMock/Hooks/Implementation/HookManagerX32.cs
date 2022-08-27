using System.Reflection;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerX32 : IHookManager
{
    private MethodMemoryInfoX32 _methodMemoryInfoX32;

    private readonly MethodBase _originalMethod;

    public HookManagerX32(MethodBase method)
    {
        _originalMethod = method ?? throw new ArgumentNullException(nameof(method));
    }

    public unsafe IReturnable ApplyHook(MethodBase hookMethod)
    {
        if (hookMethod == null)
        {
            throw new ArgumentNullException(nameof(hookMethod));
        }

        var methodPtr = (byte*) _originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        SaveMethodMemoryInfo(methodPtr);

        //push replacementSite
        *methodPtr = 0x68;
        *(uint*) (methodPtr + 1) = (uint) hookMethod.MethodHandle.GetFunctionPointer().ToInt32();

        //ret
        *(methodPtr + 5) = 0xC3;

        return this;
    }

    public unsafe void Return()
    {
        var methodPtr = (byte*) _originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        *methodPtr = _methodMemoryInfoX32.Byte1;
        *(uint*) (methodPtr + 1) = _methodMemoryInfoX32.MethodMemoryValue;
        *(methodPtr + 5) = _methodMemoryInfoX32.Byte1AfterMethod;
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _methodMemoryInfoX32.Byte1 = *methodPtr;
        _methodMemoryInfoX32.MethodMemoryValue = *(uint*) (methodPtr + 1);
        _methodMemoryInfoX32.Byte1AfterMethod = *(methodPtr + 5);
    }
}