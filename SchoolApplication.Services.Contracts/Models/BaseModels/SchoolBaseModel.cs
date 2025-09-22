namespace SchoolApplication.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель школы
    /// </summary>
    public abstract class SchoolBaseModel
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
