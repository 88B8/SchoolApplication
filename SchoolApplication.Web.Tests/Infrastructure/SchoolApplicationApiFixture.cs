using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolApplication.Context;

namespace SchoolApplication.Web.Tests
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

        internal ISchoolApplicationApiClient WebClient
        {
            get
            {
                var client = factory.CreateClient();
                return new SchoolApplicationApiClient(string.Empty, client);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Task InitializeAsync() => Context.Database.MigrateAsync();

        /// <summary>
        /// 
        /// </summary>
        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.CloseConnectionAsync();
            await Context.DisposeAsync();
            await factory.DisposeAsync();
        }
    }
}