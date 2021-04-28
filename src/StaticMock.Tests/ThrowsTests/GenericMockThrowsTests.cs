using System;
using NUnit.Framework;

namespace StaticMock.Tests.ThrowsTests
{
    [TestFixture]
    public class GenericMockThrowsTests
    {
        [Test]
        public void TestThrowsTestMethodReturn1WithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
                })
                .Throws<Exception>();
        }

        [Test]
        public void TestThrowsTestVoidMethodWithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws<Exception>();
        }

        [Test]
        public void TestThrowsTestVoidMethodWithParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
                })
                .Throws<Exception>();
        }

        [Test]
        public void TestThrowsArgumentNullException()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws<ArgumentNullException>();
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
    }
}
