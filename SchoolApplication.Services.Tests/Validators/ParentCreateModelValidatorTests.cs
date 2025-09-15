using FluentValidation.TestHelper;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Validators.CreateModels;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="ParentCreateModelValidator"/>
    /// </summary>
    public class ParentCreateModelValidatorTests
    {
        private readonly ParentCreateModelValidator validator = new();

        /// <summary>
        /// Тест на отсутствие ошибок при валидной модели
        /// </summary>
        [Fact]
        public void ValidatorShouldPassWhenModelIsValid()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Тест на ошибку при пустой фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Surname = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку при пустом имени
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Name = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при пустом отчестве
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Patronymic = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест на ошибку при короткой фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Surname = new string('a', ParentValidationRules.SurnameMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку при длинной фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooLong()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Surname = new string('a', ParentValidationRules.SurnameMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку при коротком имени
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Name = new string('a', ParentValidationRules.NameMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при длинном имени
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooLong()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Name = new string('a', ParentValidationRules.NameMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при коротком отчестве
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicTooShort()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Patronymic = new string('a', ParentValidationRules.PatronymicMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест на ошибку при длинном отчестве
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicTooLong()
        {
            // Arrange
            var model = TestDataGenerator.ParentCreateModel(x =>
            {
                x.Patronymic = new string('a', ParentValidationRules.PatronymicMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }
    }
}