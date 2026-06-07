using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AppointMe.Api.Authentication;

public class IdentityProviderOptions
{
    public required string Authority { get; init; }
    public required string OidcClientId { get; init; }
    public required string OidcClientSecret { get; init; }
    public required string ApiAudience { get; init; }
    public Action<OpenIdConnectOptions>? CustomizeOidc { get; init; }
}
