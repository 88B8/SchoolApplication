namespace SchoolApplication.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель родителя
    /// </summary>
    public class ParentModel : ParentCreateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}