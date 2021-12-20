namespace StaticMock.Services.Mock;

public interface IMockService
{
    void Throws(Type exceptionType);
    void Throws(Type exceptionType, params object[] constructorArgs);
    void Throws<TException>() where TException : Exception, new();
    void Throws<TException>(object[] constructorArgs) where TException : Exception;
}