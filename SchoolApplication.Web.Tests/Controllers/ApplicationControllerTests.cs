using FluentAssertions;
using SchoolApplication.Entities;
using SchoolApplication.Web.Controllers;
using SchoolApplication.Web.Tests.Client;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests.Controllers
{
    /// <summary>
    /// Тесты для <see cref="ApplicationController"/>
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class ApplicationControllerTests : BaseTestModel, IAsyncLifetime
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationControllerTests(SchoolApplicationApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Должен вернуть пустую коллекцию 
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var response = await WebClient.ApplicationAllAsync();

            // Assert
            response.Should().BeEmpty();
        }

        /// <summary>
        /// Должен вернуть правильную коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var application = await Seeder.SeedApplication();
            await Seeder.SeedApplication(x => x.DeletedAt = DateTimeOffset.UtcNow);

            // Act
            var response = await WebClient.ApplicationAllAsync();

            // Assert
            response.Should()
                .ContainSingle(x => x.Id == application.Id);
        }

        /// <summary>
        /// Тест на получение правильной сущности при получении сущности по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var application = await Seeder.SeedApplication();
            var expectedResult = new ApplicationApiModel
            {
                Id = application.Id,
                Reason = application.Reason,
                Parent = new ParentApiModel
                {
                    Id = application.Parent.Id,
                    Surname = application.Parent.Surname,
                    Name = application.Parent.Name,
                    Patronymic = application.Parent.Patronymic,
                },
                Student = new StudentApiModel
                {
                    Id = application.Student.Id,
                    Gender = GenderApiModel._0,
                    Grade = application.Student.Grade,
                    Name = application.Student.Name,
                    Surname = application.Student.Surname,
                    Patronymic = application.Student.Patronymic,
                },
                School = new SchoolApiModel
                {
                    Id = application.School.Id,
                    DirectorName = application.School.DirectorName,
                    Name = application.School.Name,
                },
                DateFrom = application.DateFrom,
                DateUntil = application.DateUntil,
            };

            // Act
            var response = await WebClient.ApplicationGETAsync(application.Id);

            // Assert
            response.Should()
                .BeEquivalentTo(expectedResult);
        }

        /// <summary>
        /// Тест на добавление
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var parent = await Seeder.SeedParent();
            var school = await Seeder.SeedSchool();

            var model = new ApplicationCreateRequestApiModel
            {
                StudentId = student.Id,
                ParentId = parent.Id,
                SchoolId = school.Id,
                Reason = "по семейным обстоятельствам",
                DateFrom = DateOnly.FromDateTime(new DateTime(2025, 07, 13)),
                DateUntil = DateOnly.FromDateTime(new DateTime(2025, 07, 14)),
            };

            // Act
            var response = await WebClient.ApplicationPOSTAsync(model);

            // Assert
            response.Should()
                .BeEquivalentTo(model, opt => opt
                    .Excluding(x => x.StudentId)
                    .Excluding(x => x.ParentId)
                    .Excluding(x => x.SchoolId));
        }

        /// <summary>
        /// Тест на исключение валидации при редактировании
        /// </summary>
        [Fact]
        public async Task AddShouldThrowValidationException()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var parent = await Seeder.SeedParent();
            var school = await Seeder.SeedSchool();

            var model = new ApplicationCreateRequestApiModel
            {
                StudentId = student.Id,
                ParentId = parent.Id,
                SchoolId = school.Id,
                Reason = "1",
                DateFrom = DateOnly.FromDateTime(new DateTime(2025, 07, 15)),
                DateUntil = DateOnly.FromDateTime(new DateTime(2025, 07, 14)),
            };

            // Act
            var response = () => WebClient.ApplicationPOSTAsync(model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Count.Should().Be(3);
        }

        /// <summary>
        /// Тест на редактирование
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var student = await Seeder.SeedStudent();
            var application = await Seeder.SeedApplication();
            var parent = await Seeder.SeedParent();
            var school = await Seeder.SeedSchool();

            var model = new ApplicationCreateRequestApiModel()
            {
                StudentId = student.Id,
                ParentId = parent.Id,
                SchoolId = school.Id,
                Reason = "по семейным обстоятельствам",
                DateFrom = DateOnly.FromDateTime(new DateTime(2025, 07, 13)),
                DateUntil = DateOnly.FromDateTime(new DateTime(2025, 07, 14)),
            };

            // Act
            var response = await WebClient.ApplicationPUTAsync(application.Id, model);

            // Assert
            response.Reason.Should()
                .Be("по семейным обстоятельствам");
        }

        /// <summary>
        /// Тест на исключение валидации при редактировании
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var application = await Seeder.SeedApplication();
            var student = await Seeder.SeedStudent();
            var parent = await Seeder.SeedParent();
            var school = await Seeder.SeedSchool();

            var model = new ApplicationCreateRequestApiModel
            {
                StudentId = student.Id,
                ParentId = parent.Id,
                SchoolId = school.Id,
                Reason = "1",
                DateFrom = DateOnly.FromDateTime(new DateTime(2025, 07, 15)),
                DateUntil = DateOnly.FromDateTime(new DateTime(2025, 07, 14)),
            };

            // Act
            var response = () => WebClient.ApplicationPUTAsync(application.Id, model);

            // Assert
            var ex = await response.Should().ThrowAsync<ApiException<ApiValidationExceptionDetail>>();

            ex.Which.Result.Errors.Should().HaveCount(3);
        }

        /// <summary>
        /// Тест на удаление
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var application = await Seeder.SeedApplication();

            // Act
            await WebClient.ApplicationDELETEAsync(application.Id);
            var applications = await WebClient.ApplicationAllAsync();

            // Assert
            applications.Should().BeEmpty();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            var applications = Context.Set<Application>();
            Context.RemoveRange(applications);
            await Context.SaveChangesAsync();
        }

        Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
    }
}
