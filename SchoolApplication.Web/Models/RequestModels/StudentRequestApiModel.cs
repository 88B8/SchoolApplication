namespace SchoolApplication.Web
{
    /// <summary>
    /// API модель создания или редактирования ученика
    /// </summary>
    public class StudentRequestApiModel
    {
        /// <summary>
        /// Пол ученика
        /// </summary>
        public GenderApiModel Gender { get; set; }

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
