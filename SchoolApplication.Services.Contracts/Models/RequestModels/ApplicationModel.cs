namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель заявления
    /// </summary>
    public class ApplicationModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [ExcelHeader("Идентификатор", 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор ученика
        /// </summary>
        [ExcelHeader("Идентификатор ученика", 2)]
        public Guid StudentId { get; set; }

        /// <summary>
        /// Причина заявления
        /// </summary>
        [ExcelHeader("Причина отсутствия", 3)]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Дата, с которой ученик отсутствует
        /// </summary>
        [ExcelHeader("Дата, с которой ученик отсутствует", 4)]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую ученик отсутствует
        /// </summary>
        [ExcelHeader("Дата, по которую ученик отсутствует", 5)]
        public DateTime DateUntil { get; set; }
    }
}
