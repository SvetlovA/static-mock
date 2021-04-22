using System;

namespace StaticMock.Services.Injection
{
    internal interface IReturnable : IDisposable
    {
        void Return();
    }
}
