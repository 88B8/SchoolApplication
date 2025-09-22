namespace SchoolApplication.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель заявления
    /// </summary>
    public abstract class ApplicationBaseModel
    {
        /// <summary>
        /// Причина отсутствия
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Дата, с которой ученик отсутствует
        /// </summary>
        public DateOnly DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую ученик отсутствует
        /// </summary>
        public DateOnly DateUntil { get; set; }
    }
}
