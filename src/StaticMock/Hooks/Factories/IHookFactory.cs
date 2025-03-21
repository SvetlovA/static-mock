using System;
using System.Reflection;

namespace StaticMock.Hooks.Factories;

public interface IHookFactory
{
    IDisposable? CreateHook(MethodInfo transpiler);
}