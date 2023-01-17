using System;
using System.Threading.Tasks;

namespace StaticMock.Tests.Common.TestEntities;

public class TestInstance : IEquatable<TestInstance>
{
    public int IntProperty { get; set; }
    public object ObjectProperty { get; set; }

    public int TestMethodReturn1WithoutParameters()
    {
        return 1;
    }

    public int TestMethodReturnWithParameter(int parameter)
    {
        return parameter;
    }

    public int TestMethodReturnWithParameters(int parameter, int[] arrayParameter)
    {
        return parameter;
    }

    public TEntity GenericTestMethodReturnDefaultWithoutParameters<TEntity>()
    {
        return default;
    }

    public void TestVoidMethodWithoutParametersThrowsException()
    {
        throw new Exception("Test exception");
    }

    public Task TestMethodReturnTask()
    {
        return Task.CompletedTask;
    }

    public async Task TestMethodReturnTaskAsync()
    {
        await Task.CompletedTask;
    }

    public async void TestVoidMethodReturnTaskAsync()
    {
        await Task.CompletedTask;
    }

    public async Task<int> TestMethodReturnTaskWithoutParametersAsync()
    {
        return await Task.FromResult(1);
    }

    public Task<int> TestMethodReturnTaskWithoutParameters()
    {
        return Task.FromResult(1);
    }

    public bool Equals(TestInstance other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IntProperty == other.IntProperty && Equals(ObjectProperty, other.ObjectProperty);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TestInstance)obj);
    }

    private int PrivateIntProperty { get; set; }
    private object PrivateObjectProperty { get; set; }

    private int TestPrivateMethodReturn1WithoutParameters()
    {
        return 1;
    }
}