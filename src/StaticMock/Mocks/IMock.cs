using System;

namespace StaticMock.Mocks;

public interface IMock
{
    IDisposable Throws(Type exceptionType);
    IDisposable Throws(Type exceptionType, params object[] constructorArgs);
    IDisposable Throws<TException>() where TException : Exception, new();
    IDisposable Throws<TException>(object[] constructorArgs) where TException : Exception;
}