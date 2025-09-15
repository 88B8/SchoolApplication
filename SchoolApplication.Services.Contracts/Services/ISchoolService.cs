using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts.Models.RequestModels;

namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="School"/>
    /// </summary>
    public interface ISchoolService
    {
        /// <summary>
        /// Возвращает <see cref="SchoolModel"/> по идентификатору
        /// </summary>
        Task<SchoolModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список <see cref="SchoolModel"/>
        /// </summary>
        Task<IReadOnlyCollection<SchoolModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="SchoolModel"/>
        /// </summary>
        Task<SchoolModel> Create(SchoolCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="SchoolModel"/> по идентификатору
        /// </summary>
        Task<SchoolModel> Edit(Guid id, SchoolCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="SchoolModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}