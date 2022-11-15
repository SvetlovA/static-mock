using System.Reflection;
using NUnit.Framework;
using StaticMock.Entities;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.ReturnsTests;

[TestFixture]
public class MockReturnsTests
{
    [Test]
    public void TestReturnsWithTestMethodReturn1WithoutParameters()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithoutParameters),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodReturnParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturnWithParameter),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodReturnInstanceObject()
    {
        var originalResult = TestStaticClass.TestMethodReturnReferenceObject();
        var expectedResult = new TestInstance
        {
            IntProperty = 1,
            ObjectProperty = new object()
        };

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturnReferenceObject),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodOutParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithOutParameter),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodRefParameter()
    {
        var x = 1;
        var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithRefParameter),
                () =>
                {
                    var y = 1;
                    var actualResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref y);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestInstanceMethodReturns()
    {
        var testInstance = new TestInstance();
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupReturnsAsyncMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupReturnsAsyncMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsAsyncMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsAsyncMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public void TestReturnsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        Assert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
        {
            var actualResult = instance.IntProperty;
            Assert.AreEqual(2, actualResult);
        }).Returns(2);
    }

    [Test]
    public void TestReturnsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        Assert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
        {
            var actualResult = instance.ObjectProperty;
            Assert.AreEqual(typeof(int), actualResult);
        }).Returns(typeof(int));
    }

    [Test]
    public void TestInstancePrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        MethodInfo methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.AreEqual(1, methodInfo.Invoke(testInstance, new object[] { }));
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), methodInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsPrivateIntProperty()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateIntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(testInstance, new object[] { });
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsPrivateObjectProperty()
    {

        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateObjectProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(testInstance, new object[] { });
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }


    [Test]
    public void TestReturnsWithTestPrivateStaticMethodReturn1WithoutParameters()
    {
        Type type = typeof(TestStaticClass);
        MethodInfo methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);

        var originalResult = methodInfo.Invoke(type, new object[] { });
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                methodInfo.Name,
                BindingFlags.Static | BindingFlags.NonPublic,
                () =>
                {
                    var actualResult = methodInfo.Invoke(type, new object[] { });

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsPrivateStaticIntProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            var actualResult = mothodInfo.Invoke(type, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsPrivateStaticObjectProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticObjectProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            var actualResult = mothodInfo.Invoke(type, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } },
            () =>
            {
                var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } },
            () =>
            {
                var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } },
            () =>
            {
                var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }
}