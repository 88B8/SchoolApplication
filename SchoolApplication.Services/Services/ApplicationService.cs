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
    /// <inheritdoc cref="IApplicationService"/>
    public class ApplicationService : IApplicationService, IServiceAnchor
    {
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly ISchoolReadRepository schoolReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IParentReadRepository parentReadRepository;
        private readonly IApplicationWriteRepository applicationWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationService(IApplicationReadRepository applicationReadRepository, ISchoolReadRepository schoolReadRepository, IStudentReadRepository studentReadRepository, IParentReadRepository parentReadRepository, IApplicationWriteRepository applicationWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.applicationReadRepository = applicationReadRepository;
            this.schoolReadRepository = schoolReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.parentReadRepository = parentReadRepository;
            this.applicationWriteRepository = applicationWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<ApplicationModel> IApplicationService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await applicationReadRepository.GetById(id, cancellationToken)
                       ?? throw new SchoolApplicationNotFoundException($"Не удалось найти заявление с идентификатором {id}");

            return mapper.Map<ApplicationModel>(item);
        }

        async Task<IReadOnlyCollection<ApplicationModel>> IApplicationService.GetAll(CancellationToken cancellationToken)
        {
            var items = await applicationReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<ApplicationModel>>(items);
        }

        async Task<ApplicationModel> IApplicationService.Create(ApplicationCreateModel model, CancellationToken cancellationToken)
        {
            await EnsureRelationsExist(model, cancellationToken);

            var item = mapper.Map<Application>(model);

            applicationWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ApplicationModel>(item);
        }

        async Task<ApplicationModel> IApplicationService.Edit(Guid id, ApplicationCreateModel model, CancellationToken cancellationToken)
        {
            var dbModel = await applicationReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти заявление с идентификатором {id}");

            await EnsureRelationsExist(model, cancellationToken);

            var entityToUpdate = mapper.Map<Application>(dbModel);
            mapper.Map(model, entityToUpdate);

            applicationWriteRepository.Update(entityToUpdate);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ApplicationModel>(entityToUpdate);
        }

        async Task IApplicationService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await applicationReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти заявление с идентификатором {id}");
            var item = mapper.Map<Application>(entity);

            applicationWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Проверяет наличие связей
        /// </summary>
        private async Task EnsureRelationsExist(ApplicationCreateModel model, CancellationToken cancellationToken)
        {
            var parent = await parentReadRepository.GetById(model.ParentId, cancellationToken)
                         ?? throw new SchoolApplicationNotFoundException(
                             $"Не удалось найти родителя с идентификатором {model.ParentId}");

            var student = await studentReadRepository.GetById(model.StudentId, cancellationToken)
                          ?? throw new SchoolApplicationNotFoundException(
                              $"Не удалось найти ученика с идентификатором {model.StudentId}");

            var school = await schoolReadRepository.GetById(model.SchoolId, cancellationToken)
                          ?? throw new SchoolApplicationNotFoundException(
                              $"Не удалось найти школу с идентификатором {model.SchoolId}");
        }
    }
}
