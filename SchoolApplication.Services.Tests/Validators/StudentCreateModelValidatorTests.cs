using FluentValidation.TestHelper;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Validators.CreateModels;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="StudentCreateModelValidator"/>
    /// </summary>
    public class StudentCreateModelValidatorTests
    {
        private readonly StudentCreateModelValidator validator = new();

        /// <summary>
        /// Тест на ошибку при пустой фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Surname = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку при слишком короткой фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Surname = new string('a', StudentValidationRules.SurnameMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку при слишком длинной фамилии
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenSurnameTooLong()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Surname = new string('a', StudentValidationRules.SurnameMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        /// <summary>
        /// Тест на ошибку если имя пустое
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Name = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при слишком коротком имени
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooShort()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Name = new string('a', StudentValidationRules.NameMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при слишком длинном имени
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenNameTooLong()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Name = new string('a', StudentValidationRules.NameMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на ошибку при слишком коротком отчестве
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicTooShort()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Patronymic = new string('a', StudentValidationRules.PatronymicMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест на ошибку при слишком длинном отчестве
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicTooLong()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Patronymic = new string('a', StudentValidationRules.PatronymicMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест на ошибку если отчество пустое
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenPatronymicIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Patronymic = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест на ошибку если название класса пустое
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenGradeIsEmpty()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Grade = string.Empty;
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Grade);
        }

        /// <summary>
        /// Тест на ошибку если название класса слишком короткое
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenGradeIsTooShort()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Grade = new string('1', StudentValidationRules.GradeMinLength - 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Grade);
        }

        /// <summary>
        /// Тест на ошибку если название класса слишком длинное
        /// </summary>
        [Fact]
        public void ValidatorShouldErrorWhenGradeTooLong()
        {
            // Arrange
            var model = TestDataGenerator.StudentCreateModel(x =>
            {
                x.Grade = new string('1', StudentValidationRules.GradeMaxLength + 1);
            });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Grade);
        }
    }
}
