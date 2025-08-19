using FluentValidation.TestHelper;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="ApplicationCreateModelValidator"/>
    /// </summary>
    public class ApplicationCreateModelValidatorTests
    {
        private readonly ApplicationCreateModelValidator validator = new();

        /// <summary>
        /// Тест на ошибку при пустом айди
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenStudentIdIsEmpty()
        {
            // Arrange
            var model = new ApplicationCreateModel
            {
                StudentId = Guid.Empty
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        /// <summary>
        /// Тест на ошибку при пустой причине
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenReasonIsEmpty()
        {
            // Arrange
            var model = new ApplicationCreateModel
            {
                Reason = ""
            };

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
            var model = new ApplicationCreateModel
            {
                Reason = "ab"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Reason);
        }

        /// <summary>
        /// Тест на ошибка при длинной причине
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenReasonTooLong()
        {
            // Arrange
            var model = new ApplicationCreateModel
            {
                Reason = new string('a', 256)
            };

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
            var model = new ApplicationCreateModel
            {
                DateFrom = DateTime.Today.AddDays(1),
                DateUntil = DateTime.Today
            };

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
            var model = new ApplicationCreateModel
            {
                StudentId = Guid.NewGuid(),
                Reason = "Valid reason",
                DateFrom = DateTime.Today,
                DateUntil = DateTime.Today.AddDays(1)
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}