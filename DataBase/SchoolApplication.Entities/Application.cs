namespace SchoolApplication.Entities
{
    /// <summary>
    /// Модель заявления
    /// </summary>
    public class Application : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор ученика
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Навигационное свойство ученика
        /// </summary>
        public Student Student { get; set; } = null!;

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Навигационное свойство родителя
        /// </summary>
        public Parent Parent { get; set; } = null!;

        /// <summary>
        /// Идентификатор школы
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// Навигационное свойство школы
        /// </summary>
        public School School { get; set; } = null!;

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
