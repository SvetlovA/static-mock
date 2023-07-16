using System;

namespace StaticMock.Tests.Common.TestEntities;

public class TestInstanceWithoutDefaultEmptyConstructor
{
    public TestInstanceWithoutDefaultEmptyConstructor(TestInstance param1, int param2, string param3)
    { }

    public object ObjectProperty { get; set; }

    public int TestMethodReturn1WithoutParameters()
    {
        return 1;
    }

    public void TestVoidMethodWithoutParametersThrowsException()
    {
        throw new Exception("Test exception");
    }
}