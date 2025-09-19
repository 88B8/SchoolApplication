using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using SchoolApplication.Entities.Contracts;

namespace SchoolApplication.Context.Contracts
{
    /// <summary>
    /// Общие спецификации
    /// </summary>
    public static class CommonSpecs
    {
        /// <summary>
        /// Активные. Не удаленные
        /// </summary>
        public static IQueryable<TEntity> NotDeletedAt<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IEntitySoftDeleted
            => query.Where(x => x.DeletedAt == null);

        /// <summary>
        /// По идентификатору
        /// </summary>
        public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id)
            where TEntity : class, IEntityWithId
            => query.Where(x => x.Id == id);

        /// <summary>
        /// По идентификатору родителя
        /// </summary>
        public static IQueryable<TEntity> ByParentId<TEntity>(this IQueryable<TEntity> query, Guid id)
            where TEntity : class, IEntityWithParentId
            => query.Where(x => x.ParentId == id);

        /// <summary>
        /// По идентификатору школы
        /// </summary>
        public static IQueryable<TEntity> BySchoolId<TEntity>(this IQueryable<TEntity> query, Guid id)
            where TEntity : class, IEntityWithSchoolId
            => query.Where(x => x.SchoolId == id);

        /// <summary>
        /// По идентификатору ученика
        /// </summary>
        public static IQueryable<TEntity> ByStudentId<TEntity>(this IQueryable<TEntity> query, Guid id)
            where TEntity : class, IEntityWithStudentId
            => query.Where(x => x.StudentId == id);

        /// <summary>
        /// Возвращает <see cref="IReadOnlyCollection{TEntity}"/>
        /// </summary>
        public static Task<IReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> query,
            CancellationToken cancellationToken)
            => query.ToListAsync(cancellationToken)
                .ContinueWith(x => new ReadOnlyCollection<TEntity>(x.Result) as IReadOnlyCollection<TEntity>,
                    cancellationToken);
    }
}
