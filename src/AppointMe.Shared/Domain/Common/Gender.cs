using System.Text.Json.Serialization;

namespace AppointMe.Shared.Domain.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female,
    Other
}

public static class GenderFactory
{
    extension(Gender)
    {
        public static Gender? ParseOrNull(string? gender)
        {
            return gender is null ? null : Gender.Parse(gender);
        }

        public static Gender Parse(string gender)
        {
            if (Enum.TryParse<Gender>(gender, ignoreCase: true, out var result))
            {
                return result;
            }

            var validValues = string.Join(", ", Enum.GetNames<Gender>());
            throw new ValidationException($"Invalid gender value: '{gender}'. Valid values are: {validValues}.");
        }
    }
}
