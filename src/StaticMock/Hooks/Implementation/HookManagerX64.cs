using System.Reflection;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerX64 : IHookManager
{
    private MethodMemoryInfoX64 _methodMemoryInfoX64;

    private readonly MethodBase _method;

    public HookManagerX64(MethodBase method)
    {
        _method = method ?? throw new ArgumentNullException(nameof(method));
    }

    public unsafe IReturnable ApplyHook(MethodBase hookMethod)
    {
        if (hookMethod == null)
        {
            throw new ArgumentNullException(nameof(hookMethod));
        }

        var methodPtr = (byte*)_method.MethodHandle.GetFunctionPointer().ToPointer();

        SaveMethodMemoryInfo(methodPtr);

        // mov r11, replacement
        *methodPtr = 0x49;
        *(methodPtr + 1) = 0xBB;
        *(ulong*)(methodPtr + 2) = (ulong)hookMethod.MethodHandle.GetFunctionPointer().ToInt64();
        // jmp r11
        *(methodPtr + 10) = 0x41;
        *(methodPtr + 11) = 0xFF;
        *(methodPtr + 12) = 0xE3;

        return this;
    }

    public unsafe void Return()
    {
        var methodPtr = (byte*)_method.MethodHandle.GetFunctionPointer().ToPointer();

        *methodPtr = _methodMemoryInfoX64.Byte1;
        *(methodPtr + 1) = _methodMemoryInfoX64.Byte2;
        *(ulong*)(methodPtr + 2) = _methodMemoryInfoX64.MethodMemoryValue;
        *(methodPtr + 10) = _methodMemoryInfoX64.Byte1AfterMethod;
        *(methodPtr + 11) = _methodMemoryInfoX64.Byte2AfterMethod;
        *(methodPtr + 12) = _methodMemoryInfoX64.Byte3AfterMethod;
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _methodMemoryInfoX64.Byte1 = *methodPtr;
        _methodMemoryInfoX64.Byte2 = *(methodPtr + 1);
        _methodMemoryInfoX64.MethodMemoryValue = *(ulong*)(methodPtr + 2);
        _methodMemoryInfoX64.Byte1AfterMethod = *(methodPtr + 10);
        _methodMemoryInfoX64.Byte2AfterMethod = *(methodPtr + 11);
        _methodMemoryInfoX64.Byte3AfterMethod = *(methodPtr + 12);
    }
}