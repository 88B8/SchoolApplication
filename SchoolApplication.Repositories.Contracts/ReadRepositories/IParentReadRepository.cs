using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Parent"/>
    /// </summary>
    public interface IParentReadRepository
    {
        /// <summary>
        /// Получает <see cref="Parent"/> по идентификатору
        /// </summary>
        Task<Parent?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Parent"/>
        /// </summary>
        Task<IReadOnlyCollection<Parent>> GetAll(CancellationToken cancellationToken);
    }
}
