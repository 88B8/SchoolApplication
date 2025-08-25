using FluentAssertions;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.Contracts;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Repositories.Tests
{
    /// <summary>
    /// Тесты на <see cref="StudentReadRepository"/>
    /// </summary>
    public class StudentReadRepositoryTests : SchoolApplicationContextInMemory
    {
        private readonly IStudentReadRepository schoolReadRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentReadRepositoryTests()
        {
            schoolReadRepository = new StudentReadRepository(Context);
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
            var student = TestDataGenerator.Student(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.AddAsync(student);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetById(student.Id, CancellationToken.None);

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
            var student = TestDataGenerator.Student();
            await Context.AddAsync(student);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetById(student.Id, CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(student, opt => opt);
        }

        /// <summary>
        /// Тест при вызове GetAll без объектов должна вернуться пустая коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
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
            var student = TestDataGenerator.Student();
            await Context.AddAsync(student);
            await Context.SaveChangesAsync();

            // Act
            var result = await schoolReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}