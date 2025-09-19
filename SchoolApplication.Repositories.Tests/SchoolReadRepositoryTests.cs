using FluentAssertions;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Repositories.ReadRepositories;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Repositories.Tests
{
    /// <summary>
    /// Тесты на <see cref="SchoolReadRepository"/>
    /// </summary>
    public class SchoolReadRepositoryTests : SchoolApplicationContextInMemory
    {
        private readonly ISchoolReadRepository schoolReadRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolReadRepositoryTests()
        {
            schoolReadRepository = new SchoolReadRepository(Context);
        }

        /// <summary>
        /// Тест на получение по айди должно вернуть null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var school = TestDataGenerator.School();
            Context.Add(school);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetById(id, CancellationToken.None);

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
            var school = TestDataGenerator.School(x => x.DeletedAt = DateTimeOffset.UtcNow);
            Context.Add(school);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetById(school.Id, CancellationToken.None);

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
            var school = TestDataGenerator.School();
            Context.Add(school);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetById(school.Id, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(school);
        }

        /// <summary>
        /// Тест при вызове GetAll без объектов должна вернуться пустая коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Arrange
            var school = TestDataGenerator.School(x => x.DeletedAt = DateTimeOffset.UtcNow);
            Context.Add(school);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetAll(CancellationToken.None);

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
            var school = TestDataGenerator.School();
            var deletedSchool = TestDataGenerator.School(x => x.DeletedAt = DateTimeOffset.UtcNow);

            Context.AddRange(school, deletedSchool);
            await Context.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(school);
        }
    }
}