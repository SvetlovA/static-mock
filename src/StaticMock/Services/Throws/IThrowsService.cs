using System;
using StaticMock.Services.Common;

namespace StaticMock.Services.Throws;

internal interface IThrowsService
{
    IReturnable Throws(Type exceptionType, object[] constructorArgs);
    IReturnable Throws<TException>() where TException : Exception, new();
}