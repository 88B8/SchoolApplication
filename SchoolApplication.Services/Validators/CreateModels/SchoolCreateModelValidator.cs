using FluentValidation;
using SchoolApplication.Entities.Contracts.ValidationRules;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services.Validators.CreateModels
{
    /// <summary>
    /// Валидация <see cref="SchoolCreateModel"/>
    /// </summary>
    public class SchoolCreateModelValidator : AbstractValidator<SchoolCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SchoolCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Название школы обязательно")
                .Length(SchoolValidationRules.NameMinLength, SchoolValidationRules.NameMaxLength)
                .WithMessage($"Название школы должно содержать от {SchoolValidationRules.NameMinLength} до {SchoolValidationRules.NameMaxLength} символов");

            RuleFor(x => x.DirectorName)
                .NotEmpty()
                .WithMessage("Имя директора обязательно")
                .Length(SchoolValidationRules.DirectorNameMinLength, SchoolValidationRules.DirectorNameMaxLength)
                .WithMessage($"Имя директора должно содержать от {SchoolValidationRules.DirectorNameMinLength} до {SchoolValidationRules.DirectorNameMaxLength} символов");
        }
    }
}