using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="IParentService"/>
    public class ParentService : IParentService, IServiceAnchor
    {
        private readonly IParentReadRepository parentReadRepository;
        private readonly IValidateService validateService;
        private readonly IParentWriteRepository parentWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentService(IParentReadRepository parentReadRepository, IValidateService validateService, IParentWriteRepository parentWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.parentReadRepository = parentReadRepository;
            this.validateService = validateService;
            this.parentWriteRepository = parentWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<ParentModel>> IParentService.GetAll(CancellationToken cancellationToken)
        {
            var items = await parentReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<ParentModel>>(items);
        }

        async Task<ParentModel> IParentService.Create(ParentCreateModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var item = mapper.Map<Parent>(model);

            parentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ParentModel>(item);
        }

        async Task<ParentModel> IParentService.Edit(ParentModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var entity = await parentReadRepository.GetById(model.Id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти родителя с идентификатором {model.Id}");
            }

            var createdAt = entity.CreatedAt;
            entity = mapper.Map<Parent>(model);
            entity.CreatedAt = createdAt;

            parentWriteRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ParentModel>(entity);
        }

        async Task IParentService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await parentReadRepository.GetById(id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти родителя с идентификатором {id}");
            }

            parentWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
