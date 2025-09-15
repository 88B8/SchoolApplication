namespace SchoolApplication.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class SchoolApplicationValidationException : SchoolApplicationException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public SchoolApplicationValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
