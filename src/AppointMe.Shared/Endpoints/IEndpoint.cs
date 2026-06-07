using Microsoft.AspNetCore.Routing;

namespace AppointMe.Shared.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}
