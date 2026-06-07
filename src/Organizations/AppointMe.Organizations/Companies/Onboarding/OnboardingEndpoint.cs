using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Organizations.Companies.Onboarding;

internal sealed class OnboardingEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/onboarding", Onboarding)
            .WithName(nameof(Onboarding));
    }

    private static async Task<Created<OnboardingResponse>> Onboarding([FromBody] OnboardingRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        var result = await bus.InvokeAsync<OnboardingResponse>(new OnboardingCommand
        {
            Name = request.CompanyName,
            TimeZone = request.TimeZone
        }, cancellationToken);
        return TypedResults.Created($"/companies/{result.Id}", result);
    }
}
