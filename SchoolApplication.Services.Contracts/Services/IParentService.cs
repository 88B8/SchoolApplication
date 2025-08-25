using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис <see cref="Parent"/>
    /// </summary>
    public interface IParentService
    {
        /// <summary>
        /// Возвращает список <see cref="ParentModel"/>
        /// </summary>
        Task<IReadOnlyCollection<ParentModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ParentModel"/>
        /// </summary>
        Task<ParentModel> Create(ParentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="ParentModel"/>
        /// </summary>
        Task<ParentModel> Edit(ParentModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="Parent"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}