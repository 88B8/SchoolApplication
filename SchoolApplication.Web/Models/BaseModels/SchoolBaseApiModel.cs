namespace SchoolApplication.Web.Models.BaseModels
{
    /// <summary>
    /// Базовая API-модель школы
    /// </summary>
    public abstract class SchoolBaseApiModel
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
