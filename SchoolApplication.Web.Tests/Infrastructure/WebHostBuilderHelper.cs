using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SchoolApplication.Web.Tests
{
    static internal class WebHostBuilderHelper
    {
        public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.integration.json");
                config.AddJsonFile(configPath).AddEnvironmentVariables();
            });
        }
    }
}
