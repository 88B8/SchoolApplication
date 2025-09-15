using AutoMapper.Configuration.Annotations;
using FluentAssertions;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.Contracts.Models;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Repositories.ReadRepositories;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Repositories.Tests
{
    /// <summary>
    /// Тесты на <see cref="ApplicationReadRepository"/>
    /// </summary>
    public class ApplicationReadRepositoryTests : SchoolApplicationContextInMemory
    {
        private readonly IApplicationReadRepository applicationReadRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationReadRepositoryTests()
        {
            applicationReadRepository = new ApplicationReadRepository(Context);
        }

        /// <summary>
        /// Тест на получение по айди должно вернуть null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var application = TestDataGenerator.Application();
            
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetById(id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Тест на получение по айди должно вернуть null если объект удален
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNullByDelete()
        {
            // Arrange
            var application = TestDataGenerator.Application(x => x.DeletedAt = DateTimeOffset.UtcNow);
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetById(application.Id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Тест на получение по айди должно вернуть значение
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var application = TestDataGenerator.Application();
            var applicationDbModel = new ApplicationDbModel
            {
                Id = application.Id,
                DateFrom = application.DateFrom,
                DateUntil = application.DateUntil,
                Parent = application.Parent,
                Reason = application.Reason,
                School = application.School,
                Student = application.Student,
            };
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetById(application.Id, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(applicationDbModel);
        }

        /// <summary>
        /// Тест при вызове GetAll без объектов должна вернуться пустая коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Arrange
            var application = TestDataGenerator.Application(x => x.DeletedAt = DateTimeOffset.UtcNow);

            // Act
            var result = await applicationReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Тест при вызове GetAll с объектами должна вернуться правильная коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var application = TestDataGenerator.Application();
            var deletedApplication = TestDataGenerator.Application(x => x.DeletedAt = DateTime.Now);
            var applicationDbModel = new ApplicationDbModel
            {
                Id = application.Id,
                DateFrom = application.DateFrom,
                DateUntil = application.DateUntil,
                Parent = application.Parent,
                Reason = application.Reason,
                School = application.School,
                Student = application.Student,
            };

            Context.AddRange(application, deletedApplication);
            await Context.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .ContainSingle()
                .Which.Should()
                .BeEquivalentTo(applicationDbModel);
        }
    }
}