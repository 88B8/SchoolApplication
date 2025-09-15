using AutoMapper;
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
    public class SchoolServiceTests : SchoolApplicationContextInMemory
    {
        private readonly ISchoolService schoolService;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolServiceTests()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ServiceProfile>();
            });

            var mapper = config.CreateMapper();
            schoolService = new SchoolService(new SchoolReadRepository(Context),
                new ApplicationReadRepository(Context),
                new SchoolWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                new ApplicationWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение <see cref="IReadOnlyCollection{School}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var school1 = TestDataGenerator.School();
            var school2 = TestDataGenerator.School();
            await Context.AddRangeAsync(school1, school2);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
        }

        /// <summary>
        /// Тест на получение пустой коллекции <see cref="IReadOnlyCollection{School}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await schoolService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Тест на получение правильной сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var school = TestDataGenerator.School();
            Context.Add(school);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolService.GetById(school.Id, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(school, opt => opt
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
            var result = () => schoolService.GetById(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на добавление <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.SchoolCreateModel();

            // Act
            var result = await schoolService.Create(model, CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на редактирование <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var id = Guid.NewGuid();
            var application = TestDataGenerator.School(x =>
            {
                x.Id = id;
                x.Name = "Школа 1";
            });

            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.SchoolModel(x =>
            {
                x.Id = id;
                x.Name = "Школа 2";
            });

            // Act
            await schoolService.Edit(id, updatedModel, CancellationToken.None);

            // Assert
            var updated = await Context.Set<School>().FindAsync(id);
            updated.Should().NotBeNull();
            updated.Name.Should().Be("Школа 2");
        }

        /// <summary>
        /// Тест на редактирование несуществующего <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var model = TestDataGenerator.SchoolModel();

            // Act
            var result = () => schoolService.Edit(model.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.School();
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => schoolService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().NotThrowAsync();
            var entity = await Context.Set<School>().FindAsync(model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление несуществующего <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task DeletingNotFoundShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = () => schoolService.Delete(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление удаленного <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedShouldThrowException()
        {
            // Arrange
            var model = TestDataGenerator.School(x => x.DeletedAt = DateTime.UtcNow);
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => schoolService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}