using FluentValidation;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts.Models.CreateModels;

namespace SchoolApplication.Services.Validators.CreateModels
{
    /// <summary>
    /// Валидация <see cref="StudentCreateModel"/>
    /// </summary>
    public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public StudentCreateModelValidator()
        {
            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Фамилия обязательна")
                .Length(StudentValidationRules.SurnameMinLength, StudentValidationRules.SurnameMaxLength)
                .WithMessage($"Фамилия должна содержать от {StudentValidationRules.SurnameMinLength} до {StudentValidationRules.SurnameMaxLength} символов");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя обязательно")
                .Length(StudentValidationRules.NameMinLength, StudentValidationRules.NameMaxLength)
                .WithMessage($"Имя должно содержать от {StudentValidationRules.NameMinLength} до {StudentValidationRules.NameMaxLength} символов");

            RuleFor(x => x.Patronymic)
                .NotEmpty()
                .WithMessage("Отчество обязательно")
                .Length(StudentValidationRules.PatronymicMinLength, StudentValidationRules.PatronymicMaxLength)
                .WithMessage($"Отчество должно содержать от {StudentValidationRules.PatronymicMinLength} до {StudentValidationRules.PatronymicMaxLength} символов");

            RuleFor(x => x.Grade)
                .NotEmpty()
                .WithMessage("Класс обязателен")
                .Length(StudentValidationRules.GradeMinLength, StudentValidationRules.GradeMaxLength)
                .WithMessage($"Класс должен содержать от {StudentValidationRules.GradeMinLength} до {StudentValidationRules.GradeMaxLength} символов");

            RuleFor(x => x.Gender)
                .NotNull()
                .WithMessage("Пол обязателен")
                .IsInEnum()
                .WithMessage("Недопустимое значение пола");
        }
    }
}