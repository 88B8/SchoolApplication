using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.Models;
using SchoolApplication.Repositories.Contracts.ReadRepositories;

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
                .Select(x => new ApplicationDbModel
                {
                    Id = x.Id,
                    Student = x.Student,
                    Parent = x.Parent,
                    School = x.School,
                    Reason = x.Reason,
                    DateFrom = x.DateFrom,
                    DateUntil = x.DateUntil,
                })
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<ApplicationDbModel?> IApplicationReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .Select(x => new ApplicationDbModel
                {
                    Id = x.Id,
                    Student = x.Student,
                    Parent = x.Parent,
                    School = x.School,
                    Reason = x.Reason,
                    DateFrom = x.DateFrom,
                    DateUntil = x.DateUntil,
                })
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetByParentId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .ByParentId(id)
                .Select(x => new ApplicationDbModel
                {
                    Id = x.Id,
                    Student = x.Student,
                    Parent = x.Parent,
                    School = x.School,
                    Reason = x.Reason,
                    DateFrom = x.DateFrom,
                    DateUntil = x.DateUntil,
                })
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetBySchoolId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .BySchoolId(id)
                .Select(x => new ApplicationDbModel
                {
                    Id = x.Id,
                    Student = x.Student,
                    Parent = x.Parent,
                    School = x.School,
                    Reason = x.Reason,
                    DateFrom = x.DateFrom,
                    DateUntil = x.DateUntil,
                })
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<ApplicationDbModel>> IApplicationReadRepository.GetByStudentId(Guid id, CancellationToken cancellationToken)
            => reader.Read<Application>()
                .NotDeletedAt()
                .ByStudentId(id)
                .Select(x => new ApplicationDbModel
                {
                    Id = x.Id,
                    Student = x.Student,
                    Parent = x.Parent,
                    School = x.School,
                    Reason = x.Reason,
                    DateFrom = x.DateFrom,
                    DateUntil = x.DateUntil,
                })
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
