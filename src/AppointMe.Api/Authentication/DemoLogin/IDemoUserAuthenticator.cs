namespace AppointMe.Api.Authentication.DemoLogin;

/// <summary>
/// Authenticates the pre-provisioned demo user against the configured identity provider,
/// returning the raw OIDC <c>id_token</c> (a JWT) on success, or <c>null</c> on failure.
/// Implementations are provider-specific (Keycloak ROPC, Entra native authentication).
/// </summary>
internal interface IDemoUserAuthenticator
{
    Task<string?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken);
}
