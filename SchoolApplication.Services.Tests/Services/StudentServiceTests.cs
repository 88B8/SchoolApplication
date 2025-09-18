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
    public class StudentServiceTests : SchoolApplicationContextInMemory
    {
        private readonly IStudentService studentService;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentServiceTests()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ServiceProfile>();
            });

            var mapper = config.CreateMapper();
            studentService = new StudentService(new StudentReadRepository(Context),
                new ApplicationReadRepository(Context),
                new StudentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                new ApplicationWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение <see cref="IReadOnlyCollection{Student}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var student1 = TestDataGenerator.Student();
            var student2 = TestDataGenerator.Student();
            await Context.AddRangeAsync(student1, student2);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await studentService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
        }

        /// <summary>
        /// Тест на получение пустой коллекции <see cref="IReadOnlyCollection{Student}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await studentService.GetAll(CancellationToken.None);

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
            var student = TestDataGenerator.Student();
            Context.Add(student);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await studentService.GetById(student.Id, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(student, opt => opt
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
            var result = () => studentService.GetById(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на добавление <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel();

            // Act
            var result = await studentService.Create(model, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на редактирование <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var id = Guid.NewGuid();
            var application = TestDataGenerator.Student(x =>
            {
                x.Id = id;
                x.Name = "Иван";
            });

            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.StudentModel(x =>
            {
                x.Id = id;
                x.Name = "Петр";
            });

            // Act
            await studentService.Edit(id, updatedModel, CancellationToken.None);

            // Assert
            var updated = await Context.Set<Student>().FindAsync(id);
            updated.Should().NotBeNull();
            updated.Name.Should().Be("Петр");
        }

        /// <summary>
        /// Тест на редактирование несуществующего <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var model = TestDataGenerator.StudentModel();

            // Act
            var result = () => studentService.Edit(model.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Student();
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => studentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().NotThrowAsync();
            var entity = await Context.Set<Student>().FindAsync(model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление несуществующего <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task DeletingNotFoundShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = () => studentService.Delete(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление удаленного <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedShouldThrowException()
        {
            // Arrange
            var model = TestDataGenerator.Student(x => x.DeletedAt = DateTime.UtcNow);
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => studentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}