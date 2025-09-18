using FluentAssertions;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories.ReadRepositories;
using SchoolApplication.Repositories.Contracts.ReadRepositories;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Repositories.Tests
{
    /// <summary>
    /// Тесты на <see cref="ParentReadRepository"/>
    /// </summary>
    public class ParentReadRepositoryTests : SchoolApplicationContextInMemory
    {
        private readonly IParentReadRepository parentReadRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentReadRepositoryTests()
        {
            parentReadRepository = new ParentReadRepository(Context);
        }

        /// <summary>
        /// Тест на получение по айди должно вернуть null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var parent = TestDataGenerator.Parent();
            Context.Add(parent);
            await UnitOfWork.SaveChangesAsync();
            
            // Act
            var result = await parentReadRepository.GetById(id, CancellationToken.None);

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
            var parent = TestDataGenerator.Parent(x => x.DeletedAt = DateTimeOffset.UtcNow);
            Context.Add(parent);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await parentReadRepository.GetById(parent.Id, CancellationToken.None);

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
            var parent = TestDataGenerator.Parent();
            Context.Add(parent);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await parentReadRepository.GetById(parent.Id, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(parent);
        }

        /// <summary>
        /// Тест при вызове GetAll без объектов должна вернуться пустая коллекция
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Arrange
            var parent = TestDataGenerator.Parent(x => x.DeletedAt = DateTimeOffset.UtcNow);
            Context.Add(parent);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await parentReadRepository.GetAll(CancellationToken.None);

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
            var parent = TestDataGenerator.Parent();
            var deletedParent = TestDataGenerator.Parent(x => x.DeletedAt = DateTimeOffset.UtcNow);

            Context.AddRange(parent, deletedParent);
            await Context.SaveChangesAsync();

            // Act
            var result = await parentReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .ContainSingle()
                .Which.Should().BeEquivalentTo(parent);
        }
    }
}