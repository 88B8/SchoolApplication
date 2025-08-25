namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель создания школы
    /// </summary>
    public class SchoolCreateModel
    {
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