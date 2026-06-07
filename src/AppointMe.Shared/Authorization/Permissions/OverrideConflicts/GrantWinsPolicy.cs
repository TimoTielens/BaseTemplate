namespace AppointMe.Shared.Authorization.Permissions.OverrideConflicts;

public sealed class GrantWinsPolicy : IOverrideConflictPolicy
{
    public bool Resolve(IReadOnlyList<PermissionVote> votes) => votes.Any(vote => vote.IsGranted);
}
