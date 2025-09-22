using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolApplication.Context
{
    /// <summary>
    /// Модель контекста базы данных
    /// </summary>
    public class SchoolApplicationContext : DbContext, IReader, IWriter, IUnitOfWork
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных
        /// </summary>
        public SchoolApplicationContext(DbContextOptions<SchoolApplicationContext> options) : base(options) { }

        /// <summary>
        /// Настраивает модели
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IReader.Read<TEntity>()
            where TEntity : class
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IWriter.Add<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return count;
        }
    }
}