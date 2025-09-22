using SchoolApplication.Web.Models.BaseModels;

namespace SchoolApplication.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель родителя
    /// </summary>
    public class ParentApiModel : ParentBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}