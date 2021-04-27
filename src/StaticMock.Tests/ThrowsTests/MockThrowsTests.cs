using System;
using NUnit.Framework;

namespace StaticMock.Tests.ThrowsTests
{
    [TestFixture]
    public class MockThrowsTests
    {
        [Test]
        public void TestThrowsTestMethodReturn1WithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsTestVoidMethodWithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsTestVoidMethodWithParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsArgumentNullException()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws(typeof(ArgumentNullException));
        }

        [Test]
        public void TestInstanceMethodThrows()
        {
            var testInstance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
            {
                Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
            }).Throws<Exception>();
        }
    }
}
