using NUnit.Framework;

namespace StaticMock.Tests;

[SetUpFixture]
public class AssemblySetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        Console.WriteLine($".NET Version: {Environment.Version}");
    }
}