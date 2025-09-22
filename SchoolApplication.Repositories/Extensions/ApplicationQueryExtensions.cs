using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.Models;

namespace SchoolApplication.Repositories.Extensions
{
    /// <summary>
    /// Расширения для проекции сущности <see cref="Application"/> в <see cref="ApplicationDbModel"/>
    /// </summary>
    public static class ApplicationQueryExtensions
    {
        /// <summary>
        /// Проецирует сущность <see cref="Application"/> в <see cref="ApplicationDbModel"/>
        /// </summary>
        public static IQueryable<ApplicationDbModel> ToDbModel(this IQueryable<Application> query)
            => query.Select(x => new ApplicationDbModel
            {
                Id = x.Id,
                Student = x.Student,
                Parent = x.Parent,
                School = x.School,
                Reason = x.Reason,
                DateFrom = x.DateFrom,
                DateUntil = x.DateUntil,
            });
    }
}
