using SchoolApplication.Common.Contracts;

namespace SchoolApplication.Common.Infrastructure
{
    /// <inheritdoc cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow()
            => DateTime.UtcNow;
    }
}
