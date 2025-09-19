namespace SchoolApplication.Entities.Contracts
{
    /// <summary>
    /// Сущность с идентификатором родителя
    /// </summary>
    public interface IEntityWithParentId
    {
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        Guid ParentId { get; set; }
    }
}
