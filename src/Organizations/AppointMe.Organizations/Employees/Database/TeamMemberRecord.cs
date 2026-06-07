using System.Text.Json;

namespace AppointMe.Organizations.Employees.Database;

internal sealed class TeamMemberRecord
{
    public required Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Email { get; init; }
    public required string Type { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public Guid? UserId { get; init; }
    public required string Roles { get; init; }
}

internal static class TeamMemberRecordExtensions
{
    extension(TeamMemberRecord record)
    {
        internal TeamMemberDto ToDto() => new()
        {
            Id = record.Id,
            FirstName = record.FirstName,
            LastName = record.LastName,
            Email = record.Email,
            Type = Enum.Parse<TeamMemberType>(record.Type),
            RegistrationDate = record.RegistrationDate,
            Roles = JsonSerializer.Deserialize<string[]>(record.Roles)?.Select(Role.Create).ToArray() ?? [],
            UserId = record.UserId
        };
    }
}
