namespace AppointMe.Shared.Authorization.Permissions.OverrideConflicts;

public interface IOverrideConflictPolicy
{
    bool Resolve(IReadOnlyList<PermissionVote> votes);
}
