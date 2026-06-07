namespace AppointMe.Shared.Authorization.Permissions.OverrideConflicts;

public sealed class DenyWinsPolicy : IOverrideConflictPolicy
{
    public bool Resolve(IReadOnlyList<PermissionVote> votes) => votes.All(vote => vote.IsGranted);
}
