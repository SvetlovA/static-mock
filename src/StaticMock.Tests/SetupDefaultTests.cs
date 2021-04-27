using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class SetupDefaultTests
    {
        [Test]
        public void TestSetupDefaultPositive()
        {
            Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
            {
                TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
            });
        }

        [Test]
        public void TestSetupDefaultNegative()
        {
            Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () => 
            { 
                TestStaticClass.TestMethodReturn1WithoutParameters();
            }));
        }

        [Test]
        public void TestGenericSetupDefaultPositive()
        {
            Mock.SetupDefault(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
            {
                TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
            });
        }

        [Test]
        public void TestGenericSetupDefaultNegative()
        {
            Assert.Throws<Exception>(() => Mock.SetupDefault(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                TestStaticClass.TestMethodReturn1WithoutParameters();
            }));
        }

        [Test]
        public void TestGenericSetupDefaultNegativeIncorrectExpression()
        {
            var body = Expression.Block(
                Expression.Add(Expression.Constant(1), Expression.Constant(2)),
                Expression.Constant(3));
            var lambda = Expression.Lambda<Action>(body);

            Assert.Throws<Exception>(() => Mock.SetupDefault(lambda, () =>
            {
                TestStaticClass.TestMethodReturn1WithoutParameters();
            }));
        }

        [Test]
        public void TestGenericSetupDefaultInstanceMethod()
        {
            var testInstance = new TestInstance();

            Mock.SetupDefault(() => testInstance.TestVoidMethodWithoutParametersThrowsException(), () =>
            {
                testInstance.TestVoidMethodWithoutParametersThrowsException();
            });
        }
    }
}
