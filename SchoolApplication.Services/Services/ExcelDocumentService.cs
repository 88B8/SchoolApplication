using AutoMapper;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Services;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IExcelDocumentService"
    public class ExcelDocumentService : IExcelDocumentService, IServiceAnchor
    {
        private readonly IApplicationService applicationService;
        private readonly IParentService parentService;
        private readonly ISchoolService schoolService;
        private readonly IStudentService studentService;
        private readonly IExcelService excelService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ExcelDocumentService(IApplicationService applicationService, IParentService parentService, ISchoolService schoolService, IStudentService studentService, IExcelService excelService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.parentService = parentService;
            this.schoolService = schoolService;
            this.studentService = studentService;
            this.excelService = excelService;
            this.mapper = mapper;
        }

        public async Task<byte[]> ExportFile(CancellationToken cancellationToken)
        {
            var applications = await applicationService.GetAll(cancellationToken);
            var parents = await parentService.GetAll(cancellationToken);
            var schools = await schoolService.GetAll(cancellationToken);
            var students = await studentService.GetAll(cancellationToken);

            var sheets = new Dictionary<string, IEnumerable<object>>
            {
                { "Заявления", applications },
                { "Родители", parents },
                { "Школы", schools },
                { "Ученики", students },
            };

            return excelService.Export(sheets, cancellationToken);
        }

        public async Task<Dictionary<string, int>> ImportFromFile(Stream fileStream, CancellationToken cancellationToken)
        {
            var result = new Dictionary<string, int>();
            result["Applications"] = 0;
            result["Parents"] = 0;
            result["Schools"] = 0;
            result["Students"] = 0;

            var applications = excelService.Import<ApplicationModel>(fileStream, "Заявления", cancellationToken);
            foreach (var application in applications)
            {
                var model = mapper.Map<ApplicationCreateModel>(application);
                await applicationService.Create(model, cancellationToken);
                result["Applications"] += 1;
            }

            var parents = excelService.Import<ParentModel>(fileStream, "Родители", cancellationToken);
            foreach (var parent in parents)
            {
                var model = mapper.Map<ParentCreateModel>(parent);
                await parentService.Create(model, cancellationToken);
                result["Parents"] += 1;
            }

            var schools = excelService.Import<SchoolModel>(fileStream, "Школы", cancellationToken);
            foreach (var school in schools)
            {
                var model = mapper.Map<SchoolCreateModel>(school);
                await schoolService.Create(model, cancellationToken);
                result["Schools"] += 1;
            }

            var students = excelService.Import<StudentModel>(fileStream, "Ученики", cancellationToken);
            foreach (var student in students)
            {
                var model = mapper.Map<StudentCreateModel>(student);
                await studentService.Create(model, cancellationToken);
                result["Students"] += 1;
            }

            return result;
        }
    }
}
