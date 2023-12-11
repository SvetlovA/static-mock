using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical.ReturnsTests;

[TestFixture]
public class GenericSetupMockReturnsAsyncTests
{
    [Test]
    public async Task TestGenericSetupReturnsAsyncMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters(), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsAsyncMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupInstanceReturnsAsyncMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters(), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupInstanceReturnsAsyncMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnWithoutParameterFuncAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        const int expectedResult = 2;

        Mock.Setup(
                () => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync(),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(async () => await Task.FromResult(2));

        ClassicAssert.AreEqual(1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameterFuncAsync()
    {
        const int parameter1 = 10;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParameterAsync(parameter1);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                () => TestStaticAsyncClass.TestMethodReturnWithParameterAsync(parameter1),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParameterAsync(parameter1);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int>(argument =>
            {
                ClassicAssert.AreEqual(parameter1, argument);
                return Task.FromResult(argument / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters2FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(context.It.IsAny<int>(), context.It.IsAny<string>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string>(async (argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                return await Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters3FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2, parameter3);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2, parameter3);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double>((argument1, argument2, argument3) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                return Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters4FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[]>(async (
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                return await Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters5FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[]>((
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
                return Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters6FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char>(async (
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
                return await Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters7FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char, bool>((
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
                return Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters8FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char, bool, TestInstance>(async (
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
                return await Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnParameters9FuncAsync()
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

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>(),
                    context.It.IsAny<Func<int, int>>()),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8,
                        parameter9);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>>(async (
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
                await Task.CompletedTask;

                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                ClassicAssert.AreEqual(parameter5, argument5);
                ClassicAssert.AreEqual(parameter6, argument6);
                ClassicAssert.AreEqual(parameter7, argument7);
                ClassicAssert.AreEqual(parameter8, argument8);
                ClassicAssert.AreEqual(parameter9, argument9);
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }
}