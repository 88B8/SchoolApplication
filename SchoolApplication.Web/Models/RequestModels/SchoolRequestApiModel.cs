namespace SchoolApplication.Web
{
    /// <summary>
    /// API модель создания или редактирования школы 
    /// </summary>
    public class SchoolRequestApiModel
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
