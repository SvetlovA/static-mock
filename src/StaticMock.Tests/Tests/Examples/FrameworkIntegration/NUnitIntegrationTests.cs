using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.FrameworkIntegration;

[TestFixture]
public class NUnitIntegrationTests
{
    [Test]
    public void Basic_SMock_Test()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        ClassicAssert.AreEqual(new DateTime(2024, 1, 1), result);
    }

    [Test]
    [TestCase("file1.txt", "content1")]
    [TestCase("file2.txt", "content2")]
    [TestCase("file3.txt", "content3")]
    public void Parameterized_File_Mock_Test(string fileName, string expectedContent)
    {
        using var mock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns(expectedContent);

        var result = File.ReadAllText(fileName);
        ClassicAssert.AreEqual(expectedContent, result);
    }

    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void TestCaseSource_Example(TestData data)
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(data.ExpectedDate);

        var result = DateTime.Now;
        ClassicAssert.AreEqual(data.ExpectedDate, result);
    }

    private static IEnumerable<TestData> GetTestData()
    {
        yield return new TestData { ExpectedDate = new DateTime(2024, 1, 1) };
        yield return new TestData { ExpectedDate = new DateTime(2024, 2, 1) };
        yield return new TestData { ExpectedDate = new DateTime(2024, 3, 1) };
    }

    public class TestData
    {
        public DateTime ExpectedDate { get; set; }
    }

    [Test]
    public void Test_With_Category()
    {
        using var mock = Mock.Setup(() => DateTime.UtcNow)
            .Returns(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));

        var result = DateTime.UtcNow;
        ClassicAssert.AreEqual(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), result);
        ClassicAssert.AreEqual(DateTimeKind.Utc, result.Kind);
    }

    [Test]
    [CancelAfter(5000)]
    public void Test_With_Timeout()
    {
        using var mock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // This would normally timeout, but the mock makes it complete immediately
        Task.Delay(10000).Wait();
        Assert.Pass("Test completed within timeout due to mock");
    }
}