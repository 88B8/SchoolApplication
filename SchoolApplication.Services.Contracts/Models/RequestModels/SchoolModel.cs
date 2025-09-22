using SchoolApplication.Services.Contracts.Models.BaseModels;

namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель школы
    /// </summary>
    public class SchoolModel : SchoolBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}