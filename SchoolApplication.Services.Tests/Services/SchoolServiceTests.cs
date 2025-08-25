using AutoMapper;
using Moq;
using FluentAssertions;
using SchoolApplication.Common;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Entities;

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
                new ValidateService(),
                new SchoolWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
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
        /// Тест на добавление невалидной <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.SchoolCreateModel(x => x.Name = "a");

            // Act
            Func<Task> result = () => schoolService.Create(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
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

            await Context.AddAsync(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.SchoolModel(x =>
            {
                x.Id = id;
                x.Name = "Школа 2";
            });

            // Act
            await schoolService.Edit(updatedModel, CancellationToken.None);

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
            Func<Task> result = () => schoolService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на редактирование невалидного <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.SchoolModel(x => x.Name = "1");

            // Act
            Func<Task> result = () => schoolService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="School"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.School();
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => schoolService.Delete(model.Id, CancellationToken.None);

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
            Func<Task> result = () => schoolService.Delete(id, CancellationToken.None);

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
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => schoolService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}