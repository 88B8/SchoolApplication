using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Тесты для <see cref="ApplicationController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class ApplicationControllerTests : BaseTestModel, IAsyncLifetime
    {
        public ApplicationControllerTests(SchoolApplicationApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Должен вернуть пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var response = await webClient.ApplicationAllAsync();

            // Assert
            response.Should().BeEmpty();
        }

        /// <summary>
        /// Должен вернуть не пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var application = TestDataGenerator.Application(x => x.Reason = "123");
            await context.AddAsync(application);
            await context.SaveChangesAsync();

            // Act
            var response = await webClient.ApplicationAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == application.Id);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var studentId = await seeder.SeedStudent();
            var model = new ApplicationRequestApiModel
            {
                StudentId = studentId,
                Reason = "по семейным обстоятельствам",
                DateFrom = new DateTime(2025, 07, 13),
                DateUntil = new DateTime(2025, 07, 14),
            };

            // Act
            var response = await webClient.ApplicationPOSTAsync(model);

            // Assert
            response.Should()
                .BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var studentId = await seeder.SeedStudent();
            var applicationId = await seeder.SeedApplication();
            var model = new ApplicationRequestApiModel
            {
                StudentId = studentId,
                Reason = "по семейным обстоятельствам",
                DateFrom = new DateTime(2025, 07, 13),
                DateUntil = new DateTime(2025, 07, 14),
            };

            // Act
            var response = await webClient.ApplicationPUTAsync(applicationId, model);

            // Assert
            response.Reason.Should()
                .Be("по семейным обстоятельствам");
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var applicationId = await seeder.SeedApplication();

            // Act
            await webClient.ApplicationDELETEAsync(applicationId);
            var applications = await webClient.ApplicationAllAsync();

            // Assert
            applications.Should().BeEmpty();
        }

        public async Task InitializeAsync()
        {
            var applications = context.Set<Application>();
            context.RemoveRange(applications);
            await context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
