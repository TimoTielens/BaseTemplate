namespace AppointMe.Identity.Contracts.Identity;

public sealed record IdentityProviderUserId(string Value)
{
    public const int MaxLength = 255;
}
