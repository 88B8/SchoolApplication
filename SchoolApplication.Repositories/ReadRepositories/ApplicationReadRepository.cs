using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;

namespace SchoolApplication.Repositories
{
    /// <inheritdoc cref="IApplicationReadRepository"
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

        Task<IReadOnlyCollection<Application>> IApplicationReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Application>()
                .Include(a => a.Student)
                .Include(a => a.Parent)
                .Include(a => a.School)
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Application?> IApplicationReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .Include(a => a.Student)
                .Include(a => a.Parent)
                .Include(a => a.School)
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
