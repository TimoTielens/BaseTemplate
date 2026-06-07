namespace AppointMe.Shared.Authorization.Permissions;

public sealed record SystemPermission(string Resource, string Action) : Permission(Resource, Action);
