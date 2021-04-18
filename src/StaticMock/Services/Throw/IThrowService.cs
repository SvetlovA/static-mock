using System;

namespace StaticMock.Services.Throw
{
    internal interface IThrowService
    {
        void Throws(Type exceptionType);
        void Throws<TException>() where TException : Exception, new();
    }
}
