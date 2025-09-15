using FluentValidation;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Exceptions;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Services;
using SchoolApplication.Services.Validators.CreateModels;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IValidateService"/>
    public class ValidateService : IValidateService, IServiceAnchor
    {
        private readonly Dictionary<Type, IValidator> validators;

        /// <summary>
        /// ctor
        /// </summary>
        public ValidateService()
        {
            validators = new Dictionary<Type, IValidator>
            {
                { typeof(ApplicationCreateModel), new ApplicationCreateModelValidator() },
                { typeof(StudentCreateModel), new StudentCreateModelValidator() },
                { typeof(ParentCreateModel), new ParentCreateModelValidator() },
                { typeof(SchoolCreateModel), new SchoolCreateModelValidator() },
            };
        }

        /// <summary>
        /// Валидирует модели
        /// </summary>
        public async Task Validate<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            if (!validators.TryGetValue(typeof(TModel), out var validatorObj))
            {
                throw new InvalidOperationException($"Валидатор для типа {typeof(TModel).Name} не найден");
            }

            if (validatorObj is not IValidator<TModel> validator)
            {
                throw new InvalidCastException($"Неверный тип валидатора для {typeof(TModel).Name}");
            }

            var result = await validator.ValidateAsync(model, cancellationToken);

            if (!result.IsValid)
            {
                throw new SchoolApplicationValidationException(result.Errors.Select(x =>
                    InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}