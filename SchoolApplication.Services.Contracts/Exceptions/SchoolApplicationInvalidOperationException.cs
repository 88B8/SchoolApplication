namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель InvalidOperation исключения
    /// </summary>
    public class SchoolApplicationInvalidOperationException : SchoolApplicationException
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SchoolApplicationInvalidOperationException(string message) : base(message)
        {

        }
    }
}
