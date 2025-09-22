using SchoolApplication.Entities;
using SchoolApplication.Entities.Contracts;

namespace SchoolApplication.Repositories.Contracts.Models
{
    /// <summary>
    /// Модель заявления для соединения таблиц
    /// </summary>
    public class ApplicationDbModel : IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство ученика
        /// </summary>
        public required Student Student { get; set; }

        /// <summary>
        /// Навигационное свойство родителя
        /// </summary>
        public required Parent Parent { get; set; }

        /// <summary>
        /// Навигационное свойство школы
        /// </summary>
        public required School School { get; set; }

        /// <summary>
        /// Причина заявления
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Дата, с которой ученик отсутствует
        /// </summary>
        public DateOnly DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую ученик отсутствует
        /// </summary>
        public DateOnly DateUntil { get; set; }
    }
}
