using SchoolApplication.Services.Contracts.Models.Enums;

namespace SchoolApplication.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель ученика
    /// </summary>
    public abstract class StudentBaseModel
    {
        /// <summary>
        /// Пол ученика
        /// </summary>
        public GenderModel Gender { get; set; }

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
