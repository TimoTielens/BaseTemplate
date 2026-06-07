using System.ComponentModel.DataAnnotations;

namespace AppointMe.Api.Authentication.EntraExternalId;

public class EntraExternalIdOptions
{
    [Required]
    public required string Authority { get; init; }

    [Required]
    public required string ClientId { get; init; }

    [Required]
    public required string ClientSecret { get; init; }

    [Required]
    public required string ApiAudience { get; init; }
}
