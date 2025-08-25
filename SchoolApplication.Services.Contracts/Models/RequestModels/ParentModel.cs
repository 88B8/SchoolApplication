namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель родителя
    /// </summary>
    public class ParentModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [ExcelHeader("Идентификатор", 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [ExcelHeader("Фамилия", 2)]
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        [ExcelHeader("Имя", 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        [ExcelHeader("Отчество", 3)]
        public string Patronymic { get; set; } = string.Empty;
    }
}