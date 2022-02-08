using StaticMock.Hooks;

namespace StaticMock.Mocks.Throws;

internal interface IThrowsMock
{
    IReturnable Throws(Type exceptionType, object[] constructorArgs);
    IReturnable Throws<TException>() where TException : Exception, new();
}