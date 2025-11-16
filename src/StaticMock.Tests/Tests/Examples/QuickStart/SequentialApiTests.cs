using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.QuickStart;

[TestFixture]
public class SequentialApiTests
{
    [Test]
    public void TestFileOperations()
    {
        // Mock file existence check
        using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
            .Returns(true);

        // Mock file content reading
        using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns("{\"setting\": \"test\"}");

        // Test file operations
        var exists = File.Exists("config.json");
        var content = File.ReadAllText("config.json");

        ClassicAssert.IsTrue(exists);
        ClassicAssert.AreEqual("{\"setting\": \"test\"}", content);
    }

    [Test]
    public void TestBasicDateTimeMocking()
    {
        var expectedDate = new DateTime(2024, 1, 1);

        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(expectedDate);

        var result = DateTime.Now;
        ClassicAssert.AreEqual(expectedDate, result);
    }

    [Test]
    public void TestEnvironmentMocking()
    {
        using var mock = Mock.Setup(() => Environment.MachineName)
            .Returns("TEST_MACHINE");

        var machineName = Environment.MachineName;
        ClassicAssert.AreEqual("TEST_MACHINE", machineName);
    }
}