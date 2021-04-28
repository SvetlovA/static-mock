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

        [Test]
        public void TestGenericThrowsTestMethodReturnTask()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTask), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTask());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskAsync()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
        {
            Mock.SetupVoid(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodReturnTaskAsync), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParametersAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParameters), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParameters());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTask()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTask), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
        {
            var instance = new TestInstance();

            Mock.SetupVoid(typeof(TestInstance), nameof(TestInstance.TestVoidMethodReturnTaskAsync), () =>
            {
                Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
            });
        }
    }
}
