using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="ApplicationModel"/>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationController(IApplicationService applicationService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех <see cref="ApplicationModel"/>
        /// </summary>
        /// GET: /Application/
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ApplicationApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await applicationService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<ApplicationApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новое <see cref="ApplicationModel"/>
        /// </summary>
        /// POST: /Application/
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ApplicationRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ApplicationCreateModel>(request);
            var result = await applicationService.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ApplicationCreateModel>(request));
        }

        /// <summary>
        /// Редактирует <see cref="ApplicationModel"/>
        /// </summary>
        /// PUT: /Application/id
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
        /// Удаляет <see cref="ApplicationModel"/> по идентификатору
        /// </summary>
        /// DELETE: /Application/id
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await applicationService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
