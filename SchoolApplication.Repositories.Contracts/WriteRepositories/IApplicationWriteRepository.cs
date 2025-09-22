using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Application"/>
    /// </summary>
    public interface IApplicationWriteRepository : IDbWriter<Application>
    {
        
    }
}
