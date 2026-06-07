using AppointMe.Shared.Pagination;

namespace AppointMe.Organizations.Employees.GetTeam;

public class GetTeamResponse
{
    public required PagedResult<TeamMemberDto> Members { get; init; }
}
