namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис экспорта
    /// </summary>
    public interface IExportService
    {
        /// <summary>
        /// Экспорт заявления по идентификатору
        /// </summary>
        Task<byte[]> ExportById(Guid id, CancellationToken cancellationToken);
    }
}