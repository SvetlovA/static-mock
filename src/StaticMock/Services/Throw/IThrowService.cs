using System;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Throw
{
    internal interface IThrowService
    {
        IReturnable Throws(Type exceptionType);
        IReturnable Throws<TException>() where TException : Exception, new();
    }
}
