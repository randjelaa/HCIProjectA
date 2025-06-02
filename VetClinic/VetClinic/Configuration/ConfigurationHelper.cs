using Microsoft.Extensions.Configuration;
using System.IO;

public static class ConfigurationHelper
{
    public static IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
