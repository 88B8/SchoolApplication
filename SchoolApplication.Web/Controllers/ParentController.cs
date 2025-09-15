using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Services.Services;
using SchoolApplication.Web.Exceptions;
using SchoolApplication.Web.Models.CreateRequestApiModels;
using SchoolApplication.Web.Models.ResponseModels;

namespace SchoolApplication.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="ParentModel"/>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService parentService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentController(IParentService parentService, IValidateService validateService, IMapper mapper)
        {
            this.parentService = parentService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает родителя по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ParentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await parentService.GetById(id, cancellationToken);

            return Ok(mapper.Map<ParentApiModel>(result));
        }

        /// <summary>
        /// Получает список всех родителей
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ParentApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await parentService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<ParentApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового родителя
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ParentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(ParentCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ParentCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await parentService.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ParentApiModel>(result));
        }

        /// <summary>
        /// Редактирует родителя по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ParentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, ParentCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ParentCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await parentService.Edit(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ParentApiModel>(result));
        }

        /// <summary>
        /// Удаляет родителя по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await parentService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}