namespace SchoolApplication.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания заявления
    /// </summary>
    public class ApplicationCreateModel
    {
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
        public DateOnly DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую ученик отсутствует
        /// </summary>
        public DateOnly DateUntil { get; set; }
    }
}
