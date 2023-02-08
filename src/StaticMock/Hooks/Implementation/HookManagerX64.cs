using System.Reflection;
using System.Runtime.InteropServices;
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
        var methodPtr = _originalMethod.MethodHandle.GetFunctionPointer();

        SaveMethodMemoryInfo((byte*)methodPtr.ToPointer());

        // mov r11, replacement
        Marshal.WriteByte(methodPtr, 0x49);
        Marshal.WriteByte(methodPtr, 1, 0xBB);
        Marshal.WriteIntPtr(methodPtr, 2, transpiler.MethodHandle.GetFunctionPointer());
        // jmp r11
        Marshal.WriteByte(methodPtr, 10, 0x41);
        Marshal.WriteByte(methodPtr, 11, 0xFF);
        Marshal.WriteByte(methodPtr, 12, 0xE3);

        return this;
    }

    public void Return()
    {
        var methodPtr = _originalMethod.MethodHandle.GetFunctionPointer();

        Marshal.WriteByte(methodPtr, _functionMemoryInfoX64.Byte1);
        Marshal.WriteByte(methodPtr, 1, _functionMemoryInfoX64.Byte2);
        Marshal.WriteInt64(methodPtr, 2, _functionMemoryInfoX64.FunctionMemoryValue);
        Marshal.WriteByte(methodPtr, 10, _functionMemoryInfoX64.Byte1AfterFunction);
        Marshal.WriteByte(methodPtr, 11, _functionMemoryInfoX64.Byte2AfterFunction);
        Marshal.WriteByte(methodPtr, 12, _functionMemoryInfoX64.Byte3AfterFunction);
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _functionMemoryInfoX64.Byte1 = *methodPtr;
        _functionMemoryInfoX64.Byte2 = *(methodPtr + 1);
        _functionMemoryInfoX64.FunctionMemoryValue = *(long*)(methodPtr + 2);
        _functionMemoryInfoX64.Byte1AfterFunction = *(methodPtr + 10);
        _functionMemoryInfoX64.Byte2AfterFunction = *(methodPtr + 11);
        _functionMemoryInfoX64.Byte3AfterFunction = *(methodPtr + 12);
    }
}