using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;

namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="Application"/>
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Возвращает <see cref="ApplicationModel"/> по идентификатору
        /// </summary>
        Task<ApplicationModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список <see cref="ApplicationModel"/>
        /// </summary>
        Task<IReadOnlyCollection<ApplicationModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ApplicationModel"/>
        /// </summary>
        Task<ApplicationModel> Create(ApplicationCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="ApplicationModel"/> по идентификатору
        /// </summary>
        Task<ApplicationModel> Edit(Guid id, ApplicationCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="ApplicationModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
