using AutoMapper;
using Moq;
using FluentAssertions;
using SchoolApplication.Common;
using SchoolApplication.Context.Tests;
using SchoolApplication.Repositories;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Tests.Services
{
    /// <summary>
    /// Тесты для <see cref="ApplicationService"/>
    /// </summary>
    public class ApplicationServiceTests : SchoolApplicationContextInMemory
    {
        private readonly IApplicationService applicationService;

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
            applicationService = new ApplicationService(new ApplicationReadRepository(Context),
                mapper,
                new ValidateService(),
                UnitOfWork,
                new ApplicationWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
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
            await Context.AddRangeAsync(application1, application2);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await applicationService.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.HaveCount(2);
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
        /// Тест на добавление <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel();

            // Act
            var result = await applicationService.Create(model, CancellationToken.None);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(model);
        }

        /// <summary>
        /// Тест на добавление невалидной <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x => x.Reason = "a");

            // Act
            Func<Task> result = () => applicationService.Create(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на редактирование <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var id = Guid.NewGuid();
            var application = TestDataGenerator.Application(x =>
            {
                x.Id = id;
                x.Reason = "по семейным обстоятельствам";
            });

            await Context.AddAsync(application);
            await UnitOfWork.SaveChangesAsync();

            var updatedModel = TestDataGenerator.ApplicationModel(x =>
            {
                x.Id = id;
                x.Reason = "посещение специалиста";
            });

            // Act
            await applicationService.Edit(updatedModel, CancellationToken.None);

            // Assert
            var updated = await Context.Set<Application>().FindAsync(id);
            updated.Should().NotBeNull();
            updated.Reason.Should().Be("посещение специалиста");
        }

        /// <summary>
        /// Тест на редактирование несуществующего <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationModel();

            // Act
            Func<Task> result = () => applicationService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }

        /// <summary>
        /// Тест на редактирование невалидного <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task EditShouldThrowValidationException()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationModel(x => x.Reason = "1");

            // Act
            Func<Task> result = () => applicationService.Edit(model, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationValidationException>();
        }

        /// <summary>
        /// Тест на удаление <see cref="Application"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = TestDataGenerator.Application();
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => applicationService.Delete(model.Id, CancellationToken.None);

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
            Func<Task> result = () => applicationService.Delete(id, CancellationToken.None);

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
            await Context.AddAsync(model);
            await UnitOfWork.SaveChangesAsync();

            // Act
            Func<Task> result = () => applicationService.Delete(model.Id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<SchoolApplicationNotFoundException>();
        }
    }
}