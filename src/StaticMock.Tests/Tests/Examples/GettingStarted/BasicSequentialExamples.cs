using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.GettingStarted;

[TestFixture]
public class BasicSequentialExamples
{
    [Test]
    public void MyFirstMockTest()
    {
        // SMock is ready to use immediately!
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var testDate = DateTime.Now;
        ClassicAssert.AreEqual(new DateTime(2024, 1, 1), testDate);
    }

    [Test]
    public void Mock_DateTime_Now()
    {
        var fixedDate = new DateTime(2024, 12, 25, 10, 30, 0);

        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(fixedDate);

        // Your code that uses DateTime.Now
        var currentDate = DateTime.Now;
        ClassicAssert.AreEqual(fixedDate, currentDate);

        // Verify time components
        ClassicAssert.AreEqual(2024, currentDate.Year);
        ClassicAssert.AreEqual(12, currentDate.Month);
        ClassicAssert.AreEqual(25, currentDate.Day);
        ClassicAssert.AreEqual(10, currentDate.Hour);
        ClassicAssert.AreEqual(30, currentDate.Minute);
    }

    [Test]
    public void Mock_File_Operations()
    {
        using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
            .Returns(true);

        using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns("{\"database\": \"localhost\", \"port\": 5432}");

        // Test file operations
        var exists = File.Exists("config.json");
        var content = File.ReadAllText("config.json");

        ClassicAssert.IsTrue(exists);
        ClassicAssert.AreEqual("{\"database\": \"localhost\", \"port\": 5432}", content);
        ClassicAssert.IsTrue(content.Contains("localhost"));
        ClassicAssert.IsTrue(content.Contains("5432"));
    }

    [Test]
    public void Mock_Static_Property()
    {
        using var mock = Mock.Setup(() => Environment.MachineName)
            .Returns("TEST-MACHINE");

        var machineName = Environment.MachineName;
        ClassicAssert.AreEqual("TEST-MACHINE", machineName);
    }

    [Test]
    public void Mock_With_Parameter_Matching()
    {
        // Match any string parameter
        using var anyStringMock = Mock.Setup(context => Path.GetFileName(context.It.IsAny<string>()))
            .Returns("mocked-file.txt");

        // Test with different paths
        var result1 = Path.GetFileName(@"C:\temp\test.txt");
        var result2 = Path.GetFileName(@"D:\documents\report.docx");

        ClassicAssert.AreEqual("mocked-file.txt", result1);
        ClassicAssert.AreEqual("mocked-file.txt", result2);
    }

    [Test]
    [Ignore("Now the implementation is to fail if not match the condition in 'It.Is'. Maybe should be reworked later to allow fallback to original behavior?")]
    public void Mock_With_Conditional_Parameter_Matching()
    {
        // Match with specific conditions
        using var conditionalMock = Mock.Setup(context =>
                Path.GetExtension(context.It.Is<string>(path => path.EndsWith(".txt"))))
            .Returns(".mocked");

        // This should match and return mocked value
        var result1 = Path.GetExtension("document.txt");
        ClassicAssert.AreEqual(".mocked", result1);

        // This should not match and return the original behavior
        var result2 = Path.GetExtension("document.docx");
        ClassicAssert.AreEqual(".docx", result2);
    }
}