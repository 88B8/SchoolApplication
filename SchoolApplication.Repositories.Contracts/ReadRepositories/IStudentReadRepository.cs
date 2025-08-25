using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Student"/>
    /// </summary>
    public interface IStudentReadRepository
    {
        /// <summary>
        /// Получает <see cref="Student"/> по идентификатору
        /// </summary>
        Task<Student?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию всех <see cref="Student"/>
        /// </summary>
        Task<IReadOnlyCollection<Student>> GetAll(CancellationToken cancellationToken);
    }
}