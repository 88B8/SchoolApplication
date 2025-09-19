namespace SchoolApplication.Entities.Contracts
{
    /// <summary>
    /// Сущность с идентификатором школы
    /// </summary>
    public interface IEntityWithSchoolId
    {
        /// <summary>
        /// Идентификатор школы
        /// </summary>
        Guid SchoolId { get; set; }
    }
}
