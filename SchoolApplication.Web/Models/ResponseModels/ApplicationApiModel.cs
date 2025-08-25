namespace SchoolApplication.Web
{
    /// <summary>
    /// API модель заявления
    /// </summary>
    public class ApplicationApiModel
    {
        /// <summary>
        /// Идентификтатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор ученика
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Идентификатор школы
        /// </summary>
        public Guid SchoolId { get; set; }

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
