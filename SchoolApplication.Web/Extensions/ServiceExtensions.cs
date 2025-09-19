using SchoolApplication.Common.Contracts;
using SchoolApplication.Common.Infrastructure;
using SchoolApplication.Context;
using SchoolApplication.Repositories;
using SchoolApplication.Services;
using SchoolApplication.Services.Infrastructure;
using SchoolApplication.Web.Infrastructure;

namespace SchoolApplication.Web.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует зависимости
        /// </summary>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.RegisterServices();
            services.RegisterRepositories();
            services.RegisterContext();
            services.AddAutoMapper(typeof(ServiceProfile), typeof(ApiMapper));
        }
    }
}
