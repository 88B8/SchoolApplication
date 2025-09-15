using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.ReadRepositories;

namespace SchoolApplication.Repositories.ReadRepositories
{
    /// <inheritdoc cref="ISchoolReadRepository"/>
    public class SchoolReadRepository : ISchoolReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<School>> ISchoolReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<School>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<School?> ISchoolReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<School>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
