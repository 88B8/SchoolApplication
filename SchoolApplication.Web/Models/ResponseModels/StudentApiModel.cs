using SchoolApplication.Web.Models.Enums;

namespace SchoolApplication.Web.Models.ResponseModels
{
    /// <summary>
    /// API модель ученика
    /// </summary>
    public class StudentApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
