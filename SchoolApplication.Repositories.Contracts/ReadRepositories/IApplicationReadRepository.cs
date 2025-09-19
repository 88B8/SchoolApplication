using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.Models;

namespace SchoolApplication.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Application"/>
    /// </summary>
    public interface IApplicationReadRepository
    {
        /// <summary>
        /// Получает <see cref="Application"/> по идентификатору
        /// </summary>
        Task<ApplicationDbModel?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Application"/>
        /// </summary>
        Task<IReadOnlyCollection<ApplicationDbModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Application"/> по идентификатору ученика
        /// </summary>
        Task<IReadOnlyCollection<ApplicationDbModel>> GetByStudentId(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Application"/> по идентификатору школы
        /// </summary>
        Task<IReadOnlyCollection<ApplicationDbModel>> GetBySchoolId(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Application"/> по идентификатору родителя
        /// </summary>
        Task<IReadOnlyCollection<ApplicationDbModel>> GetByParentId(Guid id, CancellationToken cancellationToken);
    }
}
