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
                new ValidateService(),
                new StudentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
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
                .NotBeNull()
                .And.BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на добавление невалидной <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x => x.Name = "a");

            // Act
            Func<Task> result = () => studentService.Create(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
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

            await Context.AddAsync(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.StudentModel(x =>
            {
                x.Id = id;
                x.Name = "Петр";
            });

            // Act
            await studentService.Edit(updatedModel, CancellationToken.None);

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
            Func<Task> result = () => studentService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на редактирование невалидного <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.StudentModel(x => x.Name = "1");

            // Act
            Func<Task> result = () => studentService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="Student"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Student();
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => studentService.Delete(model.Id, CancellationToken.None);

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
            Func<Task> result = () => studentService.Delete(id, CancellationToken.None);

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
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => studentService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}