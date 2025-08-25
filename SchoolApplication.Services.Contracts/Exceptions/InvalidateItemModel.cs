namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель инвалидации запросов
    /// </summary>
    public class InvalidateItemModel
    {
        /// <summary>
        /// Имя инвалидного поля
        /// </summary>
        public string Field { get; } = string.Empty;

        /// <summary>
        /// Сообщение инвалидации
        /// </summary>
        public string Message { get; } = string.Empty;

        /// <summary>
        /// ctor
        /// </summary>
        public InvalidateItemModel(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Создает <see cref="InvalidateItemModel"/>
        /// </summary>
        public static InvalidateItemModel New(string field, string message)
            => new InvalidateItemModel(field, message);
    }
}
