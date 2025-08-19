namespace SchoolApplication.Web
{
    /// <summary>
    /// API модель школы
    /// </summary>
    public class SchoolApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название школы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Имя директора школы
        /// </summary>
        public string DirectorName { get; set; } = string.Empty;
    }
}
