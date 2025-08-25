using AutoMapper;
using Moq;
using FluentAssertions;
using SchoolApplication.Common;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Tests.Services
{
    /// <summary>
    /// Тесты для <see cref="SchoolService"/>
    /// </summary>
    public class ParentServiceTests : SchoolApplicationContextInMemory
    {
        private readonly IParentService parentService;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentServiceTests()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ServiceProfile>();
            });

            var mapper = config.CreateMapper();
            parentService = new ParentService(new ParentReadRepository(Context),
                new ValidateService(),
                new ParentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение <see cref="IReadOnlyCollection{Parent}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var parent1 = TestDataGenerator.Parent();
            var parent2 = TestDataGenerator.Parent();
            await Context.AddRangeAsync(parent1, parent2);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await parentService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
        }

        /// <summary>
        /// Тест на получение пустой коллекции <see cref="IReadOnlyCollection{Parent}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await parentService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Тест на добавление <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel();

            // Act
            var result = await parentService.Create(model, CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на добавление невалидной <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x => x.Name = "a");

            // Act
            Func<Task> result = () => parentService.Create(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на редактирование <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var id = Guid.NewGuid();
            var application = TestDataGenerator.Parent(x =>
            {
                x.Id = id;
                x.Name = "Иван";
            });

            await Context.AddAsync(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.ParentModel(x =>
            {
                x.Id = id;
                x.Name = "Петр";
            });

            // Act
            await parentService.Edit(updatedModel, CancellationToken.None);

            // Assert
            var updated = await Context.Set<Parent>().FindAsync(id);
            updated.Should().NotBeNull();
            updated.Name.Should().Be("Петр");
        }

        /// <summary>
        /// Тест на редактирование несуществующего <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var model = TestDataGenerator.ParentModel();

            // Act
            Func<Task> result = () => parentService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на редактирование невалидного <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.ParentModel(x => x.Name = "1");

            // Act
            Func<Task> result = () => parentService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Parent();
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => parentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().NotThrowAsync();
            var entity = await Context.Set<Parent>().FindAsync(model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление несуществующего <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task DeletingNotFoundShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => parentService.Delete(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление удаленного <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedShouldThrowException()
        {
            // Arrange
            var model = TestDataGenerator.Parent(x => x.DeletedAt = DateTime.UtcNow);
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => parentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}