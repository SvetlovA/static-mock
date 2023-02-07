using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StaticMock.Tests.Settings;

namespace StaticMock.Tests;

[SetUpFixture]
public class AssemblySetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("HOOK_MANAGER_TYPE")}.json", optional: true)
            .Build();

        var appSettings = AppSettingsReader.ReadSettings(configuration);

        Mock.SetHookManagerType(appSettings.HookManagerType);
        Console.WriteLine($"Hook manager type: {appSettings.HookManagerType}");
    }
}