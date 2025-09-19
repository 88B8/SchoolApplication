using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolApplication.Context
{
    /// <summary>
    /// Фабрика для создания контекста в DesignTime
    /// </summary>
    public class SchoolApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolApplicationContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project DataBase\SchoolApplication.Context\SchoolApplication.Context.csproj
        /// 4) dotnet ef database update --project DataBase\SchoolApplication.Context\SchoolApplication.Context.csproj --connection "Host=localhost;Port=5432;Database=grid;Username=postgres;Password=123"
        /// 5) dotnet ef database update [targetMigrationName] --project SchoolApplication.Context\SchoolApplication.Context.csproj
        /// </remarks>
        public SchoolApplicationContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<SchoolApplicationContext>()
                .UseNpgsql()
                .LogTo(Console.WriteLine)
                .Options;

            return new SchoolApplicationContext(options);
        }
    }
}
