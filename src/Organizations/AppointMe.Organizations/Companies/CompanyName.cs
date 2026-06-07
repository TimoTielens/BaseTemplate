namespace AppointMe.Organizations.Companies;

public record CompanyName(string Value)
{
    public const int MaxLength = 250;
}

public static class CompanyNameFactory
{
    extension(CompanyName)
    {
        public static CompanyName Create(string value) => value switch
        {
            _ when string.IsNullOrWhiteSpace(value) =>
                throw new ValidationException("Company name is required"),

            { Length: > CompanyName.MaxLength } =>
                throw new ValidationException($"Company name must be {CompanyName.MaxLength} characters at max"),

            _ => new CompanyName(value)
        };
    }
}
