using AppointMe.Shared.Pagination;
using Dapper;

namespace AppointMe.Shared.Database.Dapper;

public sealed class ExtSqlBuilder : SqlBuilder
{
    public const string TotalCountFieldName = "TotalCount";

    public ExtSqlBuilder AddPagination(PaginationFilter pagination)
    {
        AddClause("pagination",
            "OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY",
            new { pagination.Offset, pagination.Limit },
            " "
        );

        AddClause("totalcount",
            $",COUNT(*) OVER () AS [{TotalCountFieldName}]",
            null,
            " "
        );

        return this;
    }
}

public static class SqlBuilderExtensions
{
    extension(SqlBuilder sqlBuilder)
    {
        public SqlBuilder WhereSearch(string column, string[] searchTokens)
        {
            for (var i = 0; i < searchTokens.Length; i++)
            {
                var parameters = new DynamicParameters();
                parameters.Add($"SearchToken{i}", searchTokens[i]);
                sqlBuilder.Where($"{column} LIKE '%' + @SearchToken{i} + '%'", parameters);
            }

            return sqlBuilder;
        }
    }
}
