namespace SchoolApplication.Entities
{
    /// <summary>
    /// Модель заявления
    /// </summary>
    public class Application : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

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
