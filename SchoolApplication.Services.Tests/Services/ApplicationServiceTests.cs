using AutoMapper;
using FluentAssertions;
using Moq;
using SchoolApplication.Common.Contracts;
using SchoolApplication.Context.Tests;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.ReadRepositories;
using SchoolApplication.Repositories.WriteRepositories;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Contracts.Models.Enums;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Services.Infrastructure;
using SchoolApplication.Services.Services;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Services.Tests.Services
{
    /// <summary>
    /// Тесты для <see cref="ApplicationService"/>
    /// </summary>
    public class ApplicationServiceTests : SchoolApplicationContextInMemory
    {
        private readonly IApplicationService applicationService;
        private readonly TestDataSeeder seeder;

        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationServiceTests()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ServiceProfile>();
            });

            var mapper = config.CreateMapper();
            seeder = new TestDataSeeder(Context);

            applicationService = new ApplicationService(new ApplicationReadRepository(Context),
                new SchoolReadRepository(Context),
                new StudentReadRepository(Context),
                new ParentReadRepository(Context),
                new ApplicationWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение <see cref="IReadOnlyCollection{Application}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var application1 = TestDataGenerator.Application();
            var application2 = TestDataGenerator.Application();
            var application3 = TestDataGenerator.Application(x => x.DeletedAt = DateTimeOffset.UtcNow );
            await Context.AddRangeAsync(application1, application2, application3);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .HaveCount(2);
        }

        /// <summary>
        /// Тест на получение пустой коллекции <see cref="IReadOnlyCollection{Application}"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await applicationService.GetAll(CancellationToken.None);

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
            var application = await seeder.SeedApplication();
            var expectedResult = new ApplicationModel
            {
                Id = application.Id,
                Reason = application.Reason,
                DateFrom = application.DateFrom,
                DateUntil = application.DateUntil,
                Parent = new ParentModel
                {
                    Id = application.Parent.Id,
                    Surname = application.Parent.Surname,
                    Name = application.Parent.Name,
                    Patronymic = application.Parent.Patronymic,
                },
                Student = new StudentModel
                {
                    Id = application.Student.Id,
                    Gender = GenderModel.Male,
                    Grade = application.Student.Grade,
                    Surname = application.Student.Surname,
                    Name = application.Student.Name,
                    Patronymic = application.Student.Patronymic,
                },
                School = new SchoolModel
                {
                    Id = application.School.Id,
                    DirectorName = application.School.DirectorName,
                    Name = application.School.Name,
                },
            };

            // Act
            var result = await applicationService.GetById(application.Id, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(expectedResult);
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
            var result = () => applicationService.GetById(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на добавление <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var parent = await seeder.SeedParent();
            var student = await seeder.SeedStudent();
            var school = await seeder.SeedSchool();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.ParentId = parent.Id;
                x.StudentId = student.Id;
                x.SchoolId = school.Id;
            });

            // Act
            var result = await applicationService.Create(model, CancellationToken.None);

            // Assert
            result.Should()
                .BeEquivalentTo(model, opt => opt
                    .Excluding(x => x.ParentId)
                    .Excluding(x => x.StudentId)
                    .Excluding(x => x.SchoolId));

            result.Parent.Id.Should().Be(model.ParentId);
            result.School.Id.Should().Be(model.SchoolId);
            result.Student.Id.Should().Be(model.StudentId);
        }

        /// <summary>
        /// Тест на исключение при несуществующей связи с родителем
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowByParent()
        {
            // Arrange
            var student = await seeder.SeedStudent();
            var school = await seeder.SeedSchool();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.StudentId = student.Id;
                x.SchoolId = school.Id;
            });

            // Act
            var result = () => applicationService.Create(model, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.ParentId}*");
        }

        /// <summary>
        /// Тест на исключение при несуществующей связи с учеником
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowByStudent()
        {
            // Arrange
            var school = await seeder.SeedSchool();
            var parent = await seeder.SeedParent();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.SchoolId = school.Id;
                x.ParentId = parent.Id;
            });

            // Act
            var result = () => applicationService.Create(model, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.StudentId}*");
        }

        /// <summary>
        /// Тест на исключение при несуществующей связи со школой
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowBySchool()
        {
            // Arrange
            var student = await seeder.SeedStudent();
            var parent = await seeder.SeedParent();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.StudentId = student.Id;
                x.ParentId = parent.Id;
            });

            // Act
            var result = () => applicationService.Create(model, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.SchoolId}*");
        }

        /// <summary>
        /// Тест на редактирование <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var parent = await seeder.SeedParent();
            var student = await seeder.SeedStudent();
            var school = await seeder.SeedSchool();
            var application = TestDataGenerator.Application(x =>
            {
                x.ParentId = parent.Id;
                x.StudentId = student.Id;
                x.SchoolId = school.Id;
                x.Reason = "по семейным обстоятельствам";
            });

            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.Reason = "посещение специалиста";
                x.StudentId = application.StudentId;
                x.ParentId = application.ParentId;
                x.SchoolId = application.SchoolId;
                x.DateFrom = application.DateFrom;
                x.DateUntil = application.DateUntil;
            });

            // Act
            await applicationService.Edit(application.Id, updatedModel, CancellationToken.None);

            // Assert
            var updated = await Context.Set<Application>().FindAsync(application.Id);

            updated.Should().BeEquivalentTo(updatedModel);
        }

        /// <summary>
        /// Тест на редактирование несуществующего <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = TestDataGenerator.ApplicationCreateModel();

            // Act
            var result = () => applicationService.Edit(id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на исключение при редактировании сущности с несуществующей связью с родителем
        /// </summary>
        [Fact]
        public async Task EditShouldThrowByParent()
        {
            // Arrange
            var application = TestDataGenerator.Application();
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var student = await seeder.SeedStudent();
            var school = await seeder.SeedSchool();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.StudentId = student.Id;
                x.SchoolId = school.Id;
            });

            // Act
            var result = () => applicationService.Edit(application.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.ParentId}*");
        }

        /// <summary>
        /// Тест на исключение при редактировании сущности с несуществующей связью с учеником
        /// </summary>
        [Fact]
        public async Task EditShouldThrowByStudent()
        {
            // Arrange
            var application = TestDataGenerator.Application();
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var parent = await seeder.SeedParent();
            var school = await seeder.SeedSchool();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.ParentId = parent.Id;
                x.SchoolId = school.Id;
            });

            // Act
            var result = () => applicationService.Edit(application.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.StudentId}*");
        }

        /// <summary>
        /// Тест на исключение при редактировании сущности с несуществующей связью со школой
        /// </summary>
        [Fact]
        public async Task EditShouldThrowBySchool()
        {
            // Arrange
            var application = TestDataGenerator.Application();
            Context.Add(application);
            await UnitOfWork.SaveChangesAsync();

            var parent = await seeder.SeedParent();
            var student = await seeder.SeedStudent();
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.ParentId = parent.Id;
                x.StudentId = student.Id;
            });

            // Act
            var result = () => applicationService.Edit(application.Id, model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>().WithMessage($"*{model.SchoolId}*");
        }

        /// <summary>
        /// Тест на удаление <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Application();
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => applicationService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().NotThrowAsync();
            var entity = await Context.Set<Application>().FindAsync(model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление несуществующего <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task DeletingNotFoundShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = () => applicationService.Delete(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на удаление удаленного <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedShouldThrowException()
        {
            // Arrange
            var model = TestDataGenerator.Application(x => x.DeletedAt = DateTime.UtcNow);
            Context.Add(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = () => applicationService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}