using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис <see cref="School"/>
    /// </summary>
    public interface ISchoolService
    {
        /// <summary>
        /// Возвращает список <see cref="SchoolModel"/>
        /// </summary>
        Task<IReadOnlyCollection<SchoolModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="SchoolModel"/>
        /// </summary>
        Task<SchoolModel> Create(SchoolCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="SchoolModel"/>
        /// </summary>
        Task<SchoolModel> Edit(SchoolModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="School"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}