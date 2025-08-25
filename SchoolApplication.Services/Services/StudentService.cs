using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="IStudentService"/>
    public class StudentService : IStudentService, IServiceAnchor
    {
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IValidateService validateService;
        private readonly IStudentWriteRepository studentWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentService(IStudentReadRepository studentReadRepository, IValidateService validateService, IStudentWriteRepository studentWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.studentReadRepository = studentReadRepository;
            this.validateService = validateService;
            this.studentWriteRepository = studentWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<StudentModel>> IStudentService.GetAll(CancellationToken cancellationToken)
        {
            var items = await studentReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<StudentModel>>(items);
        }

        async Task<StudentModel> IStudentService.Create(StudentCreateModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var item = mapper.Map<Student>(model);

            studentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StudentModel>(item);
        }

        async Task<StudentModel> IStudentService.Edit(StudentModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var entity = await studentReadRepository.GetById(model.Id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {model.Id}");
            }

            var createdAt = entity.CreatedAt;
            entity = mapper.Map<Student>(model);
            entity.CreatedAt = createdAt;

            studentWriteRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StudentModel>(entity);
        }

        async Task IStudentService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await studentReadRepository.GetById(id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {id}");
            }

            studentWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
