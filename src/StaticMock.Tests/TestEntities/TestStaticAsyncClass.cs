namespace StaticMock.Tests.TestEntities;

internal static class TestStaticAsyncClass
{
    public static Task TestMethodReturnTask()
    {
        return Task.CompletedTask;
    }

    public static async Task TestMethodReturnTaskAsync()
    {
        await Task.CompletedTask;
    }

    public static async void TestVoidMethodReturnTaskAsync()
    {
        await Task.CompletedTask;
    }

    public static async Task<int> TestMethodReturnTaskWithoutParametersAsync()
    {
        return await Task.FromResult(1);
    }

    public static Task<int> TestMethodReturnTaskWithoutParameters()
    {
        return Task.FromResult(1);
    }

    public static async Task TestMethodThrowsExceptionAsync()
    {
        await Task.CompletedTask;
        throw new Exception("Test exception");
    }

    public static Task TestMethodThrowsException()
    {
        throw new Exception("Test exception");
    }

    public static Task<int> TestMethodReturnWithParameterAsync(int parameter)
    {
        return Task.FromResult(parameter);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7, TestInstance parameter8)
    {
        return Task.FromResult(parameter1);
    }

    public static Task<int> TestMethodReturnWithParametersAsync(int parameter1, string parameter2, double parameter3, int[] parameter4, string[] parameter5, char parameter6, bool parameter7, TestInstance parameter8, Func<int, int> parameter9)
    {
        return Task.FromResult(parameter1);
    }
}