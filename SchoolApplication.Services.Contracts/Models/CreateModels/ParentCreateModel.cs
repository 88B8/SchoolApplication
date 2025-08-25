namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель создания родителя
    /// </summary>
    public class ParentCreateModel
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
