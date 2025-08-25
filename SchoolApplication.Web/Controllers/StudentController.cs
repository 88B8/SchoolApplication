using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="StudentModel"/>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService schoolService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentController(IStudentService schoolService, IMapper mapper)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех учеников
        /// </summary>
        /// GET: /api/Student/
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<StudentApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await schoolService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<StudentApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового ученика
        /// </summary>
        /// POST: /api/Student/
        [HttpPost]
        [ProducesResponseType(typeof(StudentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(StudentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<StudentCreateModel>(request);
            var result = await schoolService.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<StudentApiModel>(result));
        }

        /// <summary>
        /// Редактирует ученика по идентификатору
        /// </summary>
        /// PUT: /api/Student/id
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(StudentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] StudentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<StudentModel>(request);
            requestModel.Id = id;

            var result = await schoolService.Edit(requestModel, cancellationToken);

            return Ok(mapper.Map<StudentApiModel>(result));
        }

        /// <summary>
        /// Удаляет ученика по идентификатору
        /// </summary>
        /// DELETE: /api/Student/id
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