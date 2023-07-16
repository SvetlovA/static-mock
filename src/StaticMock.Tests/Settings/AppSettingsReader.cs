using Microsoft.Extensions.Configuration;
using StaticMock.Tests.Settings.Config;
using StaticMock.Tests.Settings.Entities;

namespace StaticMock.Tests.Settings;

internal static class AppSettingsReader
{
    public static AppSettings ReadSettings(IConfiguration configuration)
    {
        var appConfig = configuration.Get<AppSettingsConfigEntity>();

        return new AppSettings
        {
            HookManagerType = appConfig.HookManagerType
        };
    }
}