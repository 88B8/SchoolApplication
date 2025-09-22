using SchoolApplication.Web.Models.Enums;

namespace SchoolApplication.Web.Models.BaseModels
{
    /// <summary>
    /// Базовая API-модель ученика
    /// </summary>
    public abstract class StudentBaseApiModel
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
