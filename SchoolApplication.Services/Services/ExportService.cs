using AutoMapper;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Contracts.Services;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IExportService"/>
    public class ExportService : IExportService, IServiceAnchor
    {
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly IExcelService excelService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ExportService(IApplicationReadRepository applicationReadRepository, IExcelService excelService, IMapper mapper)
        {
            this.applicationReadRepository = applicationReadRepository;
            this.excelService = excelService;
            this.mapper = mapper;
        }

        async Task<Stream> IExportService.ExportById(Guid id, CancellationToken cancellationToken)
        {
            var item = await applicationReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Заявление с идентификатором {id} не найдено");

            return excelService.Export(mapper.Map<Entities.Application>(item), cancellationToken);
        }
    }
}