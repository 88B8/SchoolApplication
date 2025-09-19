using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="School"/>
    /// </summary>
    public interface ISchoolWriteRepository : IDbWriter<School>
    {
        
    }
}