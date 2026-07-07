namespace AppointMe.Api.Authorization;

/// <summary>
/// Authorization policy guarding the Hangfire dashboard. Requires an
/// authenticated, registered user — mirroring the API's fallback policy so the
/// ops dashboard is never reachable anonymously.
/// </summary>
public static class HangfireDashboardPolicy
{
    public const string Name = "HangfireDashboard";
}
