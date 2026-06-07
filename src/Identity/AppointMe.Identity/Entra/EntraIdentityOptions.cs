using System.ComponentModel.DataAnnotations;

namespace AppointMe.Identity.Entra;

public class EntraIdentityOptions
{
    [Required]
    public required string TenantId { get; init; }

    [Required]
    public required string ClientId { get; init; }

    [Required]
    public required string ClientSecret { get; init; }
}
