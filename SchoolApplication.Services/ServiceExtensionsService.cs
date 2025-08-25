using Microsoft.Extensions.DependencyInjection;
using SchoolApplication.Common;

namespace SchoolApplication.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsService
    {
        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        public static void RegisterServices(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }
    }
}
