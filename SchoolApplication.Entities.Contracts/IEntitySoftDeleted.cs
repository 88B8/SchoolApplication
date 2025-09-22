namespace SchoolApplication.Entities.Contracts
{
    /// <summary>
    /// Сущность с мягким удалением
    /// </summary>
    public interface IEntitySoftDeleted
    {
        /// <summary>
        /// Дата удаления
        /// </summary>
        DateTimeOffset? DeletedAt { get; set; }
    }
}
