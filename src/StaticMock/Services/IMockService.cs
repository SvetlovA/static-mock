using System;

namespace StaticMock.Services
{
    public interface IMockService
    {
        void Returns<TValue>(TValue value);
        void Throws(Type exceptionType);
        void Throws<TException>() where TException : Exception, new();
    }
}
