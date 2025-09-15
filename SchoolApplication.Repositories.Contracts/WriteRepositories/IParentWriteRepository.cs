using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;

namespace SchoolApplication.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Parent"/>
    /// </summary>
    public interface IParentWriteRepository : IDbWriter<Parent>
    {

    }
}