using FluentValidation;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <summary>
    /// Валидация <see cref="SchoolCreateModel"/>
    /// </summary>
    public class SchoolCreateModelValidator : AbstractValidator<SchoolCreateModel>
    {
        private const int MinLength = 3;
        private const int MaxLength = 255;

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название школы обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Название школы должно содержать от {MinLength} до {MaxLength} символов");

            RuleFor(x => x.DirectorName)
                .NotEmpty().WithMessage("Имя директора обязательно")
                .Length(MinLength, MaxLength).WithMessage($"Имя директора должно содержать от {MinLength} до {MaxLength} символов");
        }
    }
}