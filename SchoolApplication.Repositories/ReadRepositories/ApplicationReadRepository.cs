using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.Models;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Repositories.Extensions;
using SchoolApplication.Repositories.Specs;

namespace SchoolApplication.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IApplicationReadRepository"/>
    public class ApplicationReadRepository : IApplicationReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .ToDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<ApplicationDbModel?> IApplicationReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .ToDbModel()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Application?> IApplicationReadRepository.GetByIdRaw(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetByParentId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .Where(x => x.ParentId == id)
                .ToDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetBySchoolId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .Where(x => x.SchoolId == id)
                .ToDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetByStudentId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .Where(x => x.StudentId == id)
                .ToDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
