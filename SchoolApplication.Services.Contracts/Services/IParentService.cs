using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts.Models.RequestModels;

namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="Parent"/>
    /// </summary>
    public interface IParentService
    {
        /// <summary>
        /// Возвращает <see cref="ParentModel"/> по идентификатору
        /// </summary>
        Task<ParentModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список <see cref="ParentModel"/>
        /// </summary>
        Task<IReadOnlyCollection<ParentModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ParentModel"/>
        /// </summary>
        Task<ParentModel> Create(ParentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="ParentModel"/> по идентификатору
        /// </summary>
        Task<ParentModel> Edit(Guid id, ParentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="ParentModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}