using SchoolApplication.Services.Contracts.Models.BaseModels;

namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель ученика
    /// </summary>
    public class StudentModel : StudentBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}