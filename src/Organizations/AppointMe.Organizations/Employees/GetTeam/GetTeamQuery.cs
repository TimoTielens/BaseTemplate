using AppointMe.Shared.Pagination;

namespace AppointMe.Organizations.Employees.GetTeam;

public class GetTeamQuery
{
    public required string? Search { get; init; }
    public required PaginationFilter Pagination { get; init; }
}
