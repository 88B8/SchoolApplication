using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Moq;
using FluentAssertions;
using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.ReadRepositories;
using SchoolApplication.Repositories.WriteRepositories;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Services;
using SchoolApplication.Services.Infrastructure;
using SchoolApplication.Services.Contracts.Services;

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
        /// Тест на получение сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var parent = TestDataGenerator.Parent();
            Context.Add(parent);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await parentService.GetById(parent.Id, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(parent, opt => opt
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        }

        /// <summary>
        /// Тест на исключение при ненайденной сущности
        /// </summary>
        [Fact]
        public async Task GetByIdShouldThrowNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = () => parentService.GetById(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
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
                .BeEquivalentTo(model);
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

            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.ParentModel(x =>
            {
                x.Id = id;
                x.Name = "Петр";
            });

            // Act
            await parentService.Edit(id, updatedModel, CancellationToken.None);

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
            var result = () => parentService.Edit(model.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="Parent"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Parent();
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => parentService.Delete(model.Id, CancellationToken.None);

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
            var result = () => parentService.Delete(id, CancellationToken.None);

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
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => parentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}