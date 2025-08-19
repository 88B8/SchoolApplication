using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolApplication.Context;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("integration");
            return base.CreateHost(builder);
        }

        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.ConfigureWebHost"/>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SchoolApplicationContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddSingleton<DbContextOptions<SchoolApplicationContext>>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("IntegrationConnection");
                    var dbContextOptions = new DbContextOptions<SchoolApplicationContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                    var optionsBuilder = new DbContextOptionsBuilder<SchoolApplicationContext>(dbContextOptions)
                        .UseApplicationServiceProvider(provider)
                        .UseNpgsql(connectionString: string.Format(connectionString!, Guid.NewGuid().ToString("N")));
                    return optionsBuilder.Options;
                });
            });
        }
    }
}
