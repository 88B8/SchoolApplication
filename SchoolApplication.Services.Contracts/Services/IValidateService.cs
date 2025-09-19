namespace SchoolApplication.Services.Contracts.Services
{
    /// <summary>
    /// Сервис валидации
    /// </summary>
    public interface IValidateService
    {
        Task Validate<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}