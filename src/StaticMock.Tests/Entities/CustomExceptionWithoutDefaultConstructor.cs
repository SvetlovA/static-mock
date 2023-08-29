namespace StaticMock.Tests.Entities;

internal class CustomExceptionWithoutDefaultConstructor : Exception
{
    public CustomExceptionWithoutDefaultConstructor(string customMessage) : base(customMessage) { }
}