using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Contracts;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.WriteRepositories;

namespace SchoolApplication.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IStudentWriteRepository"/>
    public class StudentWriteRepository : BaseWriteRepository<Student>, IStudentWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public StudentWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}
