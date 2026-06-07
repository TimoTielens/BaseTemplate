namespace AppointMe.Organizations.Companies.Onboarding;

public sealed class OnboardingCommand
{
    public required string Name { get; init; }
    public required string TimeZone { get; init; }
}
