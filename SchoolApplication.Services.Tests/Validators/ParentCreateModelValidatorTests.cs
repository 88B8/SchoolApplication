using FluentValidation.TestHelper;
using SchoolApplication.Services.Contracts;

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Name = "Иван",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "Иван",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "И",
                Name = "Иван",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = new string('a', 256),
                Name = "Иван",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "И",
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = new string('a', 256),
                Patronymic = "Иванович",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "И",
            };

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
            var model = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = new string('a', 256),
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }
    }
}