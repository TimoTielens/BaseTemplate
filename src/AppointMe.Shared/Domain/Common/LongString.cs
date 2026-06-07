namespace AppointMe.Shared.Domain.Common;

public sealed record LongString(string Value)
{
    public const int MaxLength = 4000;
}

public static class LongStringFactory
{
    extension(LongString)
    {
        public static LongString? CreateOrNull(string? value)
        {
            return value is null ? null : LongString.Create(value);
        }

        public static LongString Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Value cannot be empty.");
            }

            if (value.Length > LongString.MaxLength)
            {
                throw new ValidationException($"Value must be {LongString.MaxLength} characters at max.");
            }

            return new LongString(value);
        }
    }
}
