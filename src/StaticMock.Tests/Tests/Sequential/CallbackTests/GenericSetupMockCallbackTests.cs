using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential.CallbackTests;

[TestFixture]
public class GenericSetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException()).Callback(() =>
        {
            DoSomething();
        });

        TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
    }

    [Test]
    public void TestActionCallbackThrows()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException()).Callback(() => throw new Exception());
        Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException());
    }

    [Test]
    public void TestActionCallbackInstance()
    {
        var instance = new TestInstance();

        static void DoSomething() { }

        using var _ = Mock.Setup(() => instance.TestVoidMethodWithoutParametersThrowsException()).Callback(() =>
        {
            DoSomething();
        });

        instance.TestVoidMethodWithoutParametersThrowsException();
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWithParameters()
    {
        const int parameter1 = 10;

        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(parameter1)).Callback<int>(argument => 
        {
            ClassicAssert.AreEqual(parameter1, argument);
        });

        TestStaticClass.TestVoidMethodWithParameters(parameter1);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith2Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        using var _ = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()))
            .Callback<int, string>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
            });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith3Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>()))
            .Callback<int, string, double>((argument1, argument2, argument3) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
            });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2, parameter3);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith4Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>()))
            .Callback<int, string, double, int[]>((
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith5Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>()))
            .Callback<int, string, double, int[], string[]>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith6Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>()))
            .Callback<int, string, double, int[], string[], char>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
                ClassicAssert.AreEqual(parameter6, argument6);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith7Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>()))
            .Callback<int, string, double, int[], string[], char, bool>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6,
                argument7) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
                ClassicAssert.AreEqual(parameter6, argument6);
                ClassicAssert.AreEqual(parameter7, argument7);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith8Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>()))
            .Callback<int, string, double, int[], string[], char, bool, TestInstance>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6,
                argument7,
                argument8) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
                ClassicAssert.AreEqual(parameter6, argument6);
                ClassicAssert.AreEqual(parameter7, argument7);
                ClassicAssert.AreEqual(parameter8, argument8);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8);
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith9Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();
        Func<int, int> parameter9 = x => x;

        using var _ = Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>(),
                    context.It.IsAny<Func<int, int>>()))
            .Callback<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6,
                argument7,
                argument8,
                argument9) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
                ClassicAssert.AreEqual(parameter6, argument6);
                ClassicAssert.AreEqual(parameter7, argument7);
                ClassicAssert.AreEqual(parameter8, argument8);
                ClassicAssert.AreEqual(parameter9, argument9);
            });

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9);
    }


    [Test]
    public void TestGenericSetupVoidWithTestMethodParameterCountMismatchFunc()
    {
        var setup = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()));

        Assert.Throws<Exception>(() => setup.Callback<int>(argument => { }));
    }

    [Test]
    public void TestGenericSetupVoidWithTestMethodParameterTypeMismatchFunc()
    {
        var setup = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()));

        Assert.Throws<Exception>(() => setup.Callback<string, string>((argument1, argument2) => { }));
    }

    [Test]
    public void TestGenericSetupVoidWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
        var executed = false;

        using var _ = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>())).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);

        ClassicAssert.IsTrue(executed);
    }

    [Test]
    public void TestGenericSetupVoidUnsafeStaticMethod()
    {
        TestStaticClass.TestUnsafeStaticVoidMethod();
        var executed = false;

        using var _ = Mock.Setup(context => TestStaticClass.TestUnsafeStaticVoidMethod()).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });

        TestStaticClass.TestUnsafeStaticVoidMethod();

        Assert.That(executed, Is.True);
    }
    
    [Test]
    public void TestGenericSetupVoidUnsafeInstanceMethod()
    {
        var instance = new TestInstance();
        instance.TestUnsafeInstanceVoidMethod();
        var executed = false;

        using var _ = Mock.Setup(context => instance.TestUnsafeInstanceVoidMethod()).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });

        instance.TestUnsafeInstanceVoidMethod();

        Assert.That(executed, Is.True);
    }
}