namespace SchoolApplication.Services.Contracts
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

        public SchoolApplicationValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
