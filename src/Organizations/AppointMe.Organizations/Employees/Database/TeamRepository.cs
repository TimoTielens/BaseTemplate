using AppointMe.Shared.Database;
using AppointMe.Shared.Database.Dapper;
using AppointMe.Shared.Pagination;
using Dapper;

namespace AppointMe.Organizations.Employees.Database;

public sealed class TeamRepository(IDbConnectionFactory connectionFactory)
{
    private const string TeamSqlTemplate =
        """
        SELECT TeamMembers.[Id],
               TeamMembers.[FirstName],
               TeamMembers.[LastName],
               TeamMembers.[Email],
               TeamMembers.[Type],
               TeamMembers.[UserId],
               TeamMembers.[RegistrationDate],
               TeamMembers.[Roles]
        /**totalcount**/
        FROM (SELECT Employees.[Id],
                     Employees.[FirstName],
                     Employees.[LastName],
                     Employees.[Email],
                     'Employee' AS [Type],
                     Employees.[RegistrationDate],
                     Employees.[UserId],
                     Employees.[Roles],
                     Employees.[SearchKey]
              FROM [organizations].[Employees] Employees
              WHERE Employees.[CompanyId] = @CompanyId
                AND Employees.[IsDeleted] = 0

              UNION ALL

              SELECT Invitations.[Id],
                     NULL                       AS [FirstName],
                     NULL                       AS [LastName],
                     Invitations.[Email],
                     'Invitation'               AS [Type],
                     Invitations.[InvitedAt]    AS [RegistrationDate],
                     NULL                       AS [UserId],
                     Invitations.[Roles],
                     LOWER(Invitations.[Email]) AS [SearchKey]
              FROM [organizations].[EmployeeInvitations] Invitations
              WHERE Invitations.[CompanyId] = @CompanyId
                AND Invitations.[Status] <> 'Accepted') TeamMembers
        /**where**/
        ORDER BY TeamMembers.[RegistrationDate] DESC, TeamMembers.[Id] DESC
        /**pagination**/
        """;

    public async Task<PagedResult<TeamMemberDto>> GetTeam(GetTeamFilter filter, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId.Value);

        var builder = new ExtSqlBuilder()
            .AddPagination(filter.Pagination)
            .WhereSearch("TeamMembers.[SearchKey]", filter.SearchTokens);

        var template = builder.AddTemplate(TeamSqlTemplate, parameters);

        using var connection = await connectionFactory.OpenConnectionAsync(cancellationToken);
        var (records, totalCount) = await connection
            .QueryWithPaginationAsync<TeamMemberRecord>(template.RawSql, template.Parameters);

        return records.Select(record => record.ToDto()).ToPagedResult(filter.Pagination, totalCount);
    }
}
