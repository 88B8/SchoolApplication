namespace SchoolApplication.Web.Models.CreateRequestApiModels
{
    /// <summary>
    /// API модель запроса создания или редактирования школы 
    /// </summary>
    public class SchoolCreateRequestApiModel
    {
        /// <summary>
        /// Название школы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ФИО директора школы
        /// </summary>
        public string DirectorName { get; set; } = string.Empty;
    }
}
