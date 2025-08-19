namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель ученика
    /// </summary>
    public class StudentModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [ExcelHeader("Идентификатор", 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        [ExcelHeader("Идентификатор родителя", 2)]
        public Guid ParentId { get; set; }

        /// <summary>
        /// Идентификатор школы
        /// </summary>
        [ExcelHeader("Идентификатор школы", 3)]
        public Guid SchoolId { get; set; }

        /// <summary>
        /// Пол ученика
        /// </summary>
        [ExcelHeader("Пол ученика", 4)]
        public GenderModel Gender { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [ExcelHeader("Фамилия", 5)]
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        [ExcelHeader("Имя", 6)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        [ExcelHeader("Отчество", 7)]
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Класс ученика
        /// </summary>
        [ExcelHeader("Класс", 8)]
        public string Grade { get; set; } = string.Empty;
    }
}