using FluentValidation;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services;

/// <inheritdoc cref="IValidateService"
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
            { typeof(ApplicationModel), new ApplicationModelValidator() },
            { typeof(StudentModel), new StudentModelValidator() },
            { typeof(ParentModel), new ParentModelValidator() },
            { typeof(SchoolModel), new SchoolModelValidator() },
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
            throw new SchoolApplicationInvalidOperationException($"Валидатор для типа {typeof(TModel).Name} не найден");
        }

        var validator = validatorObj as IValidator<TModel>;

        if (validator is null)
        {
            throw new InvalidCastException($"Неверный тип валидатора для {typeof(TModel).Name}");
        }

        var result = await validator.ValidateAsync(model, cancellationToken);

        if (!result.IsValid)
        {
            throw new SchoolApplicationValidationException(result.Errors.Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
        }
    }
}