using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="IExportService"/>
    public class ExportService : IExportService, IServiceAnchor
    {
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly IExcelService excelService;

        /// <summary>
        /// ctor
        /// </summary>
        public ExportService(IApplicationReadRepository applicationReadRepository, IExcelService excelService)
        {
            this.applicationReadRepository = applicationReadRepository;
            this.excelService = excelService;
        }

        async Task<byte[]> IExportService.ExportById(Guid id, CancellationToken cancellationToken)
        {
            var application = await applicationReadRepository.GetById(id, cancellationToken);

            if (application == null)
            {
                throw new SchoolApplicationNotFoundException($"Заявление с идентификатором {id} не найдено");
            }

            return excelService.Export(application, cancellationToken);
        }
    }
}