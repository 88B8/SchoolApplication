namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель школы
    /// </summary>
    public class SchoolModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [ExcelHeader("Идентификатор", 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название школы
        /// </summary>
        [ExcelHeader("Название", 2)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Имя директора школы
        /// </summary>
        [ExcelHeader("Имя директора", 3)]
        public string DirectorName { get; set; } = string.Empty;
    }
}