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
        /// Навигационное свойство
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public Parent Parent { get; set; }

        /// <summary>
        /// Идентификатор школы
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public School School { get; set; }

        /// <summary>
        /// Причина заявления
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Дата, с которой ученик отсутствует
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую ученик отсутствует
        /// </summary>
        public DateTime DateUntil { get; set; }
    }
}
