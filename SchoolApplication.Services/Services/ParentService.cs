using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Repositories.Contracts.WriteRepositories;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IParentService"/>
    public class ParentService : IParentService, IServiceAnchor
    {
        private readonly IParentReadRepository parentReadRepository;
        private readonly IParentWriteRepository parentWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentService(
            IParentReadRepository parentReadRepository,
            IParentWriteRepository parentWriteRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.parentReadRepository = parentReadRepository;
            this.parentWriteRepository = parentWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<ParentModel>> IParentService.GetAll(CancellationToken cancellationToken)
        {
            var items = await parentReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<ParentModel>>(items);
        }

        async Task<ParentModel> IParentService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await parentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти родителя с идентификатором {id}");

            return mapper.Map<ParentModel>(item);
        }

        async Task<ParentModel> IParentService.Create(ParentCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Parent>(model);

            parentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ParentModel>(item);
        }

        async Task<ParentModel> IParentService.Edit(Guid id, ParentCreateModel model, CancellationToken cancellationToken)
        {
            var dbModel = await parentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти родителя с идентификатором {id}");

            var entityToUpdate = mapper.Map<Parent>(dbModel);
            mapper.Map(model, entityToUpdate);

            parentWriteRepository.Update(entityToUpdate);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await parentReadRepository.GetById(id, cancellationToken);

            return mapper.Map<ParentModel>(updatedEntity);
        }

        async Task IParentService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await parentReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти родителя с идентификатором {id}");

            parentWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
