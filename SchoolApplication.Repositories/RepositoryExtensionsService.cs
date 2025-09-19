using Microsoft.Extensions.DependencyInjection;
using SchoolApplication.Common;

namespace SchoolApplication.Repositories
{
    /// <summary>
    /// Расширение для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RepositoryExtensionsService
    {
        /// <summary>
        /// Регистрация репозиториев
        /// </summary>
        public static void RegisterRepositories(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
