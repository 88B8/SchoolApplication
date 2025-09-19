using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Web.Controllers;
using SchoolApplication.Web.Tests.Client;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests.Controllers
{
    /// <summary>
    /// Тесты для <see cref="ParentController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class ParentControllerTests : BaseTestModel, IAsyncLifetime
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ParentControllerTests(SchoolApplicationApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Должен вернуть пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var response = await WebClient.ParentAllAsync();

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
            var parent = await Seeder.SeedParent();
            await Seeder.SeedParent(x => x.DeletedAt = DateTimeOffset.UtcNow);

            // Act
            var response = await WebClient.ParentAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == parent.Id);
        }

        /// <summary>
        /// Тест на возврат правильной сущности при получении сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var parent = await Seeder.SeedParent();
            var expectedResult = new ParentApiModel
            {
                Id = parent.Id,
                Surname = parent.Surname,
                Name = parent.Name,
                Patronymic = parent.Patronymic,
            };

            // Act
            var response = await WebClient.ParentGETAsync(parent.Id);

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
            var model = new ParentCreateRequestApiModel
            {
                Surname = "Петрова",
                Name = $"test_parent_{Guid.NewGuid()}",
                Patronymic = "Владимировна",
            };

            // Act
            var response = await WebClient.ParentPOSTAsync(model);

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
            var model = new ParentCreateRequestApiModel
            {
                Name = "1",
                Surname = "1",
                Patronymic = "1",
            };

            // Act
            var response = () => WebClient.ParentPOSTAsync(model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Count.Should().Be(3);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var parent = await Seeder.SeedParent();
            var model = new ParentCreateRequestApiModel
            {
                Surname = "Петрова",
                Name = $"test_parent_{Guid.NewGuid()}",
                Patronymic = "Владиславовна",
            };

            // Act
            var response = await WebClient.ParentPUTAsync(parent.Id, model);

            // Assert
            response.Patronymic
                .Should().Be(model.Patronymic);
        }

        /// <summary>
        /// Тест на исключение валидации при редактировании
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var parent = await Seeder.SeedParent();
            var model = new ParentCreateRequestApiModel
            {
                Surname = "1",
                Name = "1",
                Patronymic = "1",
            };

            // Act
            var response = () => WebClient.ParentPUTAsync(parent.Id, model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Should().HaveCount(3);
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var parent = await Seeder.SeedParent();

            // Act
            await WebClient.ParentDELETEAsync(parent.Id);

            // Assert
            var parents = await WebClient.ParentAllAsync();
            parents.Should().BeEmpty();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            var testParents = Context.Set<Parent>()
                .Where(x => x.Name.StartsWith("test_"));
            Context.RemoveRange(testParents);
            await Context.SaveChangesAsync();
        }

        Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
    }
}
