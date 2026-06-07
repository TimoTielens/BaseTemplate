using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointMe.Organizations.Database.Migrations
{
    /// <inheritdoc />
    public partial class ConvertRolesToStringJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(BuildIntToNameSql("Employees"));
            migrationBuilder.Sql(BuildIntToNameSql("EmployeeInvitations"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(BuildNameToIntSql("Employees"));
            migrationBuilder.Sql(BuildNameToIntSql("EmployeeInvitations"));
        }

        private static string BuildIntToNameSql(string table) => $"""
            UPDATE [organizations].[{table}]
            SET [Roles] = (
                SELECT ISNULL('[' + STRING_AGG('"' +
                    CASE [value]
                        WHEN '0' THEN 'Owner'
                        WHEN '1' THEN 'Manager'
                        WHEN '2' THEN 'Staff'
                        WHEN '3' THEN 'Receptionist'
                        ELSE [value]
                    END + '"', ',') + ']', '[]')
                FROM OPENJSON([Roles])
            )
            WHERE [Roles] IS NOT NULL AND ISJSON([Roles]) = 1;
            """;

        private static string BuildNameToIntSql(string table) => $"""
            UPDATE [organizations].[{table}]
            SET [Roles] = (
                SELECT ISNULL('[' + STRING_AGG(
                    CASE [value]
                        WHEN 'Owner' THEN '0'
                        WHEN 'Manager' THEN '1'
                        WHEN 'Staff' THEN '2'
                        WHEN 'Receptionist' THEN '3'
                        ELSE [value]
                    END, ',') + ']', '[]')
                FROM OPENJSON([Roles])
            )
            WHERE [Roles] IS NOT NULL AND ISJSON([Roles]) = 1;
            """;
    }
}
