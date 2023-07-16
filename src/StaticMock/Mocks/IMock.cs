using System;

namespace StaticMock.Mocks;

public interface IMock
{
    void Throws(Type exceptionType);
    void Throws(Type exceptionType, params object[] constructorArgs);
    void Throws<TException>() where TException : Exception, new();
    void Throws<TException>(object[] constructorArgs) where TException : Exception;
}