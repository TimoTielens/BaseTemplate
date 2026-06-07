using AppointMe.Shared.Database;
using AppointMe.Shared.Database.Dapper;
using Dapper;

namespace AppointMe.Booking.Appointments.Database;

public sealed class AppointmentsRepository(IDbConnectionFactory connectionFactory)
{
    private const string AppointmentsSqlTemplate =
        """
        SELECT Appointments.[Id],
               Appointments.[Start],
               Appointments.[End],
               Appointments.[Status],
               Appointments.[ProviderId],
               ServiceProviders.[FirstName] AS [ProviderFirstName],
               ServiceProviders.[LastName]  AS [ProviderLastName],
               Appointments.[AttendeeId],
               Attendees.[FirstName]        AS [AttendeeFirstName],
               Attendees.[LastName]         AS [AttendeeLastName],
               Appointments.[Notes],
               Appointments.[ScheduledAt]
        FROM [booking].[Appointments] Appointments
                 INNER JOIN [booking].[ServiceProviders] ServiceProviders ON ServiceProviders.[Id] = Appointments.[ProviderId]
                 INNER JOIN [booking].[Attendees] Attendees ON Attendees.[Id] = Appointments.[AttendeeId]
        /**where**/
        /**orderby**/
        """;

    private async Task<AppointmentDto?> GetById(AppointmentId appointmentId, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        var builder = new ExtSqlBuilder()
            .Where("Appointments.[CompanyId] = @CompanyId", new { CompanyId = companyId.Value })
            .Where("Appointments.[Id] = @Id", new { Id = appointmentId.Value });

        var template = builder.AddTemplate(AppointmentsSqlTemplate);

        using var connection = await connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandDefinition = new CommandDefinition(template.RawSql, template.Parameters,
            cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<AppointmentDto>(commandDefinition);
    }

    public async Task<AppointmentDto> LoadById(AppointmentId appointmentId, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        return await GetById(appointmentId, companyId, cancellationToken)
               ?? throw new NotFoundException($"Appointment with id='{appointmentId.Value}' was not found");
    }

    public async Task<IReadOnlyList<AppointmentDto>> GetByDateRange(DateTimeOffsetPeriod range,
        CompanyId companyId, CancellationToken cancellationToken)
    {
        var builder = new ExtSqlBuilder()
            .Where("Appointments.[CompanyId] = @CompanyId", new
            {
                CompanyId = companyId.Value
            })
            .Where("Appointments.[Start] < @To", new
            {
                To = range.End
            })
            .Where("Appointments.[End] > @From", new
            {
                From = range.Start
            });

        builder.OrderBy("Appointments.[Start]");

        var template = builder.AddTemplate(AppointmentsSqlTemplate);

        using var connection = await connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandDefinition = new CommandDefinition(template.RawSql, template.Parameters,
            cancellationToken: cancellationToken);
        var results = await connection.QueryAsync<AppointmentDto>(commandDefinition);
        return results.ToList();
    }
}
