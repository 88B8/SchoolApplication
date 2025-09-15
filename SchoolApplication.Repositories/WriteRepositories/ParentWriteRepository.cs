using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.WriteRepositories;

namespace SchoolApplication.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IParentWriteRepository"/>
    public class ParentWriteRepository : BaseWriteRepository<Parent>, IParentWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ParentWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}
