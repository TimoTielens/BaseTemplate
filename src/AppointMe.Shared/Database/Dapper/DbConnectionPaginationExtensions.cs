using System.Data;
using Dapper;

namespace AppointMe.Shared.Database.Dapper;

public static class DbConnectionPaginationExtensions
{
    extension(IDbConnection connection)
    {
        public async Task<(IEnumerable<T>, int)> QueryWithPaginationAsync<T>(string sql, object? param = null)
        {
            var totalCount = 0;
            var result = await connection.QueryAsync<T, int, T>(
                sql, (record, count) =>
                {
                    totalCount = count;
                    return record;
                }, param, splitOn: ExtSqlBuilder.TotalCountFieldName);
            return (result, totalCount);
        }
    }
}
