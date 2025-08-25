using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Тесты для <see cref="StudentController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class StudentControllerTests : BaseTestModel, IAsyncLifetime
    {
        public StudentControllerTests(SchoolApplicationApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Должен вернуть пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var response = await webClient.StudentAllAsync();

            // Assert
            response.Should().BeEmpty();
        }

        /// <summary>
        /// Должен вернуть не пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var student = TestDataGenerator.Student();
            await context.AddAsync(student);
            await context.SaveChangesAsync();

            // Act
            var response = await webClient.StudentAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == student.Id);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new StudentRequestApiModel
            {
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Gender = GenderApiModel._0,
                Grade = "9В",
            };

            // Act
            var response = await webClient.StudentPOSTAsync(model);

            // Assert
            response.Should().BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var studentId = await seeder.SeedStudent();
            var model = new StudentRequestApiModel
            {
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Gender = GenderApiModel._0,
                Grade = "9Г",
            };

            // Act
            var response = await webClient.StudentPUTAsync(studentId, model);

            // Assert
            response.Grade.Should()
                .Be("9Г");
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var studentId = await seeder.SeedStudent();

            // Act
            await webClient.StudentDELETEAsync(studentId);
            var students = await webClient.StudentAllAsync();

            // Assert
            students.Should().BeEmpty();
        }

        public async Task InitializeAsync()
        {
            var students = context.Set<Student>();
            context.RemoveRange(students);
            await context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
