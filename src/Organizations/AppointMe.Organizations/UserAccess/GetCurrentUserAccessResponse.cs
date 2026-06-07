namespace AppointMe.Organizations.UserAccess;

public sealed record GetCurrentUserAccessResponse
{
    public required Guid CompanyId { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
    public required IReadOnlyList<string> Permissions { get; init; }
}
