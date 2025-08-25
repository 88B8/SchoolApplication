using Microsoft.Extensions.DependencyInjection;
using SchoolApplication.Context.Contracts;

namespace SchoolApplication.Context
{
    /// <summary>
    /// Расширение для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ContextExtensionsService
    {
        /// <summary>
        /// Регистрирует контекст
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterContext(this IServiceCollection service)
        {
            service.AddScoped<IReader>(x => x.GetRequiredService<SchoolApplicationContext>());
            service.AddScoped<IWriter>(x => x.GetRequiredService<SchoolApplicationContext>());
            service.AddScoped<IUnitOfWork>(x => x.GetRequiredService<SchoolApplicationContext>());
        }
    }
}
