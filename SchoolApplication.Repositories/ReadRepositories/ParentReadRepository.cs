using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;

namespace SchoolApplication.Repositories
{
    /// <inheritdoc cref="IParentReadRepository"
    public class ParentReadRepository : IParentReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Parent>> IParentReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Parent>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Parent?> IParentReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Parent>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
