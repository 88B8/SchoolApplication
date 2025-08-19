using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts;

namespace SchoolApplication.Repositories
{
    /// <inheritdoc cref="IStudentReadRepository"
    public class StudentReadRepository : IStudentReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Student>> IStudentReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Student>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Student?> IStudentReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Student>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
