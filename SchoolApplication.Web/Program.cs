using Microsoft.EntityFrameworkCore;
using SchoolApplication.Context;
using SchoolApplication.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SchoolApplicationContext>(options =>
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine));

builder.Services.RegisterDependencies();

// Add services to the container.
var ctrls = builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<SchoolApplicationExceptionFilter>();
});

if (builder.Environment.EnvironmentName == "integration")
{
    ctrls.AddControllersAsServices();
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

public partial class Program { }