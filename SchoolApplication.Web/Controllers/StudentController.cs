using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Web.Exceptions;
using SchoolApplication.Web.Models.CreateRequestApiModels;
using SchoolApplication.Web.Models.ResponseModels;

namespace SchoolApplication.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с <see cref="StudentModel"/>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentController(IStudentService studentService, IValidateService validateService, IMapper mapper)
        {
            this.studentService = studentService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает ученика по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(StudentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await studentService.GetById(id, cancellationToken);

            return Ok(mapper.Map<StudentApiModel>(result));
        }

        /// <summary>
        /// Получает список всех учеников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<StudentApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await studentService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<StudentApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового ученика
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(StudentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(StudentCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<StudentCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await studentService.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<StudentApiModel>(result));
        }

        /// <summary>
        /// Редактирует ученика по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(StudentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, StudentCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<StudentCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await studentService.Edit(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<StudentApiModel>(result));
        }

        /// <summary>
        /// Удаляет ученика по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await studentService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}