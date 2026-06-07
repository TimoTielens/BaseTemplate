using AppointMe.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Organizations.Employees.GetTeam;

public sealed class GetTeamRequest : PaginationRequest
{
    [FromQuery(Name = "search")]
    public string? Search { get; init; }
}
