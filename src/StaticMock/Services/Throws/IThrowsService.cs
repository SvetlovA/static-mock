using System;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Throws
{
    internal interface IThrowsService
    {
        IReturnable Throws(Type exceptionType);
        IReturnable Throws<TException>() where TException : Exception, new();
    }
}
