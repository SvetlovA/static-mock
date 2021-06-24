using System;
using StaticMock.Services.Common;

namespace StaticMock.Services.Throws
{
    internal interface IThrowsService : IReturnable
    {
        IReturnable Throws(Type exceptionType);
        IReturnable Throws<TException>() where TException : Exception, new();
    }
}
