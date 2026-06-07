namespace AppointMe.Shared.Domain.Common;

public static class TimeZoneInfoFactory
{
    extension(TimeZoneInfo)
    {
        public static TimeZoneInfo Create(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Time zone is required.");
            }

            if (!TimeZoneInfo.TryFindSystemTimeZoneById(id, out var timeZone))
            {
                throw new ValidationException($"Invalid time zone: {id}");
            }

            return timeZone;
        }
    }
}
