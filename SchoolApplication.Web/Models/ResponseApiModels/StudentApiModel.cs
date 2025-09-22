using SchoolApplication.Web.Models.BaseModels;

namespace SchoolApplication.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель ученика
    /// </summary>
    public class StudentApiModel : StudentBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
