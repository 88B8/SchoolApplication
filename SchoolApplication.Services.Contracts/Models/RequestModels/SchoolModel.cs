namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель школы
    /// </summary>
    public class SchoolModel : SchoolCreateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}