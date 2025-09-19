using SchoolApplication.Web.Models.Enums;

namespace SchoolApplication.Web.Models.CreateRequestApiModels
{
    /// <summary>
    /// API модель запроса создания или редактирования ученика
    /// </summary>
    public class StudentCreateRequestApiModel
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
