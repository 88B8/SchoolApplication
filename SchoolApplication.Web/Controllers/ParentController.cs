using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="ParentModel"/>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService parentService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentController(IParentService parentService, IMapper mapper)
        {
            this.parentService = parentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех <see cref="ParentModel"/>
        /// </summary>
        /// GET: /Parent/
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ParentApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await parentService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<ParentApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового <see cref="ParentModel"/>
        /// </summary>
        /// POST: /Parent/
        [HttpPost]
        [ProducesResponseType(typeof(ParentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ParentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ParentCreateModel>(request);
            var result = await parentService.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ParentCreateModel>(request));
        }

        /// <summary>
        /// Редактирует <see cref="ParentModel"/>
        /// </summary>
        /// PUT: /Parent/id
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ParentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] ParentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ParentModel>(request);
            requestModel.Id = id;

            var result = await parentService.Edit(requestModel, cancellationToken);

            return Ok(mapper.Map<ParentApiModel>(result));
        }

        /// <summary>
        /// Удаляет <see cref="ParentModel"/> по идентификатору
        /// </summary>
        /// DELETE: /Parent/id
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await parentService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}