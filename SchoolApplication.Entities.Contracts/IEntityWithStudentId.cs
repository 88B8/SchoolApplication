namespace SchoolApplication.Entities.Contracts
{
    /// <summary>
    /// Сущность с идентификатором ученика
    /// </summary>
    public interface IEntityWithStudentId
    {
        /// <summary>
        /// Идентификатор ученика
        /// </summary>
        Guid StudentId { get; set; }
    }
}
