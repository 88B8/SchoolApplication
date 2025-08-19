namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис создания и чтения Excel документа
    /// </summary>
    public interface IExcelDocumentService
    {
        /// <summary>
        /// Импорт данных
        /// </summary>
        Task<Dictionary<string, int>> ImportFromFile(Stream fileStream, CancellationToken cancellationToken);

        /// <summary>
        /// Экспорт данных
        /// </summary>
        Task<byte[]> ExportFile(CancellationToken cancellationToken);
    }
}
