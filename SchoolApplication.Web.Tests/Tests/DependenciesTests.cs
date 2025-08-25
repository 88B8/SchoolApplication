using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;

namespace SchoolApplication.Web.Tests
{
    public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        /// <summary>
        /// ctor
        /// </summary>
        public DependenciesTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestAppConfiguration();
                builder.UseEnvironment("integration");
            });
        }

        /// <summary>
        /// Проверка резолва зависимостей
        /// </summary>
        [Theory]
        [MemberData(nameof(WebControllerCore))]
        public void ControllerCoreShouldBeResolver(Type controller)
        {
            // Arrange
            using var scoped = factory.Services.CreateScope();

            // Art
            var instance = scoped.ServiceProvider.GetRequiredService(controller);

            // Assert
            instance.Should().NotBeNull();
        }

        public static TheoryData<Type> WebControllerCore => GetControllers<ApplicationController>();

        private static TheoryData<Type> GetControllers<TController>()
        {
            var result = new TheoryData<Type>();

            var assembly = Assembly.GetAssembly(typeof(TController));
            if (assembly == null)
                return result;

            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeof(ControllerBase).IsAssignableFrom(typeInfo) && !typeInfo.IsAbstract)
                {
                    result.Add(typeInfo.AsType());
                }
            }

            return result;
        }
    }
}