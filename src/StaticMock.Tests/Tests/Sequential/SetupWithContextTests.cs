﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential;

[TestFixture]
public class SetupWithContextTests
{
    [Test]
    public void TestReturnSetupItIsWithParameterPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1))).Returns(expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsWithParameterNegative()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)))
                   .Returns(default(int)))
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>())).Returns(expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsWithParametersPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0))).Returns(expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>());
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestReturnSetupItIsWithParametersNegative()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
                       context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0))).Returns(default(int)))
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameters(2, new[] { 0 }));
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestReturnSetupItIsAnyWithParametersPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
                   context.It.IsAny<int>(), context.It.IsAny<int[]>())).Returns(expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameters(1, new[] { 0 });
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1))).Returns<int>(_ => expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterNegative()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)))
                   .Returns<int>(_ => default))
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>())).Returns<int>(_ => expectedValue))
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsWithParameterPositive()
    {
        const int expectedValue = 2;

        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1))).Returns<int>(_ => expectedValue))
        {
            var actualValue = instance.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsWithParameterNegative()
    {
        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)))
                   .Returns<int>(_ => default))
        {
            Assert.Throws<Exception>(() => instance.TestMethodReturnWithParameter(2));
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.IsAny<int>())).Returns<int>(_ => expectedValue))
        {
            var actualValue = instance.TestMethodReturnWithParameter(1);
            ClassicAssert.AreEqual(expectedValue, actualValue);
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterPositive()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
                           context.It.Is<int>(x => x == 1)))
                   .Throws<ArgumentException>())
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterNegative()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)))
                   .Throws<ArgumentException>())
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsAnyWithParameterPositive()
    {
        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));

        using (Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()))
                   .Throws<ArgumentException>())
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }

        ClassicAssert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsWithParametersPositive()
    {
        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameters(
                       context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0))).Throws<ArgumentException>())
        {
            Assert.Throws<ArgumentException>(
                () => instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsWithParametersNegative()
    {
        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameters(
                           context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0)))
                   .Throws<ArgumentException>())
        {
            Assert.Throws<Exception>(() => instance.TestMethodReturnWithParameters(2, new[] { 0 }));
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsAnyWithParametersPositive()
    {
        var instance = new TestInstance();
        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));

        using (Mock.Setup(context => instance.TestMethodReturnWithParameters(
                       context.It.IsAny<int>(), context.It.IsAny<int[]>())).Throws<ArgumentException>())
        {
            Assert.Throws<ArgumentException>(
                () => instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        }

        ClassicAssert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }
}