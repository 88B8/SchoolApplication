using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="SchoolModel"/>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService schoolService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolController(ISchoolService schoolService, IMapper mapper)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех <see cref="SchoolModel"/>
        /// </summary>
        /// GET: /School/
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<SchoolApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await schoolService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<SchoolApiModel>>(result));
        }

        /// <summary>
        /// Добавляет <see cref="SchoolModel"/>
        /// </summary>
        /// POST: /School/
        [HttpPost]
        [ProducesResponseType(typeof(SchoolApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(SchoolRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<SchoolCreateModel>(request);
            var result = await schoolService.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<SchoolCreateModel>(request));
        }

        /// <summary>
        /// Редактирует <see cref="SchoolModel"/>
        /// </summary>
        /// PUT: /School/id
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SchoolApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] SchoolRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<SchoolModel>(request);
            requestModel.Id = id;

            var result = await schoolService.Edit(requestModel, cancellationToken);

            return Ok(mapper.Map<SchoolApiModel>(result));
        }

        /// <summary>
        /// Удаляет <see cref="SchoolModel"/> по идентификатору
        /// </summary>
        /// DELETE: /School/id
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await schoolService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}