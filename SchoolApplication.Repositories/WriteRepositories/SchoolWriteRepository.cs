using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.WriteRepositories;

namespace SchoolApplication.Repositories.WriteRepositories
{
    /// <inheritdoc cref="ISchoolWriteRepository"/>
    public class SchoolWriteRepository : BaseWriteRepository<School>, ISchoolWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SchoolWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}
