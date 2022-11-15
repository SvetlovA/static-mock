namespace StaticMock.Tests.TestEntities;

public static class TestStaticClass
{
    public static int TestMethodReturn1WithoutParameters()
    {
        return 1;
    }

    public static TEntity GenericTestMethodReturnDefaultWithoutParameters<TEntity>()
    {
        return default;
    }

    public static TestInstance TestMethodReturnReferenceObject()
    {
        return new();
    }

    public static int TestMethodReturn1WithOutParameter(out int x)
    {
        x = 1;
        return 1;
    }

    public static int TestMethodReturn1WithRefParameter(ref int x)
    {
        x = 1;
        return 1;
    }

    public static void TestVoidMethodWithoutParameters()
    { }

    public static void TestVoidMethodWithoutParametersThrowsException()
    {
        throw new Exception("Test exception");
    }

    public static void TestVoidMethodWithParameters(int parameter)
    { }

    public static int TestMethodReturnWithParameter(int parameter)
    {
        return parameter;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7, TestInstance parameter8)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7, TestInstance parameter8, Func<int, int> parameter9)
    {
        return parameter1;
    }

    public static int TestMethodReturnWithParameters(int parameter, int[] arrayParameter)
    {
        return parameter;
    }

    public static int StaticIntProperty { get; set; }
    public static object StaticObjectProperty { get; set; }

    private static int PrivateStaticIntProperty { get; set; }
    private static object PrivateStaticObjectProperty { get; set; }

    private static int TestPrivateStaticMethodReturn1WithoutParameters()
    {
        return 1;
    }
}