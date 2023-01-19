using System.Reflection;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerX32 : IHookManager
{
    private FunctionMemoryInfoX32 _functionMemoryInfoX32;

    private readonly MethodBase _originalMethod;

    public HookManagerX32(MethodBase originalMethod)
    {
        _originalMethod = originalMethod;
    }

    public unsafe IReturnable ApplyHook(MethodInfo transpiler)
    {
        var methodPtr = (byte*) _originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        SaveMethodMemoryInfo(methodPtr);

        //push replacementSite
        *methodPtr = 0x68;
        *(uint*) (methodPtr + 1) = (uint) transpiler.MethodHandle.GetFunctionPointer().ToInt32();

        //ret
        *(methodPtr + 5) = 0xC3;

        return this;
    }

    public unsafe void Return()
    {
        var methodPtr = (byte*) _originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        *methodPtr = _functionMemoryInfoX32.Byte1;
        *(uint*) (methodPtr + 1) = _functionMemoryInfoX32.FunctionMemoryValue;
        *(methodPtr + 5) = _functionMemoryInfoX32.Byte1AfterFunction;
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _functionMemoryInfoX32.Byte1 = *methodPtr;
        _functionMemoryInfoX32.FunctionMemoryValue = *(uint*) (methodPtr + 1);
        _functionMemoryInfoX32.Byte1AfterFunction = *(methodPtr + 5);
    }
}