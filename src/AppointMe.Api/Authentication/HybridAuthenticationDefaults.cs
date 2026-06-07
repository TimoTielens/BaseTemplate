namespace AppointMe.Api.Authentication;

public static class HybridAuthenticationDefaults
{
    public const string AuthenticationScheme = "Hybrid";

    extension(HttpRequest request)
    {
        public bool HasBearerTokenHeader()
        {
            var authHeader = request.Headers.Authorization.FirstOrDefault();
            return authHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}
