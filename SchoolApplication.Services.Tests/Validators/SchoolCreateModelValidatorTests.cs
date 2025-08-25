using FluentValidation.TestHelper;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="SchoolCreateModelValidator"/>
    /// </summary>
    public class SchoolCreateModelValidatorTests
    {
        private readonly SchoolCreateModelValidator validator = new();

        /// <summary>
        /// Тест на отсутствие ошибок при валидной модели
        /// </summary>
        [Fact]
        public void ValidatorShouldPassWhenModelIsValid()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = "Школа №1",
                DirectorName = "Петров П.П.",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Тест на ошибку при пустом названии школы
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameIsEmpty()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                DirectorName = "Петров П.П.",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при пустом ФИО директора
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenDirectorNameIsEmpty()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = "Школа №1",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DirectorName);
        }

        /// <summary>
        /// Тест на ошибку при коротком названии школы
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooShort()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = "ab",
                DirectorName = "Петров П.П.",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при длинном названии школы
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooLong()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = new string('a', 256),
                DirectorName = "Петров П.П.",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при коротком ФИО директора
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenDirectorNameTooShort()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = "Школа №1",
                DirectorName = "ab",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DirectorName);
        }

        /// <summary>
        /// Тест на ошибку при длинном ФИО директора
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenDirectorNameTooLong()
        {
            // Arrange
            var model = new SchoolCreateModel
            {
                Name = "Школа №1",
                DirectorName = new string('a', 256),
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DirectorName);
        }
    }
}