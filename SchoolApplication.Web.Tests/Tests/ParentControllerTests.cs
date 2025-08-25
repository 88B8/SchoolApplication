using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Entities;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Тесты для <see cref="ParentController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class ParentControllerTests : BaseTestModel, IAsyncLifetime
    {
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
            var response = await webClient.ParentAllAsync();

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
            var parent = TestDataGenerator.Parent();
            await context.AddAsync(parent);
            await context.SaveChangesAsync();

            // Act
            var response = await webClient.ParentAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == parent.Id);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new ParentRequestApiModel
            {
                Surname = "Петрова",
                Name = "Наталья",
                Patronymic = "Владимировна",
            };

            // Act
            var response = await webClient.ParentPOSTAsync(model);

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
            var parentId = await seeder.SeedParent();
            var model = new ParentRequestApiModel
            {
                Surname = "Петрова",
                Name = "Наталья",
                Patronymic = "Владиславовна",
            };

            // Act
            var response = await webClient.ParentPUTAsync(parentId, model);

            // Assert
            response.Patronymic.Should()
                .Be("Владиславовна");
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var parentId = await seeder.SeedParent();

            // Act
            await webClient.ParentDELETEAsync(parentId);
            var parents = await webClient.ParentAllAsync();

            // Assert
            parents.Should().BeEmpty();
        }

        public async Task InitializeAsync()
        {
            var parents = context.Set<Parent>();
            context.RemoveRange(parents);
            await context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
