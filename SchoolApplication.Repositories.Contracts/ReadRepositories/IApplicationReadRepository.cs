using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Application"/>
    /// </summary>
    public interface IApplicationReadRepository
    {
        /// <summary>
        /// Получает <see cref="Application"/> по идентификатору
        /// </summary>
        Task<Application?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Application"/>
        /// </summary>
        Task<IReadOnlyCollection<Application>> GetAll(CancellationToken cancellationToken);
    }
}
