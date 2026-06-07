using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Shared.Authorization.Permissions.OverrideConflicts;

public sealed record PermissionVote(Role Role, bool IsGranted);
