namespace AppointMe.Organizations.Employees;

public sealed record TeamMemberDto
{
    public required Guid Id { get; init; }
    public string? FullName { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Initials { get; init; }
    public required string Email { get; init; }
    public required Role[] Roles { get; init; }
    public required TeamMemberType Type { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public Guid? UserId { get; init; }
    public bool IsCurrentUser { get; init; }
    public bool IsPrimaryOwner { get; init; }
    public Role[] LockedRoles { get; init; } = [];
}

public enum TeamMemberType
{
    Employee,
    Invitation
}
