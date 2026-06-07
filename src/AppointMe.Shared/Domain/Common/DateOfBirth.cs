namespace AppointMe.Shared.Domain.Common;

public sealed record DateOfBirth(DateOnly Value)
{
    public const int MaximumAgeYears = 150;

    public int GetAge(TimeProvider timeProvider)
    {
        var today = timeProvider.Today();
        var age = today.Year - Value.Year;
        if (Value > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}

public static class DateOfBirthFactory
{
    extension(DateOfBirth)
    {
        public static DateOfBirth? CreateOrNull(DateOnly? value, TimeProvider timeProvider)
        {
            return value is null ? null : DateOfBirth.Create(value.Value, timeProvider);
        }

        public static DateOfBirth Create(DateOnly value, TimeProvider timeProvider)
        {
            var today = timeProvider.Today();
            if (value > today)
            {
                throw new ValidationException("Date of birth cannot be in the future.");
            }

            var minDate = today.AddYears(-DateOfBirth.MaximumAgeYears);
            if (value < minDate)
            {
                throw new ValidationException("Date of birth is not valid.");
            }

            return new DateOfBirth(value);
        }
    }
}
