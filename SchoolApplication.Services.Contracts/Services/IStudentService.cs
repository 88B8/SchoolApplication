using SchoolApplication.Entities;

namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Сервис <see cref="Student"/>
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Возвращает список <see cref="StudentModel"/>
        /// </summary>
        Task<IReadOnlyCollection<StudentModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="StudentModel"/>
        /// </summary>
        Task<StudentModel> Create(StudentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует сущность <see cref="StudentModel"/>
        /// </summary>
        Task<StudentModel> Edit(StudentModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="Student"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}