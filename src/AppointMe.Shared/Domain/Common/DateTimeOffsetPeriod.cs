namespace AppointMe.Shared.Domain.Common;

public sealed record DateTimeOffsetPeriod(DateTimeOffset Start, DateTimeOffset End);

public static class DateTimeOffsetPeriodFactory
{
    extension(DateTimeOffsetPeriod)
    {
        public static DateTimeOffsetPeriod Create(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != TimeSpan.Zero || end.Offset != TimeSpan.Zero)
            {
                throw new ValidationException("Start and end must be in UTC.");
            }

            if (end <= start)
            {
                throw new ValidationException("End time must be after start time.");
            }

            return new DateTimeOffsetPeriod(start, end);
        }
    }
}
