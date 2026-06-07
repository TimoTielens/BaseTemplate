using AppointMe.Shared.Pagination;

namespace AppointMe.Organizations.Employees.Database;

public sealed class GetTeamFilter
{
    public required string[] SearchTokens { get; init; }
    public required PaginationFilter Pagination { get; init; }
}
