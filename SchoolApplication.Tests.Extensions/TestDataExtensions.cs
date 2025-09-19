using SchoolApplication.Entities;

namespace SchoolApplication.Tests.Extensions
{
    /// <summary>
    /// Модель расширений для генератора данных
    /// </summary>
    public static class TestDataExtensions
    {
        /// <summary>
        /// Устанавливает тестовые данные
        /// </summary>
        public static void SetBaseAuditData<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
