using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context;
using SchoolApplication.Web.Extensions;
using SchoolApplication.Web.Infrastructure;

namespace SchoolApplication.Web
{
    /// <summary>
    /// Точка входа в приложение
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<SchoolApplicationContext>(options =>
                options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine));

            builder.Services.RegisterDependencies();

            // Add services to the container.
            var controllers = builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<SchoolApplicationExceptionFilter>();
            });

            if (builder.Environment.EnvironmentName == EnvironmentNames.Integration)
            {
                controllers.AddControllersAsServices();
            }

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var baseDirectory = AppContext.BaseDirectory;
                c.IncludeXmlComments(Path.Combine(baseDirectory, "SchoolApplication.Web.xml"));
                c.IncludeXmlComments(Path.Combine(baseDirectory, "SchoolApplication.Entities.xml"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}