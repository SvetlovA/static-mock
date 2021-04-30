using System;

namespace StaticMock.Services.Mock
{
    public interface IFuncMockService<TReturn> : IMockService
    {
        void Callback(Func<TReturn> callback);
        void Returns(TReturn value);
    }

    public interface IFuncMockService : IMockService
    {
        void Callback<TReturnValue>(Func<TReturnValue> callback);
        void Returns<TValue>(TValue value);
    }
}
