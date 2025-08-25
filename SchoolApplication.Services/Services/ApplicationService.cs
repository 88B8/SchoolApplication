using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="IApplicationService"/>
    public class ApplicationService : IApplicationService, IServiceAnchor
    {
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly IValidateService validateService;
        private readonly IApplicationWriteRepository applicationWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationService(IApplicationReadRepository applicationReadRepository, IMapper mapper, IValidateService validateService, IUnitOfWork unitOfWork, IApplicationWriteRepository applicationWriteRepository)
        {
            this.applicationReadRepository = applicationReadRepository;
            this.mapper = mapper;
            this.validateService = validateService;
            this.unitOfWork = unitOfWork;
            this.applicationWriteRepository = applicationWriteRepository;
        }

        async Task<ApplicationModel> IApplicationService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await applicationReadRepository.GetById(id, cancellationToken);

            return mapper.Map<ApplicationModel>(item);
        }

        async Task<IReadOnlyCollection<ApplicationModel>> IApplicationService.GetAll(CancellationToken cancellationToken)
        {
            var items = await applicationReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<ApplicationModel>>(items);
        }

        async Task<ApplicationModel> IApplicationService.Create(ApplicationCreateModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var item = mapper.Map<Application>(model);

            applicationWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ApplicationModel>(item);
        }

        async Task<ApplicationModel> IApplicationService.Edit(ApplicationModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var entity = await applicationReadRepository.GetById(model.Id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти заявление с идентификатором {model.Id}");
            }

            var createdAt = entity.CreatedAt;
            entity = mapper.Map<Application>(model);
            entity.CreatedAt = createdAt;

            applicationWriteRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ApplicationModel>(entity);
        }

        async Task IApplicationService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await applicationReadRepository.GetById(id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти заявление с идентификатором {id}");
            }

            applicationWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
