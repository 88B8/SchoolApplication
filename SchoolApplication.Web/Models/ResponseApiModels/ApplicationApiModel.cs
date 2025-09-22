using SchoolApplication.Web.Models.BaseModels;

namespace SchoolApplication.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель заявления
    /// </summary>
    public class ApplicationApiModel : ApplicationBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство ученика
        /// </summary>
        public StudentApiModel Student { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство родителя
        /// </summary>
        public ParentApiModel Parent { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство школы
        /// </summary>
        public SchoolApiModel School { get; set; } = null!;
    }
}
