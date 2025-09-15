namespace SchoolApplication.Web.Models.ResponseModels
{
    /// <summary>
    /// API модель заявления
    /// </summary>
    public class ApplicationApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство ученика
        /// </summary>
        public StudentApiModel Student { get; set; } = new StudentApiModel();

        /// <summary>
        /// Навигационное свойство родителя
        /// </summary>
        public ParentApiModel Parent { get; set; } = new ParentApiModel();

        /// <summary>
        /// Навигационное свойство школы
        /// </summary>
        public SchoolApiModel School { get; set; } = new SchoolApiModel();

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
