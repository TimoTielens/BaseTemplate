namespace AppointMe.Shared.Authorization.Permissions;

public record Permission(string Resource, string Action)
{
    public string Code { get; } = $"{Resource}:{Action}";
}
