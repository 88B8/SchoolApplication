using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Validators.CreateModels
{
    /// <summary>
    /// Валидация <see cref="ParentCreateModel"/>
    /// </summary>
    public class ParentCreateModelValidator : AbstractValidator<ParentCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ParentCreateModelValidator()
        {
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .Length(ParentValidationRules.SurnameMinLength, ParentValidationRules.SurnameMaxLength)
                .WithMessage($"Фамилия должна содержать от {ParentValidationRules.SurnameMinLength} до {ParentValidationRules.SurnameMaxLength} символов");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .Length(ParentValidationRules.NameMinLength, ParentValidationRules.NameMaxLength)
                .WithMessage($"Имя должно содержать от {ParentValidationRules.NameMinLength} до {ParentValidationRules.NameMaxLength} символов");

            RuleFor(x => x.Patronymic)
                .NotEmpty().WithMessage("Отчество обязательно")
                .Length(ParentValidationRules.PatronymicMinLength, ParentValidationRules.PatronymicMaxLength)
                .WithMessage($"Отчество должно содержать от {ParentValidationRules.PatronymicMinLength} до {ParentValidationRules.PatronymicMaxLength} символов");
        }
    }
}
