using System;
using System.Reflection;

namespace StaticMock.Services.Hook;

internal interface IHookBuilder
{
    MethodInfo CreateReturnHook<TReturn>(TReturn value);
    MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new();
}