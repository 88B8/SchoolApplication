namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель заявления
    /// </summary>
    public class ApplicationModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство родителя
        /// </summary>
        public ParentModel Parent { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство школы
        /// </summary>
        public SchoolModel School { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство ученика
        /// </summary>
        public StudentModel Student { get; set; } = null!;

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
