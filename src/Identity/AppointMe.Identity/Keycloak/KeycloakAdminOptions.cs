using System.ComponentModel.DataAnnotations;

namespace AppointMe.Identity.Keycloak;

public class KeycloakAdminOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Realm { get; init; }

    [Required]
    public required string ClientId { get; init; }

    [Required]
    public required string ClientSecret { get; init; }

}
