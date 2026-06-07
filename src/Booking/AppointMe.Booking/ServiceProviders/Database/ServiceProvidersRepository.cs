using AppointMe.Shared.Database;
using Dapper;

namespace AppointMe.Booking.ServiceProviders.Database;

public sealed class ServiceProvidersRepository(IDbConnectionFactory connectionFactory)
{
    private const string Sql =
        """
        SELECT
            [Id],
            [FirstName],
            [LastName]
        FROM [booking].[ServiceProviders]
        WHERE
            [CompanyId] = @CompanyId
            AND [IsDeleted] = 0
        ORDER BY [FirstName], [LastName]
        """;

    public async Task<IReadOnlyList<ServiceProviderDto>> GetAll(CompanyId companyId,
        CancellationToken cancellationToken)
    {
        using var connection = await connectionFactory.OpenConnectionAsync(cancellationToken);
        var results = await connection.QueryAsync<ServiceProviderDto>(
            new CommandDefinition(Sql, new { CompanyId = companyId.Value },
                cancellationToken: cancellationToken));
        return results.ToList();
    }
}
