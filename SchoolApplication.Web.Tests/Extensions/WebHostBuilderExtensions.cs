using Microsoft.AspNetCore.Hosting;
using SchoolApplication.Web.Infrastructure;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureIntegrationTestApp(this IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.UseEnvironment(EnvironmentNames.Integration);
            return builder;
        }
    }
}
