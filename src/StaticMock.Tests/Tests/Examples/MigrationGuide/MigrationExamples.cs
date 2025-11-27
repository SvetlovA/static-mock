using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.MigrationGuide;

[TestFixture]
public class MigrationExamples
{
    [Test]
    public void SMock_Basic_Mocking_Example()
    {
        // SMock (static methods) - Expression-based syntax
        using var mock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns("content");

        var result = File.ReadAllText("test.txt");
        ClassicAssert.AreEqual("content", result);
    }

    [Test]
    public void SMock_Parameter_Matching_Example()
    {
        // SMock parameter matching with It.IsAny<T>()
        using var mock = Mock.Setup(context => Path.GetFileName(context.It.IsAny<string>()))
            .Returns("result");

        var result = Path.GetFileName("test.txt");
        ClassicAssert.AreEqual("result", result);
    }

    [Test]
    public void SMock_Callback_Verification_Example()
    {
        var callCount = 0;

        using var mock = Mock.Setup(context => File.WriteAllText(context.It.IsAny<string>(), context.It.IsAny<string>()))
            .Callback<string, string>((_, _) => callCount++);

        File.WriteAllText("test.txt", "content");
        File.WriteAllText("test2.txt", "content2");

        ClassicAssert.AreEqual(2, callCount);
    }

    [Test]
    public void SMock_Exception_Throwing_Example()
    {
        // SMock exception throwing
        using var mock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Throws<FileNotFoundException>();

        Assert.Throws<FileNotFoundException>(() => File.ReadAllText("nonexistent.txt"));
    }

    [Test]
    [Ignore("Now the implementation is to fail if not match the condition in 'It.Is'. Maybe should be reworked later to allow fallback to original behavior?")]
    public void SMock_Conditional_Parameter_Matching()
    {
        // SMock conditional parameter matching with It.Is<T>()
        using var mock = Mock.Setup(context =>
                File.ReadAllText(context.It.Is<string>(path => path.EndsWith(".json"))))
            .Returns("{\"test\": \"data\"}");

        // This should match
        var result1 = File.ReadAllText("config.json");
        ClassicAssert.AreEqual("{\"test\": \"data\"}", result1);

        // This should not match and use original behavior
        try
        {
            File.ReadAllText("config.txt");
            // If this doesn't throw, it means the mock didn't match (expected)
        }
        catch (FileNotFoundException)
        {
            // Expected for unmocked call
            Assert.Pass("Unmocked call behaved as expected");
        }
    }

    [Test]
    public void SMock_Multiple_Return_Values()
    {
        var callCount = 0;

        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(() =>
            {
                callCount++;
                return callCount switch
                {
                    1 => new DateTime(2024, 1, 1),
                    2 => new DateTime(2024, 1, 2),
                    _ => new DateTime(2024, 1, 3)
                };
            });

        var date1 = DateTime.Now;
        var date2 = DateTime.Now;
        var date3 = DateTime.Now;

        ClassicAssert.AreEqual(new DateTime(2024, 1, 1), date1);
        ClassicAssert.AreEqual(new DateTime(2024, 1, 2), date2);
        ClassicAssert.AreEqual(new DateTime(2024, 1, 3), date3);
    }

    [Test]
    public void SMock_Property_Mocking()
    {
        // SMock property mocking
        using var mock = Mock.Setup(() => Environment.MachineName)
            .Returns("TEST_MACHINE");

        var machineName = Environment.MachineName;
        ClassicAssert.AreEqual("TEST_MACHINE", machineName);
    }
}