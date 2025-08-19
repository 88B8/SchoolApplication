namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Not Found исключение
    /// </summary>
    public class SchoolApplicationNotFoundException : SchoolApplicationException
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SchoolApplicationNotFoundException(string message) : base(message)
        {
            
        }
    }
}
