using SchoolApplication.Services.Contracts.Models.BaseModels;

namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель родителя
    /// </summary>
    public class ParentModel : ParentBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}