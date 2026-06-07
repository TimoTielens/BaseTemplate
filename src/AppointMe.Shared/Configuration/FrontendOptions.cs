using System.ComponentModel.DataAnnotations;

namespace AppointMe.Shared.Configuration;

public class FrontendOptions
{
    private const string InvitationPath = "/auth/login";

    [Required, Url]
    public required Uri BaseUrl { get; init; }

    public string InvitationUrl => new Uri(BaseUrl, InvitationPath).ToString();
}
