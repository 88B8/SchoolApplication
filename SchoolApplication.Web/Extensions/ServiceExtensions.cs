using SchoolApplication.Common;
using SchoolApplication.Context;
using SchoolApplication.Repositories;
using SchoolApplication.Services;

namespace SchoolApplication.Web
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
