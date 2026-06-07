namespace AppointMe.Organizations.Companies.Onboarding;

public sealed class OnboardingRequest
{
    public required string CompanyName { get; init; }
    public required string TimeZone { get; init; }
}
