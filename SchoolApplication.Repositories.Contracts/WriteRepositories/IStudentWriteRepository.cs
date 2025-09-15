using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Student"/>
    /// </summary>
    public interface IStudentWriteRepository : IDbWriter<Student>
    {
        
    }
}