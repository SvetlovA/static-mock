﻿namespace StaticMock.Tests.Tests.ThrowsTests.Entities;

internal class CustomExceptionWithoutDefaultConstructor : Exception
{
    public CustomExceptionWithoutDefaultConstructor(string customMessage) : base(customMessage) { }
}