using System;
using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class NestedMockingTests
    {
        [Test]
        public void TestNestedReturnMock()
        {
            const int expectedParentResult = 2;
            const int expectedChildResult = 3;

            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(expectedParentResult, actualParentResult);
                Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    var actualChildResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                    Assert.AreEqual(expectedChildResult, actualChildResult);
                }).Returns(expectedChildResult);
            }).Returns(expectedParentResult);
        }

        [Test]
        public void TestNestedReturnThrowsTest()
        {
            const int expectedParentResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(expectedParentResult, actualParentResult);
                Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
                }).Throws<Exception>();
            }).Returns(expectedParentResult);
        }

        [Test]
        public void TestNestedThrowsReturnTest()
        {
            const int expectedChildResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                
                Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
                Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    var actualChildResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                    Assert.AreEqual(expectedChildResult, actualChildResult);
                }).Returns(expectedChildResult);
            }).Throws<Exception>();
        }
    }
}
