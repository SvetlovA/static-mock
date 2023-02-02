using NUnit.Framework;
using StaticMock.Entities.Enums;

namespace StaticMock.Tests;

[SetUpFixture]
public class AssemblySetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        Mock.SetHookManagerType(HookManagerType.Harmony);
    }
}