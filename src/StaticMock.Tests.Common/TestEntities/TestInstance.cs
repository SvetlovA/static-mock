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

    private int PrivateIntProperty { get; set; }
    private object PrivateObjectProperty { get; set; }

    private int TestPrivateMethodReturn1WithoutParameters()
    {
        return 1;
    }

    public bool Equals(TestInstance other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IntProperty == other.IntProperty && Equals(ObjectProperty, other.ObjectProperty) && PrivateIntProperty == other.PrivateIntProperty && Equals(PrivateObjectProperty, other.PrivateObjectProperty);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TestInstance)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = IntProperty;
            hashCode = (hashCode * 397) ^ (ObjectProperty != null ? ObjectProperty.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ PrivateIntProperty;
            hashCode = (hashCode * 397) ^ (PrivateObjectProperty != null ? PrivateObjectProperty.GetHashCode() : 0);
            return hashCode;
        }
    }
}