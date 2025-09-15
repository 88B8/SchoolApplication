namespace SchoolApplication.Web.Models.CreateRequestApiModels
{
    /// <summary>
    /// API модель запроса создания или редактирования родителя
    /// </summary>
    public class ParentCreateRequestApiModel
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