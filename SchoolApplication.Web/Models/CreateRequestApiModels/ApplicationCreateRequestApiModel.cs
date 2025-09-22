using SchoolApplication.Web.Models.BaseModels;

namespace SchoolApplication.Web.Models.CreateRequestApiModels
{
    /// <summary>
    /// API модель запроса создания или редактирования заявления
    /// </summary>
    public class ApplicationCreateRequestApiModel : ApplicationBaseApiModel
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
