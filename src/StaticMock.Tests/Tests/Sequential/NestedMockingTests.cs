using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential;

[TestFixture]
public class NestedMockingTests
{
    [Test]
    public void TestNestedReturnMock()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        using (Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult))
        {
            var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
        }

        using (Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedChildResult))
        {
            var actualChildResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
        }
    }

    [Test]
    public void TestNestedReturnMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult);
        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Returns(expectedChildResult);
        var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);

        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
        ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
    }

    [Test]
    public void TestNestedReturnMockWithDifferentFunctionsParentAfterChild()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult);
        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();

        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Returns(expectedChildResult);
        var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);

        ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
    }

    [Test]
    public void TestNestedInChildReturnMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult);
        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Returns(expectedChildResult);

        var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
        ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
    }

    [Test]
    public void TestNestedInChildReturnThrowsMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult);
        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Throws<Exception>();

        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
    }

    [Test]
    public void TestNestedReturnThrowsTest()
    {
        const int expectedParentResult = 2;

        using var parent =Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedParentResult);
        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();

        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestNestedThrowsReturnTest()
    {
        const int expectedChildResult = 2;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(expectedChildResult);
        var actualChildResult = TestStaticClass.TestMethodReturn1WithoutParameters();

        ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
    }

    [Test]
    public void TestNestedGenericThrowsMockWithDifferentFunctionsParentAfterChild()
    {
        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Throws<ArgumentNullException>();

        Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestMethodReturnWithParameter(2));
    }

    [Test]
    public void TestNestedThrowsMockWithDifferentFunctionsParentAfterChild()
    {
        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws(typeof(Exception));

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Throws(typeof(ArgumentNullException));

        Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestMethodReturnWithParameter(2));
    }

    [Test]
    public void TestNestedInChildCallbackMockWithDifferentFunctions()
    {
        const int expectedParentResult = 2;
        const int expectedChildResult = 3;

        using var parent = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Returns(() => expectedParentResult);

        using var child = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(2)).Returns<int>(x => expectedChildResult);

        var actualChildResult = TestStaticClass.TestMethodReturnWithParameter(2);
        ClassicAssert.AreEqual(expectedChildResult, actualChildResult);
        var actualParentResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(expectedParentResult, actualParentResult);
    }
}