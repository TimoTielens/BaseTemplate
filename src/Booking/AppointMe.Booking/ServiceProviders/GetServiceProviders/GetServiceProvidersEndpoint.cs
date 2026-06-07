
namespace AppointMe.Booking.ServiceProviders.GetServiceProviders;

internal sealed class GetServiceProvidersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/booking/service-providers", GetServiceProviders)
            .WithName(nameof(GetServiceProviders));
    }

    private static async Task<IReadOnlyList<ServiceProviderDto>> GetServiceProviders(
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<IReadOnlyList<ServiceProviderDto>>(
            new GetServiceProvidersQuery(), cancellationToken);
    }
}
