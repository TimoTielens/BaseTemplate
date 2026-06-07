using System.Net.Mail;

namespace AppointMe.Shared.Domain.Common;

public sealed record Email(string Value)
{
    public const int MaxLength = 200;
}

public static class EmailFactory
{
    extension(Email)
    {
        public static Email? CreateOrNull(string? email)
        {
            return email is null ? null : Email.Create(email);
        }

        public static Email Create(string email)
        {
            email = email.Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Email address is required.");
            }

            if (email.Length > Email.MaxLength)
            {
                throw new ValidationException($"Email must be {Email.MaxLength} chars at max.");
            }

            if (!MailAddress.TryCreate(email, out var mailAddress))
            {
                throw new ValidationException($"'{email}' is not a valid email address.");
            }

            return new Email(mailAddress.Address);
        }
    }
}
