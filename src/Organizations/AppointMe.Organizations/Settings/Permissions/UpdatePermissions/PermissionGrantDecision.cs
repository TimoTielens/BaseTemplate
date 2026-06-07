namespace AppointMe.Organizations.Settings.Permissions.UpdatePermissions;

public enum PermissionGrantAction
{
    NoOp,
    Add,
    Update,
    Remove,
}

public static class PermissionGrantDecision
{
    public static PermissionGrantAction Decide(bool requestedGrant, bool defaultGranted, bool? existingGrant)
    {
        var matchesDefault = requestedGrant == defaultGranted;

        if (matchesDefault)
        {
            return existingGrant is not null ? PermissionGrantAction.Remove : PermissionGrantAction.NoOp;
        }

        if (existingGrant is null)
        {
            return PermissionGrantAction.Add;
        }

        return existingGrant.Value == requestedGrant ? PermissionGrantAction.NoOp : PermissionGrantAction.Update;
    }
}
