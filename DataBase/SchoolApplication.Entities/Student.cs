namespace SchoolApplication.Entities
{
    /// <summary>
    /// Модель ученика
    /// </summary>
    public class Student : BaseAuditEntity
    {
        /// <summary>
        /// Пол ученика
        /// </summary>
        public Gender Gender { get; set; }

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

        /// <summary>
        /// Класс ученика
        /// </summary>
        public string Grade { get; set; } = string.Empty;
    }
}
