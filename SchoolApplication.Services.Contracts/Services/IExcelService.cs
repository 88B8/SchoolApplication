using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис работы с OpenXML
    /// </summary>
    public interface IExcelService
    {
        /// <summary>
        /// Экспорт заявления
        /// </summary>
        byte[] Export(Application application, CancellationToken cancellationToken);
    }
}
