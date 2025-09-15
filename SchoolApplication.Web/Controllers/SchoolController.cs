using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Web.Exceptions;
using SchoolApplication.Web.Models.CreateRequestApiModels;
using SchoolApplication.Web.Models.ResponseModels;

namespace SchoolApplication.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="SchoolModel"/>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService schoolService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolController(ISchoolService schoolService, IValidateService validateService, IMapper mapper)
        {
            this.schoolService = schoolService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает школу по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SchoolApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await schoolService.GetById(id, cancellationToken);

            return Ok(mapper.Map<SchoolApiModel>(result));
        }

        /// <summary>
        /// Получает список всех школ
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<SchoolApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await schoolService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<SchoolApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новую школу
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SchoolApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(SchoolCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<SchoolCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await schoolService.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<SchoolApiModel>(result));
        }

        /// <summary>
        /// Редактирует школу по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SchoolApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, SchoolCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<SchoolCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await schoolService.Edit(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<SchoolApiModel>(result));
        }

        /// <summary>
        /// Удаляет школу по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await schoolService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}