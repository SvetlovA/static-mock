﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical;

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
            ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualChildResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
            }).Returns(expectedChildResult);
        }).Returns(expectedParentResult);
    }

    [Test]
    public void TestNestedReturnMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
                ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
            }).Returns(expectedChildResult);
        }).Returns(expectedParentResult);
    }

    [Test]
    public void TestNestedReturnMockWithDifferentFunctionsParentAfterChild()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
            }).Returns(expectedChildResult);

            var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
        }).Returns(expectedParentResult);
    }

    [Test]
    public void TestNestedInChildReturnMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
                var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
            }).Returns(expectedChildResult);
        }).Returns(expectedParentResult);
    }

    [Test]
    public void TestNestedInChildReturnThrowsMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
                Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
                actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
            }).Throws<Exception>();
        }).Returns(expectedParentResult);
    }

    [Test]
    public void TestNestedReturnThrowsTest()
    {
        const int expectedParentResult = 2;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
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
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
            }).Returns(expectedChildResult);
        }).Throws<Exception>();
    }

    [Test]
    public void TestNestedGenericThrowsMockWithDifferentFunctionsParentAfterChild()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestMethodReturnWithParameter(2));
            }).Throws<ArgumentNullException>();

            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestNestedThrowsMockWithDifferentFunctionsParentAfterChild()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestMethodReturnWithParameter(2));
            }).Throws(typeof(ArgumentNullException));

            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
        }).Throws(typeof(Exception));
    }

    [Test]
    public void TestNestedInChildCallbackMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2), () =>
            {
                var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
                ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
                var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
            }).Returns<int>(x => expectedChildResult);
        }).Returns(() => expectedParentResult);
    }
}