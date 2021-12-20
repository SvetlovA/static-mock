using System;
using NUnit.Framework;
using StaticMock.Tests.TestEntities;
using StaticMock.Tests.ThrowsTests.Entities;

namespace StaticMock.Tests.ThrowsTests;

[TestFixture]
public class GenericMockThrowsTests
{
    [Test]
    public void TestThrowsTestMethodReturn1WithoutParameters()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
            }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsTestVoidMethodWithoutParameters()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
            }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsTestVoidMethodWithParameters()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
            }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsArgumentNullException()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
            {
                Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
            }).Throws<ArgumentNullException>();
    }

    [Test]
    public void TestInstanceMethodThrows()
    {
        var testInstance = new TestInstance();

        Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters(), () =>
        {
            Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTask()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturnTask(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTask());
        });
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskAsync()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturnTaskAsync(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskAsync());
        });
    }

    [Test]
    public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodReturnTaskAsync(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodReturnTaskAsync());
        });
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync());
        });
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParameters(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParameters());
        });
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTask()
    {
        var instance = new TestInstance();

        Mock.Setup(() => instance.TestMethodReturnTask(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
        });
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        Mock.Setup(() => instance.TestMethodReturnTaskAsync(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
        });
    }

    [Test]
    public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        Mock.Setup(() => instance.TestVoidMethodReturnTaskAsync(), () =>
        {
            Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
        });
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
    {
        var instance = new TestInstance();

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
        });
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
    {
        var instance = new TestInstance();

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters(), () =>
        {
            Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
        });
    }

    [Test]
    public void TestThrowsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        Assert.AreEqual(default(int), originalValue);

        Mock.Setup(() => TestStaticClass.StaticIntProperty, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = TestStaticClass.StaticIntProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        Assert.AreEqual(default, originalValue);

        Mock.Setup(() => TestStaticClass.StaticObjectProperty, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = TestStaticClass.StaticObjectProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        Assert.AreEqual(default(int), originalValue);

        Mock.Setup(() => instance.IntProperty, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = instance.IntProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        Assert.AreEqual(default, originalValue);

        Mock.Setup(() => instance.ObjectProperty, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = instance.ObjectProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>(), () =>
        {
            Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters(), () =>
        {
            Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsParameterLessCustomExceptionTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                Assert.Throws<CustomExceptionWithoutDefaultConstructor>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
            }).Throws<CustomExceptionWithoutDefaultConstructor>(new object[] { message });
    }

    [Test]
    public void TestThrowsExceptionWithMessageTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
        }).Throws<Exception>(new object[] { message });
    }
}