using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Web.Controllers;
using SchoolApplication.Web.Tests.Client;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests.Controllers
{
    /// <summary>
    /// Тесты для <see cref="StudentController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class StudentControllerTests : BaseTestModel, IAsyncLifetime
    {
        /// <summary>
        /// ctor
        /// </summary>
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
            var response = await WebClient.StudentAllAsync();

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
            var student = await Seeder.SeedStudent();

            // Act
            var response = await WebClient.StudentAllAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == student.Id);
        }

        /// <summary>
        /// Тест на возврат правильной сущности при получении сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var expectedResult = new StudentApiModel
            {
                Id = student.Id,
                Surname = student.Surname,
                Name = student.Name,
                Patronymic = student.Patronymic,
                Grade = student.Grade,
                Gender = (GenderApiModel)student.Gender,
            };

            // Act
            var response = await WebClient.StudentGETAsync(student.Id);

            // Assert
            response.Should().BeEquivalentTo(expectedResult);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new StudentCreateRequestApiModel
            {
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Gender = GenderApiModel._0,
                Grade = "9В",
            };

            // Act
            var response = await WebClient.StudentPOSTAsync(model);

            // Assert
            response.Should().BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на исключение при валидации
        /// </summary>
        [Fact]
        public async Task AddShouldThrowValidationException()
        {
            // Arrange
            var model = new StudentCreateRequestApiModel
            {
                Surname = "1",
                Name = "1",
                Patronymic = "1",
                Gender = GenderApiModel._0,
                Grade = "1",
            };

            // Act
            var response = () => WebClient.StudentPOSTAsync(model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Count.Should().Be(4);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var model = new StudentCreateRequestApiModel()
            {
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Gender = GenderApiModel._0,
                Grade = "9Г",
            };

            // Act
            var response = await WebClient.StudentPUTAsync(student.Id, model);

            // Assert
            response.Grade.Should()
                .Be("9Г");
        }

        /// <summary>
        /// Тест на исключение валидации при редактировании
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var model = new StudentCreateRequestApiModel
            {
                Surname = "1",
                Name = "1",
                Patronymic = "1",
                Gender = GenderApiModel._0,
                Grade = "1",
            };

            // Act
            var response = () => WebClient.StudentPUTAsync(student.Id, model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Should().HaveCount(4);
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var student = await Seeder.SeedStudent();

            // Act
            await WebClient.StudentDELETEAsync(student.Id);
            var students = await WebClient.StudentAllAsync();

            // Assert
            students.Should().BeEmpty();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            var students = Context.Set<Student>();
            Context.RemoveRange(students);
            await Context.SaveChangesAsync();
        }

        Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
    }
}
