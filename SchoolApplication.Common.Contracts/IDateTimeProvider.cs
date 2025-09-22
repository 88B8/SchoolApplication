namespace SchoolApplication.Common.Contracts
{
    /// <summary>
    /// Поставщик даты и времени
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Время сейчас
        /// </summary>
        DateTimeOffset UtcNow();
    }
}
