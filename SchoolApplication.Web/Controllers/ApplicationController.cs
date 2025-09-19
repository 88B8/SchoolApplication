using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Web.Exceptions;
using SchoolApplication.Web.Models.CreateRequestApiModels;
using SchoolApplication.Web.Models.ResponseModels;

namespace SchoolApplication.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="ApplicationModel"/>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;
        private readonly IExportService exportService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationController(IApplicationService applicationService, IExportService exportService, IValidateService validateService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.exportService = exportService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Экспортирует заявление по идентификатору
        /// </summary>
        [HttpGet("{id:guid}/export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExportById(Guid id, CancellationToken cancellationToken)
        {
            var stream = await exportService.ExportById(id, cancellationToken);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
        }

        /// <summary>
        /// Получает заявление по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await applicationService.GetById(id, cancellationToken);

            return Ok(mapper.Map<ApplicationApiModel>(result));
        }

        /// <summary>
        /// Получает список всех заявлений
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ApplicationApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await applicationService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<ApplicationApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новое заявление
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(ApplicationCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ApplicationCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await applicationService.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ApplicationApiModel>(result));
        }

        /// <summary>
        /// Редактирует заявление по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, ApplicationCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ApplicationCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await applicationService.Edit(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ApplicationApiModel>(result));
        }

        /// <summary>
        /// Удаляет заявление по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await applicationService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}
