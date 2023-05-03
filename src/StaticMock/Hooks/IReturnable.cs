using System;

namespace StaticMock.Hooks;

internal interface IReturnable : IDisposable
{
    void Return();
}