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
    /// <inheritdoc cref="ISchoolService"/>
    public class SchoolService : ISchoolService, IServiceAnchor
    {
        private readonly ISchoolReadRepository schoolReadRepository;
        private readonly IApplicationReadRepository applicationReadRepository;
        private readonly ISchoolWriteRepository schoolWriteRepository;
        private readonly IApplicationWriteRepository applicationWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolService(ISchoolReadRepository schoolReadRepository, IApplicationReadRepository applicationReadRepository, ISchoolWriteRepository schoolWriteRepository, IApplicationWriteRepository applicationWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.schoolReadRepository = schoolReadRepository;
            this.applicationReadRepository = applicationReadRepository;
            this.schoolWriteRepository = schoolWriteRepository;
            this.applicationWriteRepository = applicationWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<SchoolModel>> ISchoolService.GetAll(CancellationToken cancellationToken)
        {
            var items = await schoolReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<SchoolModel>>(items);
        }

        async Task<SchoolModel> ISchoolService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await schoolReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти ученика с идентификатором {id}");

            return mapper.Map<SchoolModel>(item);
        }

        async Task<SchoolModel> ISchoolService.Create(SchoolCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<School>(model);

            schoolWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<SchoolModel>(item);
        }

        async Task<SchoolModel> ISchoolService.Edit(Guid id, SchoolCreateModel model, CancellationToken cancellationToken)
        {
            var dbModel = await schoolReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти школу с идентификатором {id}");

            var entityToUpdate = mapper.Map<School>(dbModel);
            mapper.Map(model, entityToUpdate);

            schoolWriteRepository.Update(entityToUpdate);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await schoolReadRepository.GetById(id, cancellationToken);

            return mapper.Map<SchoolModel>(updatedEntity);
        }

        async Task ISchoolService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await schoolReadRepository.GetById(id, cancellationToken)
                ?? throw new SchoolApplicationNotFoundException($"Не удалось найти школу с идентификатором {id}");

            schoolWriteRepository.Delete(entity);

            var relatedApplicationDbModels = await applicationReadRepository.GetBySchoolId(id, cancellationToken);
            var relatedApplications = mapper.Map<IReadOnlyCollection<Application>>(relatedApplicationDbModels);

            foreach (var application in relatedApplications)
            {
                applicationWriteRepository.Delete(application);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}