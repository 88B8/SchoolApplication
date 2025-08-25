using FluentValidation.TestHelper;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="StudentCreateModelValidator"/>
    /// </summary>
    public class StudentCreateModelValidatorTests
    {
        private readonly StudentCreateModelValidator validator = new();

        [Fact]
        public void ValidatorShouldErrorWhenSurnameIsEmpty()
        {
            // Arrange
            var model = new StudentCreateModel { Surname = "" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooShort()
        {
            // Arrange
            var model = new StudentCreateModel { Surname = "A" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooLong()
        {
            // Arrange
            var model = new StudentCreateModel { Surname = new string('a', 256) };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void ValidatorShouldErrorWhenNameIsEmpty()
        {
            // Arrange
            var model = new StudentCreateModel { Name = "" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ValidatorShouldErrorWhenPatronymicIsEmpty()
        {
            // Arrange
            var model = new StudentCreateModel { Patronymic = "" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        [Fact]
        public void ValidatorShouldErrorWhenGradeIsEmpty()
        {
            // Arrange
            var model = new StudentCreateModel { Grade = "" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Grade);
        }

        [Fact]
        public void ValidatorShouldErrorWhenGradeTooLong()
        {
            // Arrange
            var model = new StudentCreateModel { Grade = "11АБВГД" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Grade);
        }
    }
}
