using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Web.Controllers;
using SchoolApplication.Web.Tests.Client;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests.Controllers
{
    /// <summary>
    /// Тесты для <see cref="SchoolController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class SchoolControllerTests : BaseTestModel, IAsyncLifetime
    {
        /// <summary>
        /// ctor
        /// </summary>
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
            var response = await WebClient.SchoolAllAsync();

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
            var school = await Seeder.SeedSchool();

            // Act
            var response = await WebClient.SchoolAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == school.Id);
        }

        /// <summary>
        /// Тест на возврат правильной сущности при получении сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var school = await Seeder.SeedSchool();
            var expectedResult = new SchoolApiModel
            {
                Id = school.Id,
                Name = school.Name,
                DirectorName = school.DirectorName,
            };

            // Act
            var response = await WebClient.SchoolGETAsync(school.Id);

            // Assert
            response.Should().BeEquivalentTo(expectedResult);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new SchoolCreateRequestApiModel
            {
                Name = "Школа №3",
                DirectorName = "Васильева С.Б.",
            };

            // Act
            var response = await WebClient.SchoolPOSTAsync(model);

            // Assert
            response.Should().BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на исключение при валидации
        /// </summary>
        [Fact]
        public async Task AddShouldThrowValidationException()
        {
            // Arrange
            var model = new SchoolCreateRequestApiModel
            {
                Name = "1",
                DirectorName = "1",
            };

            // Act
            var response = () => WebClient.SchoolPOSTAsync(model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Count.Should().Be(2);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var school = await Seeder.SeedSchool();
            var model = new SchoolCreateRequestApiModel
            {
                Name = "Школа №3",
                DirectorName = "Петрова С.Б.",
            };

            // Act
            var response = await WebClient.SchoolPUTAsync(school.Id, model);

            // Assert
            response.DirectorName.Should()
                .Be("Петрова С.Б.");
        }

        /// <summary>
        /// Тест на исключение валидации при редактировании
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var school = await Seeder.SeedSchool();
            var model = new SchoolCreateRequestApiModel()
            {
                Name = "1",
                DirectorName = "1",
            };

            // Act
            var response = () => WebClient.SchoolPUTAsync(school.Id, model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Should().HaveCount(2);
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var school = await Seeder.SeedSchool();

            // Act
            await WebClient.SchoolDELETEAsync(school.Id);
            var schools = await WebClient.SchoolAllAsync();

            // Assert
            schools.Should().BeEmpty();
        }

        public async Task InitializeAsync()
        {
            var schools = Context.Set<School>();
            Context.RemoveRange(schools);
            await Context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
