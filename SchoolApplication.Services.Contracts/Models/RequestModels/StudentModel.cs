using SchoolApplication.Services.Contracts.Models.CreateModels;

namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель ученика
    /// </summary>
    public class StudentModel : StudentCreateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}