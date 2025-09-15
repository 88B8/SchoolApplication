using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SchoolApplication.Web.Tests.Infrastructure
{
    public static class WebHostBuilderHelper
    {
        public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.integration.json");
                config.AddJsonFile(configPath).AddEnvironmentVariables();
            });
        }
    }
}
