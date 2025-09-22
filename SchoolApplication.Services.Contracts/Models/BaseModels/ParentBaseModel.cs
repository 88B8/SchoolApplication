namespace SchoolApplication.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель родителя
    /// </summary>
    public abstract class ParentBaseModel
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;
    }
}
