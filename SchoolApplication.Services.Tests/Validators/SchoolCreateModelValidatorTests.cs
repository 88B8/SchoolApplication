using FluentValidation.TestHelper;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Validators.CreateModels;
using SchoolApplication.Tests.Extensions;

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
            var model = TestDataGenerator.SchoolCreateModel();

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
            var model = TestDataGenerator.SchoolCreateModel(x =>
            {
                x.Name = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при коротком названии школы
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.SchoolCreateModel(x =>
            {
                x.Name = new string('a', SchoolValidationRules.NameMinLength - 1);
            });

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
            var model = TestDataGenerator.SchoolCreateModel(x =>
            {
                x.Name = new string('a', SchoolValidationRules.NameMaxLength + 1);
            });

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
        /// Тест на ошибку при коротком ФИО директора
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenDirectorNameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.SchoolCreateModel(x =>
            {
                x.DirectorName = new string('a', SchoolValidationRules.DirectorNameMinLength - 1);
            });

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
            var model = TestDataGenerator.SchoolCreateModel(x =>
            {
                x.DirectorName = new string('a', SchoolValidationRules.DirectorNameMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DirectorName);
        }
    }
}