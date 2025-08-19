using AutoMapper;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="ISchoolService"/>
    public class SchoolService : ISchoolService, IServiceAnchor
    {
        private readonly ISchoolReadRepository schoolReadRepository;
        private readonly IValidateService validateService;
        private readonly ISchoolWriteRepository schoolWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolService(ISchoolReadRepository schoolReadRepository, IValidateService validateService, ISchoolWriteRepository schoolWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.schoolReadRepository = schoolReadRepository;
            this.validateService = validateService;
            this.schoolWriteRepository = schoolWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IReadOnlyCollection<SchoolModel>> ISchoolService.GetAll(CancellationToken cancellationToken)
        {
            var items = await schoolReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<SchoolModel>>(items);
        }

        async Task<SchoolModel> ISchoolService.Create(SchoolCreateModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var item = mapper.Map<School>(model);

            schoolWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<SchoolModel>(item);
        }

        async Task<SchoolModel> ISchoolService.Edit(SchoolModel model, CancellationToken cancellationToken)
        {
            await validateService.Validate(model, cancellationToken);
            var entity = await schoolReadRepository.GetById(model.Id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти школу с идентификатором {model.Id}");
            }

            var createdAt = entity.CreatedAt;
            entity = mapper.Map<School>(model);
            entity.CreatedAt = createdAt;

            schoolWriteRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<SchoolModel>(entity);
        }

        async Task ISchoolService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await schoolReadRepository.GetById(id, cancellationToken);

            if (entity == null)
            {
                throw new SchoolApplicationNotFoundException($"Не удалось найти школу с идентификатором {id}");
            }

            schoolWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
