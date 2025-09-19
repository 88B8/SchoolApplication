namespace SchoolApplication.Entities
{
    /// <summary>
    /// Модель школы
    /// </summary>
    public class School : BaseAuditEntity
    {
        /// <summary>
        /// Название школы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ФИО директора школы
        /// </summary>
        public string DirectorName { get; set; } = string.Empty;
    }
}
