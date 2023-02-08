using System.Reflection;
using System.Runtime.InteropServices;
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
        var methodPtr = _originalMethod.MethodHandle.GetFunctionPointer();//(byte*) _originalMethod.MethodHandle.GetFunctionPointer().ToPointer();

        SaveMethodMemoryInfo((byte*)methodPtr.ToPointer());

        //push replacementSite
        Marshal.WriteByte(methodPtr, 0x68);
        Marshal.WriteIntPtr(methodPtr, 1, methodPtr);
        //ret
        Marshal.WriteByte(methodPtr, 5, 0xC3);

        return this;
    }

    public unsafe void Return()
    {
        var methodPtr = _originalMethod.MethodHandle.GetFunctionPointer();

        Marshal.WriteByte(methodPtr, _functionMemoryInfoX32.Byte1);
        Marshal.WriteInt32(methodPtr, 1, _functionMemoryInfoX32.FunctionMemoryValue);
        Marshal.WriteByte(methodPtr, 5, _functionMemoryInfoX32.Byte1AfterFunction);
    }

    public void Dispose()
    {
        Return();
    }

    private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
    {
        _functionMemoryInfoX32.Byte1 = *methodPtr;
        _functionMemoryInfoX32.FunctionMemoryValue = *(int*) (methodPtr + 1);
        _functionMemoryInfoX32.Byte1AfterFunction = *(methodPtr + 5);
    }
}