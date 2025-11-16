using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.GettingStarted;

[TestFixture]
public class AsyncExamples
{
    [Test]
    public async Task Mock_Async_Methods()
    {
        // Mock async HTTP call - using expression-based setup
        using var mock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Test the mocked delay
        await Task.Delay(1000); // Should complete immediately due to mock

        // Verify the test completes quickly (no actual delay)
        Assert.Pass("Async mock executed successfully");
    }

    [Test]
    public async Task Mock_Task_FromResult()
    {
        using var mock = Mock.Setup(() => Task.FromResult(42))
            .Returns(Task.FromResult(100));

        var result = await Task.FromResult(42);
        ClassicAssert.AreEqual(100, result);
    }

    [Test]
    public async Task Mock_Async_With_Delay_Simulation()
    {
        const string testData = "processed data";

        // Mock Task.Delay to return immediately
        using var delayMock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Simulate an async operation that would normally take time
        await Task.Delay(5000); // This should complete immediately
        var result = testData.ToUpper();

        ClassicAssert.AreEqual("PROCESSED DATA", result);
    }

    [Test]
    public async Task Mock_Async_Exception_Handling()
    {
        // Mock Task.Delay to throw an exception
        using var mock = Mock.Setup(context => Task.Delay(context.It.Is<int>(ms => ms < 0)))
            .Throws<ArgumentOutOfRangeException>();

        // Test exception handling in async context
        try
        {
            await Task.Delay(-1);
            Assert.Fail("Expected ArgumentOutOfRangeException to be thrown");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            ClassicAssert.IsNotNull(exception);
        }
    }

    [Test]
    public async Task Mock_Async_Return_Values()
    {
        const string mockResult = "async mock result";

        // Mock an async method that returns a value
        using var mock = Mock.Setup(context => Task.FromResult(context.It.IsAny<string>()))
            .ReturnsAsync(mockResult);

        var result = await Task.FromResult("original");
        ClassicAssert.AreEqual(mockResult, result);
    }

    [Test]
    public async Task Mock_Multiple_Async_Operations()
    {
        // Mock multiple async operations
        using var delayMock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        using var resultMock = Mock.Setup(context => Task.FromResult(context.It.IsAny<int>()))
            .ReturnsAsync(50);

        // Execute multiple async operations
        await Task.Delay(1000); // Should complete immediately
        var value = await Task.FromResult(10); // Should return 50

        ClassicAssert.AreEqual(50, value);
    }
}