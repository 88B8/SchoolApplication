namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис импорта и экспорта
    /// </summary>
    public interface IExcelService
    {
        /// <summary>
        /// Импорт данных
        /// </summary>
        IEnumerable<T> Import<T>(Stream fileStream, string sheetName, CancellationToken cancellationToken) where T : class, new();

        /// <summary>
        /// Экспорт данных
        /// </summary>
        byte[] Export(Dictionary<string, IEnumerable<object>> sheets, CancellationToken cancellationToken);
    }
}