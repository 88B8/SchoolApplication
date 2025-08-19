using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис <see cref="Application"/>
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Возвращает список <see cref="ApplicationModel"/>
        /// </summary>
        Task<IReadOnlyCollection<ApplicationModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ApplicationModel"/>
        /// </summary>
        Task<ApplicationModel> Create(ApplicationCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="ApplicationModel"/>
        /// </summary>
        Task<ApplicationModel> Edit(ApplicationModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="Application"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
