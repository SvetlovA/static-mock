using System.Reflection;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerX64 : IHookManager
{
    private FunctionMemoryInfoX64 _functionMemoryInfoX64;

    private readonly MethodBase _originalMethod;

    public HookManagerX64(MethodBase originalMethod)
    {
        _originalMethod = originalMethod;
    }

    public unsafe IReturnable ApplyHook(MethodInfo transpiler)
    {
        var methodPtr = (byte*)_originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        SaveMethodMemoryInfo(methodPtr);

        // mov r11, replacement
        *methodPtr = 0x49;
        *(methodPtr + 1) = 0xBB;
        *(ulong*)(methodPtr + 2) = (ulong)transpiler.MethodHandle.GetFunctionPointer().ToInt64();
        // jmp r11
        *(methodPtr + 10) = 0x41;
        *(methodPtr + 11) = 0xFF;
        *(methodPtr + 12) = 0xE3;

        return this;
    }

    public unsafe void Return()
    {
        var methodPtr = (byte*)_originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        *methodPtr = _functionMemoryInfoX64.Byte1;
        *(methodPtr + 1) = _functionMemoryInfoX64.Byte2;
        *(ulong*)(methodPtr + 2) = _functionMemoryInfoX64.FunctionMemoryValue;
        *(methodPtr + 10) = _functionMemoryInfoX64.Byte1AfterFunction;
        *(methodPtr + 11) = _functionMemoryInfoX64.Byte2AfterFunction;
        *(methodPtr + 12) = _functionMemoryInfoX64.Byte3AfterFunction;
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _functionMemoryInfoX64.Byte1 = *methodPtr;
        _functionMemoryInfoX64.Byte2 = *(methodPtr + 1);
        _functionMemoryInfoX64.FunctionMemoryValue = *(ulong*)(methodPtr + 2);
        _functionMemoryInfoX64.Byte1AfterFunction = *(methodPtr + 10);
        _functionMemoryInfoX64.Byte2AfterFunction = *(methodPtr + 11);
        _functionMemoryInfoX64.Byte3AfterFunction = *(methodPtr + 12);
    }
}