using System.ComponentModel.DataAnnotations;

namespace AppointMe.Api.Authentication.Keycloak;

public class KeycloakOptions
{
    [Required]
    public required string Authority { get; init; }

    [Required]
    public required string FrontendClientId { get; init; }

    [Required]
    public required string FrontendClientSecret { get; init; }

    [Required]
    public required string ApiAudience { get; init; }
}
