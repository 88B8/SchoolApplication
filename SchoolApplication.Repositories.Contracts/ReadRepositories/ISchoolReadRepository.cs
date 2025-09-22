using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="School"/>
    /// </summary>
    public interface ISchoolReadRepository
    {
        /// <summary>
        /// Получает <see cref="School"/> по идентификатору
        /// </summary>
        Task<School?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="School"/>
        /// </summary>
        Task<IReadOnlyCollection<School>> GetAll(CancellationToken cancellationToken);
    }
}