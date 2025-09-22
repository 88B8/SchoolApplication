using SchoolApplication.Services.Contracts.Models.BaseModels;

namespace SchoolApplication.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания заявления
    /// </summary>
    public class ApplicationCreateModel : ApplicationBaseModel
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
    }
}
