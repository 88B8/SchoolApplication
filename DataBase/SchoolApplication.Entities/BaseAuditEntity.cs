using SchoolApplication.Entities.Contracts;

namespace SchoolApplication.Entities
{
    /// <summary>
    /// Модель сущности с аудитом
    /// </summary>
    public abstract class BaseAuditEntity : IEntitySoftDeleted, IEntityWithAudit, IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
