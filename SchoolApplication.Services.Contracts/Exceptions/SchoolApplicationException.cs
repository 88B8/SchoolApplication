namespace SchoolApplication.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class SchoolApplicationException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SchoolApplicationException"/> с указанием сообщения без параметров
        /// </summary>
        protected SchoolApplicationException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SchoolApplicationException"/> с указанием сообщения об ошибке
        /// </summary>
        protected SchoolApplicationException(string message)
            : base(message) { }
    }
}
