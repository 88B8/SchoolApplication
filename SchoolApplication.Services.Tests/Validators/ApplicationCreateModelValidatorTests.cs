using FluentValidation.TestHelper;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Validators.CreateModels;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="ApplicationCreateModelValidator"/>
    /// </summary>
    public class ApplicationCreateModelValidatorTests
    {
        private readonly ApplicationCreateModelValidator validator = new();

        /// <summary>
        /// Тест на ошибку при пустом айди ученика
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenStudentIdIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.StudentId = Guid.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        /// <summary>
        /// Тест на ошибку при пустом айди родителя
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenParentIdIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.ParentId = Guid.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ParentId);
        }

        /// <summary>
        /// Тест на ошибку при пустом айди школы
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSchoolIdIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.SchoolId = Guid.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SchoolId);
        }

        /// <summary>
        /// Тест на ошибку при пустой причине
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenReasonIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.Reason = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Reason);
        }

        /// <summary>
        /// Тест на ошибку при короткой причине
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenReasonTooShort()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.Reason = new string('a', ApplicationValidationRules.ReasonMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Reason);
        }

        /// <summary>
        /// Тест на ошибку при длинной причине
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenReasonTooLong()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.Reason = new string('a', ApplicationValidationRules.ReasonMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Reason);
        }

        /// <summary>
        /// Тест на ошибку если дата окончания раньше даты начала
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenDateFromIsAfterDateUntil()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel(x =>
            {
                x.DateFrom = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
                x.DateUntil = DateOnly.FromDateTime(DateTime.Today);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DateFrom);
            result.ShouldHaveValidationErrorFor(x => x.DateUntil);
        }

        /// <summary>
        /// Тест на отсутствие ошибок при валидной модели
        /// </summary>
        [Fact]
        public void ValidatorShouldPassWhenModelIsValid()
        {
            // Arrange
            var model = TestDataGenerator.ApplicationCreateModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}