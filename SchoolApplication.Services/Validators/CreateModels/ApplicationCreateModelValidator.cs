using FluentValidation;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts.Models.CreateModels;

namespace SchoolApplication.Services.Validators.CreateModels
{
    /// <summary>
    /// Валидация <see cref="ApplicationCreateModel"/>
    /// </summary>
    public class ApplicationCreateModelValidator : AbstractValidator<ApplicationCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApplicationCreateModelValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty()
                .WithMessage("StudentId обязателен");

            RuleFor(x => x.ParentId)
                .NotEmpty()
                .WithMessage("ParentId обязателен");

            RuleFor(x => x.SchoolId)
                .NotEmpty()
                .WithMessage("SchoolId обязателен");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Причина заявления обязательна")
                .Length(ApplicationValidationRules.ReasonMinLength, ApplicationValidationRules.ReasonMaxLength)
                .WithMessage("Длина причины не должна быть меньше 3 символов и не должна превышать 255 символов");

            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateUntil)
                .WithMessage("Дата начала отсутствия не может быть позже даты окончания");

            RuleFor(x => x.DateUntil)
                .GreaterThanOrEqualTo(x => x.DateFrom)
                .WithMessage("Дата окончания должна быть не раньше даты начала");
        }
    }
}
