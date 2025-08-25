using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="ApplicationModel"/>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;
        private readonly IExportService exportService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationController(IApplicationService applicationService, IExportService exportService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.exportService = exportService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Экспортирует заявление по идентификатору
        /// </summary>
        /// GET: /api/id/export
        [HttpGet("{id:guid}/export")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExportById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var excelBytes = await exportService.ExportById(id, cancellationToken);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
        }

        /// <summary>
        /// Получает список всех заявлений
        /// </summary>
        /// GET: /api/Application/
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
        /// POST: /api/Application/
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ApplicationRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ApplicationCreateModel>(request);
            var result = await applicationService.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ApplicationApiModel>(result));
        }

        /// <summary>
        /// Редактирует заявление по идентификатору
        /// </summary>
        /// PUT: /api/Application/id
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] ApplicationRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ApplicationModel>(request);
            requestModel.Id = id;

            var result = await applicationService.Edit(requestModel, cancellationToken);

            return Ok(mapper.Map<ApplicationApiModel>(result));
        }

        /// <summary>
        /// Удаляет заявление по идентификатору
        /// </summary>
        /// DELETE: /api/Application/id
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await applicationService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
