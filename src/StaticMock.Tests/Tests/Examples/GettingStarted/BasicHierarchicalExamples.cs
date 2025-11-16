using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.GettingStarted;

[TestFixture]
public class BasicHierarchicalExamples
{
    private const string ExpectedPath = "important.txt";
    private const string MockContent = "validated content";
    private const string ActualContent = "actual text";
    
    [SetUp]
    public void Setup()
    {
        File.WriteAllText(ExpectedPath, ActualContent);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(ExpectedPath))
        {
            File.Delete(ExpectedPath);
        }
    }
    
    [Test]
    public void Hierarchical_API_Example()
    {
        Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()), () =>
        {
            // This validation runs DURING the mock call
            var content = File.ReadAllText(ExpectedPath);
            ClassicAssert.IsNotNull(content);
            ClassicAssert.AreEqual(MockContent, content);

            // You can even verify the mock was called with correct parameters
        }).Returns(MockContent);

        // Test your code - validation happens automatically
        var result = File.ReadAllText("important.txt");
        ClassicAssert.AreEqual(ActualContent, result);
    }

    [Test]
    public void Hierarchical_Parameter_Validation()
    {
        Mock.Setup(context => Path.Combine(context.It.IsAny<string>(), context.It.IsAny<string>()), () =>
        {
            // Validate the actual parameters that were passed
            var result = Path.Combine("test", "path");
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsTrue(result.Contains("test"));
            ClassicAssert.IsTrue(result.Contains("path"));
        }).Returns(@"test\path");

        var combinedPath = Path.Combine("test", "path");
        ClassicAssert.AreEqual(@"test\path", combinedPath);
    }

    [Test]
    public void Hierarchical_With_Complex_Validation()
    {
        var callCount = 0;
        var mockDate = new DateTime(2024, 1, 1);

        Mock.Setup(context => DateTime.Now, () =>
        {
            callCount++;
            var currentTime = DateTime.Now;

            // Verify this is the mocked time
            ClassicAssert.AreEqual(mockDate, currentTime);

            // You can perform additional validations here
            ClassicAssert.Greater(callCount, 0, "Method should be called at least once");
        }).Returns(mockDate);

        ClassicAssert.AreNotEqual(mockDate, DateTime.Now);
    }

    [Test]
    public void Hierarchical_Exception_Testing()
    {
        var exceptionThrown = false;

        Mock.Setup(context => File.ReadAllText(context.It.Is<string>(path => path == "invalid.json")), () =>
        {
            // Validation that runs when exception scenario is triggered
            exceptionThrown = true;
        }).Throws<FileNotFoundException>();

        // Test exception handling
        Assert.Throws<FileNotFoundException>(() => File.ReadAllText("invalid.json"));
        ClassicAssert.IsTrue(exceptionThrown, "Exception validation should have been executed");
    }
}