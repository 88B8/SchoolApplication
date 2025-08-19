using FluentValidation;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <summary>
    /// Валидация <see cref="ApplicationModel"/>
    /// </summary>
    public class ApplicationModelValidator : AbstractValidator<ApplicationModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationModelValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId обязателен");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Причина заявления обязательна")
                .Length(3, 255).WithMessage("Длина причины не должна быть меньше 3 символов и не должна превышать 255 символов");

            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateUntil)
                .WithMessage("Дата начала отсутствия не может быть позже даты окончания");

            RuleFor(x => x.DateUntil)
                .GreaterThanOrEqualTo(x => x.DateFrom)
                .WithMessage("Дата окончания должна быть не раньше даты начала");
        }
    }
}
