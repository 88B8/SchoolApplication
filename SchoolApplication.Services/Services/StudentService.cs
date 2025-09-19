using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Repositories.Contracts.WriteRepositories;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IStudentService"/>
    public class StudentService : IStudentService, IServiceAnchor
    {
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly IStudentWriteRepository studentWriteRepository;
        private readonly IApplicationWriteRepository applicationWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentService(IStudentReadRepository studentReadRepository, IApplicationReadRepository applicationReadRepository, IStudentWriteRepository studentWriteRepository, IApplicationWriteRepository applicationWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.studentReadRepository = studentReadRepository;
            this.applicationReadRepository = applicationReadRepository;
            this.studentWriteRepository = studentWriteRepository;
            this.applicationWriteRepository = applicationWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<StudentModel>> IStudentService.GetAll(CancellationToken cancellationToken)
        {
            var items = await studentReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<StudentModel>>(items);
        }

        async Task<StudentModel> IStudentService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await studentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {id}");

            return mapper.Map<StudentModel>(item);
        }

        async Task<StudentModel> IStudentService.Create(StudentCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Student>(model);

            studentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StudentModel>(item);
        }
        async Task<StudentModel> IStudentService.Edit(Guid id, StudentCreateModel model, CancellationToken cancellationToken)
        {
            var dbModel = await studentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {id}");

            var entityToUpdate = mapper.Map<Student>(dbModel);
            mapper.Map(model, entityToUpdate);

            studentWriteRepository.Update(entityToUpdate);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await studentReadRepository.GetById(entityToUpdate.Id, cancellationToken);

            return mapper.Map<StudentModel>(updatedEntity);
        }

        async Task IStudentService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await studentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {id}");

            studentWriteRepository.Delete(entity);

            var relatedApplicationDbModels = await applicationReadRepository.GetByStudentId(id, cancellationToken);
            var relatedApplications = mapper.Map<IReadOnlyCollection<Application>>(relatedApplicationDbModels);

            foreach (var application in relatedApplications)
            {
                applicationWriteRepository.Delete(application);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
