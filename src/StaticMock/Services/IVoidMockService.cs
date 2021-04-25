using System;

namespace StaticMock.Services
{
    public interface IVoidMockService
    {
        void Throws(Type exceptionType);
        void Throws<TException>() where TException : Exception, new();
    }
}
