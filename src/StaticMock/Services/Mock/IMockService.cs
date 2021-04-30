using System;

namespace StaticMock.Services.Mock
{
    public interface IMockService
    {
        void Throws(Type exceptionType);
        void Throws<TException>() where TException : Exception, new();
    }
}
