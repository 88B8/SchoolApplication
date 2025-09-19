using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolApplication.Context;
using SchoolApplication.Web.Tests.Client;

namespace SchoolApplication.Web.Tests.Infrastructure
{
    /// <summary>
    /// Фикстура
    /// </summary>
    public class SchoolApplicationApiFixture : IAsyncLifetime
    {
        private readonly TestWebApplicationFactory factory;
        private SchoolApplicationContext? context;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolApplicationApiFixture()
        {
            factory = new TestWebApplicationFactory();
        }

        /// <summary>
        /// Предоставляет доступ к констексту
        /// </summary>
        internal SchoolApplicationContext Context
        {
            get
            {
                if (context != null)
                {
                    return context;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                context = scope.ServiceProvider.GetRequiredService<SchoolApplicationContext>();
                return context;
            }
        }

        /// <summary>
        /// Предоставляет доступ к веб клиенту
        /// </summary>
        internal ISchoolApplicationApiClient WebClient
        {
            get
            {
                var client = factory.CreateClient();
                return new SchoolApplicationApiClient(string.Empty, client);
            }
        }

        /// <inheritdoc cref="IAsyncLifetime.InitializeAsync"/>
        public Task InitializeAsync() => Context.Database.MigrateAsync();

        /// <inheritdoc cref="IAsyncLifetime.DisposeAsync"/>
        async Task IAsyncLifetime.DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.CloseConnectionAsync();
            await Context.DisposeAsync();
            await factory.DisposeAsync();
        }
    }
}