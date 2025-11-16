using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Examples.QuickStart;

[TestFixture]
public class HierarchicalApiTests
{
    [Test]
    public void TestDatabaseOperations()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()), () =>
        {
            // This validation runs DURING the mock execution
            var result = TestStaticClass.TestMethodReturnWithParameter(1);
            ClassicAssert.IsNotNull(result);
            ClassicAssert.Greater(result, 0);
        }).Returns(1);

        // Test your service
        var result = TestStaticClass.TestMethodReturnWithParameter(1);

        ClassicAssert.AreEqual(1, result);
    }

    [Test]
    public void TestParameterValidation()
    {
        const int validParameter = 42;
        const int mockParameter = 100;

        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x > 0)), () =>
        {
            // Validate the parameter during execution
            var result = TestStaticClass.TestMethodReturnWithParameter(validParameter);
            ClassicAssert.AreEqual(mockParameter, result);
        }).Returns(mockParameter);

        // Test with valid parameter
        var result = TestStaticClass.TestMethodReturnWithParameter(validParameter);
        ClassicAssert.AreEqual(validParameter, result);
    }
}