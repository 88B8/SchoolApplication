using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис работы с OpenXML
    /// </summary>
    public interface IExcelService
    {
        /// <summary>
        /// Экспорт заявления
        /// </summary>
        Stream Export(Application application, CancellationToken cancellationToken);
    }
}
