using FluentValidation;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <summary>
    /// Валидация <see cref="StudentCreateModel"/>
    /// </summary>
    public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
    {
        private const int MinLength = 2;
        private const int MaxLength = 255;
        private const int GradeMaxLength = 5;

        public StudentCreateModelValidator()
        {
            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage("Идентификатор родителя обязателен");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .Length(MinLength, MaxLength).WithMessage($"Фамилия должна содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Имя должно содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.Patronymic)
                .NotEmpty().WithMessage("Отчество обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Отчество должно содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.Grade)
                .NotEmpty().WithMessage("Класс обязателен")
                .MaximumLength(GradeMaxLength).WithMessage($"Класс не должен превышать {GradeMaxLength} символов");

            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Пол обязателен");
        }
    }
}