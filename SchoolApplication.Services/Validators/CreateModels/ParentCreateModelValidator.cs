using FluentValidation;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <summary>
    /// Валидация <see cref="ParentCreateModel"/>
    /// </summary>
    public class ParentCreateModelValidator : AbstractValidator<ParentCreateModel>
    {
        private const int MinLength = 2;
        private const int MaxLength = 255;

        /// <summary>
        /// ctor
        /// </summary>
        public ParentCreateModelValidator()
        {
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .Length(MinLength, MaxLength).WithMessage($"Фамилия должна содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Имя должно содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.Patronymic)
                .NotEmpty().WithMessage("Отчество обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Отчество должно содержать от {MinLength} до {MaxLength} символов");
        }
    }
}
