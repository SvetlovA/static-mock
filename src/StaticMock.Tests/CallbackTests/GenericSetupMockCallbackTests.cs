using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.CallbackTests;

[TestFixture]
public class GenericSetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }

    [Test]
    public void TestActionCallbackThrows()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException());
        }).Callback(() => throw new Exception());
    }

    [Test]
    public void TestActionCallbackInstance()
    {
        var instance = new TestInstance();

        static void DoSomething() { }

        Mock.Setup(() => instance.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            instance.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWithParameters()
    {
        const int parameter1 = 10;

        Mock.Setup(
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1),
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1))
            .Callback<int>(argument =>
            {
                Assert.AreEqual(parameter1, argument);
            });
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith2Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2))
            .Callback<int, string>((argument1, argument2) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
            });
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith3Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>()),
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2, parameter3))
            .Callback<int, string, double>((argument1, argument2, argument3) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
            });
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith4Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4))
            .Callback<int, string, double, int[]>((
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
            });
    }

    [Test]
    public void TestGenericSetupWithTestVoidMethodWith5Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5))
            .Callback<int, string, double, int[], string[]>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
            });
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

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6))
            .Callback<int, string, double, int[], string[], char>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
            });
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

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7))
            .Callback<int, string, double, int[], string[], char, bool>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6,
                argument7) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
            });
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

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8))
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
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
                Assert.AreEqual(parameter8, argument8);
            });
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

        Mock.Setup(
                context => TestStaticClass.TestVoidMethodWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>(),
                    context.It.IsAny<Func<int, int>>()),
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8,
                        parameter9))
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
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
                Assert.AreEqual(parameter8, argument8);
                Assert.AreEqual(parameter9, argument9);
            });
    }


    [Test]
    public void TestGenericSetupVoidWithTestMethodParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var setup = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2));

        Assert.Throws<Exception>(() => setup.Callback<int>(argument => { }));
    }

    [Test]
    public void TestGenericSetupVoidWithTestMethodParameterTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var setup = Mock.Setup(
            context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
            () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2));

        Assert.Throws<Exception>(() => setup.Callback<string, string>((argument1, argument2) => {}));
    }

    [Test]
    public void TestGenericSetupVoidWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
        var executed = false;

        Mock.Setup(
            context => TestStaticClass.TestVoidMethodWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
            () =>
            {
                TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
            }).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });

        Assert.IsTrue(executed);
    }
}