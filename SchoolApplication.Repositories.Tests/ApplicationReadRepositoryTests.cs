using FluentAssertions;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.Contracts;
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
            await Context.AddAsync(application);
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
            await Context.AddAsync(application);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetById(application.Id, CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(application, opt => opt
                .Excluding(x => x.Student));
        }

        /// <summary>
        /// Тест при вызове GetAll без объектов должна вернуться пустая коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
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
            await Context.AddAsync(application);
            await Context.SaveChangesAsync();

            // Act
            var result = await applicationReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}