using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Тесты для <see cref="SchoolController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class SchoolControllerTests : BaseTestModel, IAsyncLifetime
    {
        public SchoolControllerTests(SchoolApplicationApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Должен вернуть пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var response = await webClient.SchoolAllAsync();

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
            var school = TestDataGenerator.School();
            await context.AddAsync(school);
            await context.SaveChangesAsync();

            // Act
            var response = await webClient.SchoolAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == school.Id);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new SchoolRequestApiModel
            {
                Name = "Школа №3",
                DirectorName = "Васильева С.Б.",
            };

            // Act
            var response = await webClient.SchoolPOSTAsync(model);

            // Assert
            response.Should().BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var schoolId = await seeder.SeedSchool();
            var model = new SchoolRequestApiModel
            {
                Name = "Школа №3",
                DirectorName = "Петрова С.Б.",
            };

            // Act
            var response = await webClient.SchoolPUTAsync(schoolId, model);

            // Assert
            response.DirectorName.Should()
                .Be("Петрова С.Б.");
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var schoolId = await seeder.SeedSchool();

            // Act
            await webClient.SchoolDELETEAsync(schoolId);
            var schools = await webClient.SchoolAllAsync();

            // Assert
            schools.Should().BeEmpty();
        }

        public async Task InitializeAsync()
        {
            var schools = context.Set<School>();
            context.RemoveRange(schools);
            await context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
