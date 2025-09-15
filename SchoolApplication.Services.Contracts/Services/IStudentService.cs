using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;

namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="Student"/>
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Возвращает <see cref="StudentModel"/> по идентификатору
        /// </summary>
        Task<StudentModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список <see cref="StudentModel"/>
        /// </summary>
        Task<IReadOnlyCollection<StudentModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="StudentModel"/>
        /// </summary>
        Task<StudentModel> Create(StudentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="StudentModel"/> по идентификатору
        /// </summary>
        Task<StudentModel> Edit(Guid id, StudentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="StudentModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}