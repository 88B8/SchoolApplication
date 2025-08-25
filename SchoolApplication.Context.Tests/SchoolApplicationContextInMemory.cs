using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SchoolApplication.Context.Contracts;
using System.Diagnostics;

namespace SchoolApplication.Context.Tests
{
    /// <summary>
    /// Класс <see cref="SchoolApplicationContext"/> для тестов с базой в памяти. Один контекст на тест
    /// </summary>
    public abstract class SchoolApplicationContextInMemory : IAsyncDisposable
    {
        /// <summary>
        /// Контекст для <see cref="SchoolApplicationContext"/>
        /// </summary>
        protected SchoolApplicationContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork"
        protected IUnitOfWork UnitOfWork => Context;

        /// <summary>
        /// ctor
        /// </summary>
        protected SchoolApplicationContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolApplicationContext>()
                .UseInMemoryDatabase($"SchoolApplicationTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new SchoolApplicationContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IAsyncDisposable"/>
        public async ValueTask DisposeAsync()
        {
            try
            {
                await Context.Database.EnsureDeletedAsync();
                await Context.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
