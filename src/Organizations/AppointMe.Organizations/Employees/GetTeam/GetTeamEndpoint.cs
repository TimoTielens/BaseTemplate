using AppointMe.Shared.Pagination;

namespace AppointMe.Organizations.Employees.GetTeam;

internal sealed class GetTeamEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/team", GetTeam).WithName(nameof(GetTeam));
    }

    private static async Task<GetTeamResponse> GetTeam([AsParameters] GetTeamRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetTeamResponse>(new GetTeamQuery
        {
            Search = request.Search,
            Pagination = request.ToPaginationFilter()
        }, cancellationToken);
    }
}
