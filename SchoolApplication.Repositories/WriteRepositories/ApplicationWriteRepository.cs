using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.WriteRepositories;

namespace SchoolApplication.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IApplicationWriteRepository"/>
    public class ApplicationWriteRepository : BaseWriteRepository<Application>, IApplicationWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}