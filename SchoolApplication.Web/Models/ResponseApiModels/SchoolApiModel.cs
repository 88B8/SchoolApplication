using SchoolApplication.Web.Models.BaseModels;

namespace SchoolApplication.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель школы
    /// </summary>
    public class SchoolApiModel : SchoolBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
