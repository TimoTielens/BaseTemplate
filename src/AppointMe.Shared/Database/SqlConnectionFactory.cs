using System.Data;
using Microsoft.Data.SqlClient;

namespace AppointMe.Shared.Database;

public sealed class SqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public async Task<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        var sqlConnection = new SqlConnection(connectionString);
        await sqlConnection.OpenAsync(cancellationToken);
        return sqlConnection;
    }
}
