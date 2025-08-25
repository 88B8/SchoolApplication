using SchoolApplication.Common;

namespace SchoolApplication.Web
{
    /// <inheritdoc cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow()
            => DateTimeOffset.UtcNow;
    }
}
