using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical.ReturnsTests;

[TestFixture]
public class GenericSetupMockReturnsTests
{
    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturn1WithoutParameters()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        const int expectedResult = 2;

        Mock.Setup(
                () => TestStaticClass.TestMethodReturn1WithoutParameters(),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);

        var afterMockActualResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(originalResult, afterMockActualResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturn1WithoutParametersAfterCheck()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        var expectedResult = 2;

        ClassicAssert.AreEqual(1, originalResult);

        Mock.Setup(
                () => TestStaticClass.TestMethodReturn1WithoutParameters(),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);

        var afterMockActualResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(1, afterMockActualResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
        var expectedResult = 2;

        Mock.Setup(
                () => TestStaticClass.TestMethodReturnWithParameter(10),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnInstanceObject()
    {
        var originalResult = TestStaticClass.TestMethodReturnReferenceObject();
        var expectedResult = new TestInstance
        {
            IntProperty = 1,
            ObjectProperty = new object()
        };

        Mock.Setup(
                () => TestStaticClass.TestMethodReturnReferenceObject(),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodOutParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);
        var expectedResult = 2;

        var x = 1;
        Mock.Setup(
                () => TestStaticClass.TestMethodReturn1WithOutParameter(out x),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodRefParameter()
    {
        var x = 1;
        var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
        var expectedResult = 2;

        Mock.Setup(
                () => TestStaticClass.TestMethodReturn1WithRefParameter(ref x),
                () =>
                {
                    var y = 1;
                    var actualResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref y);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestInstanceMethodReturns()
    {
        var testInstance = new TestInstance();
        ClassicAssert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        var expectedResult = 2;

        Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters(), () =>
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters(), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestGenericSetupReturnsMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestGenericSetupInstanceReturnsMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters(), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestGenericSetupInstanceReturnsMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public void TestReturnsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticClass.StaticIntProperty, () =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.Setup(() => TestStaticClass.StaticObjectProperty, () =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        Mock.Setup(() => instance.IntProperty, () =>
        {
            var actualResult = instance.IntProperty;
            ClassicAssert.AreEqual(2, actualResult);
        }).Returns(2);
    }

    [Test]
    public void TestReturnsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        Mock.Setup(() => instance.ObjectProperty, () =>
        {
            var actualResult = instance.ObjectProperty;
            ClassicAssert.AreEqual(typeof(int), actualResult);
        }).Returns(typeof(int));
    }

    [Test]
    public void TestGenericSetupReturnsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>(), () =>
        {
            var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>(), () =>
        {
            var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        ClassicAssert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters(), () =>
        {
            var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnWithoutParameterFunc()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        const int expectedResult = 2;

        Mock.Setup(
                () => TestStaticClass.TestMethodReturn1WithoutParameters(),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns(() => 2);

        ClassicAssert.AreEqual(1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameterFunc()
    {
        const int parameter1 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                () => TestStaticClass.TestMethodReturnWithParameter(parameter1),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int>(argument =>
            {
                ClassicAssert.AreEqual(parameter1, argument);
                return argument / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsTaskWithTestMethodReturnParameterFunc()
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
    public void TestGenericSetupReturnsWithTestMethodReturnParameters2Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters3Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double>((argument1, argument2, argument3) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters4Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[]>((
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters5Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
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
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters6Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char>((
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
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters7Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
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
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
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
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters8Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
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
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
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
            .Returns<int, string, double, int[], string[], char, bool, TestInstance>((
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
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameters9Func()
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

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
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
                context => TestStaticClass.TestMethodReturnWithParameters(
                    context.It.IsAny<int>(),
                    context.It.IsAny<string>(),
                    context.It.IsAny<double>(),
                    context.It.IsAny<int[]>(),
                    context.It.IsAny<string[]>(),
                    context.It.IsAny<char>(),
                    context.It.IsAny<bool>(),
                    context.It.IsAny<TestInstance>(),
                    context.It.IsAny<Func<int, int>>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
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
            .Returns<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>>((
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
                return argument1 / 2;
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }


    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                });

        Assert.Throws<Exception>(() => setup.Returns<int>(argument => argument / 2));
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
                context => TestStaticClass.TestMethodReturnWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                    ClassicAssert.AreNotEqual(originalResult, actualResult);
                    ClassicAssert.AreEqual(expectedResult, actualResult);
                });

        Assert.Throws<Exception>(() => setup.Returns<string, string>((argument1, argument2) => 2));
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
            .Returns<int, string>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                return Task.FromResult(argument1 / 2);
            });

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;
        var executed = false;

        ClassicAssert.AreEqual(parameter1, originalResult);

        Mock.Setup(
            context => TestStaticClass.TestMethodReturnWithParameters(context.It.IsAny<int>(), context.It.IsAny<string>()),
            () =>
            {
                var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                ClassicAssert.AreNotEqual(originalResult, actualResult);
                ClassicAssert.AreEqual(expectedResult, actualResult);
            }).Returns(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
            return originalResult / 2;
        });

        ClassicAssert.IsTrue(executed);
        ClassicAssert.AreEqual(parameter1, originalResult);
    }
    
    [Test]
    public void TestGenericSetupDateTimeUtcNow()
    {
        var expectedDate = new DateTime(2020, 4, 5);

        Mock.Setup(context => DateTime.UtcNow,
                () => { Assert.That(DateTime.UtcNow, Is.EqualTo(expectedDate)); })
            .Returns(expectedDate);
        
        Assert.That(DateTime.UtcNow, Is.Not.EqualTo(expectedDate));
    }

    [Test]
    public void TestGenericSetupDateTimeNow()
    {
        var expectedDate = new DateTime(2020, 4, 5);

        Mock.Setup(context => DateTime.Now,
                () => { Assert.That(DateTime.Now, Is.EqualTo(expectedDate)); })
            .Returns(expectedDate);
        
        Assert.That(DateTime.Now, Is.Not.EqualTo(expectedDate));
    }
}