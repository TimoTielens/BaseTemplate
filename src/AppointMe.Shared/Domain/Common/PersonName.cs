using System.Globalization;

namespace AppointMe.Shared.Domain.Common;

public sealed record PersonName(string FirstName, string? LastName)
{
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;
}

public static class PersonNameFactory
{
    extension(PersonName personName)
    {
        public string FullName => $"{personName.FirstName} {personName.LastName}".TrimEnd();

        public string Initials
        {
            get
            {
                if (string.IsNullOrWhiteSpace(personName.FirstName))
                {
                    return string.Empty;
                }

                var first = char.ToUpperInvariant(personName.FirstName[0]);
                if (string.IsNullOrWhiteSpace(personName.LastName))
                {
                    return first.ToString();
                }

                return $"{first}{char.ToUpperInvariant(personName.LastName[0])}";
            }
        }
    }

    extension(PersonName)
    {
        public static PersonName Create(string firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ValidationException($"{nameof(firstName)} must not be empty.");
            }

            if (firstName.Length > PersonName.FirstNameMaxLength)
            {
                throw new ValidationException(
                    $"{nameof(firstName)} must be {PersonName.FirstNameMaxLength} chars at max.");
            }

            if (lastName?.Length > PersonName.LastNameMaxLength)
            {
                throw new ValidationException(
                    $"{nameof(lastName)} must be {PersonName.LastNameMaxLength} chars at max.");
            }

            return new PersonName(firstName.Capitalize(), lastName?.Capitalize());
        }

        public static PersonName? CreateOrNull(string? firstName, string? lastName)
        {
            return firstName is null ? null : PersonName.Create(firstName, lastName);
        }

        public static PersonName FromFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ValidationException("Full name must not be empty.");
            }

            var parts = fullName
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return PersonName.Create(parts[0], parts.Length == 1 ? null : string.Join(' ', parts.Skip(1)));
        }
    }
}

file static class StringExtensions
{
    extension(string value)
    {
        public string Capitalize()
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.Trim().ToLowerInvariant());
        }
    }
}
