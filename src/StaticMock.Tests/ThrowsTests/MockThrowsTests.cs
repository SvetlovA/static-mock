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
                    Assert.Throws<Exception>(TestStaticClass.TestVoidMethodWithoutParameters);
                })
                .Throws(typeof(Exception));
        }
    }
}
