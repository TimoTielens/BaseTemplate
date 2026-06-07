using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.Client;
using FS.Keycloak.RestApiClient.Authentication.ClientFactory;
using FS.Keycloak.RestApiClient.Authentication.Flow;
using FS.Keycloak.RestApiClient.Client;
using FS.Keycloak.RestApiClient.Model;
using Microsoft.Extensions.Options;
using ApiClientFactory = FS.Keycloak.RestApiClient.ClientFactory.ApiClientFactory;

namespace AppointMe.Identity.Keycloak;

public sealed class KeycloakIdentityProvider(IOptions<KeycloakAdminOptions> adminOptions) : IIdentityProvider
{
    private const string RequiredActionVerifyEmail = "VERIFY_EMAIL";
    private const string RequiredActionUpdatePassword = "UPDATE_PASSWORD";

    public async Task<IdentityProviderUserId> CreateUserWithPasswordSetup(string email, PersonName? name,
        string redirectUri, CancellationToken cancellationToken)
    {
        var options = adminOptions.Value;
        using var httpClient = CreateHttpClient(options);
        using var usersApi = ApiClientFactory.Create<UsersApi>(httpClient);

        try
        {
            var response = await usersApi.PostUsersWithHttpInfoAsync(options.Realm, new UserRepresentation
            {
                Username = email,
                Email = email,
                FirstName = name?.FirstName,
                LastName = name?.LastName,
                EmailVerified = false,
                Enabled = true,
                RequiredActions = [RequiredActionVerifyEmail, RequiredActionUpdatePassword]
            }, cancellationToken);
            var userId = response.ExtractUserIdFromLocation();

            await usersApi.PutUsersSendVerifyEmailByUserIdAsync(
                options.Realm,
                userId,
                options.ClientId,
                null,
                redirectUri: redirectUri,
                cancellationToken
            );

            return new IdentityProviderUserId(userId);
        }
        catch (ApiException ex) when (ex.ErrorCode == 409)
        {
            // User already exists — this is fine for invitations, the user might already have an account
            var existingUsers = await usersApi.GetUsersAsync(options.Realm, email: email,
                cancellationToken: cancellationToken);
            var existingUser = existingUsers.FirstOrDefault()
                               ?? throw new ConflictException("User exists with same email", "email_already_exists");
            return new IdentityProviderUserId(existingUser.Id);
        }
    }

    public async Task ResendInvitationEmail(string email, string redirectUri, CancellationToken cancellationToken)
    {
        var options = adminOptions.Value;
        using var httpClient = CreateHttpClient(options);
        using var usersApi = ApiClientFactory.Create<UsersApi>(httpClient);

        var users = await usersApi.GetUsersAsync(options.Realm, email: email, exact: true,
            cancellationToken: cancellationToken);
        var user = users.FirstOrDefault()
                   ?? throw new NotFoundException($"No Keycloak user found for email '{email}'.");

        await usersApi.PutUsersSendVerifyEmailByUserIdAsync(
            options.Realm,
            user.Id,
            options.ClientId,
            null,
            redirectUri: redirectUri,
            cancellationToken
        );
    }

    private static AuthenticationHttpClient CreateHttpClient(KeycloakAdminOptions options)
    {
        return AuthenticationHttpClientFactory.Create(new ClientCredentialsFlow
        {
            KeycloakUrl = options.BaseUrl,
            Realm = options.Realm,
            ClientId = options.ClientId,
            ClientSecret = options.ClientSecret
        });
    }
}

file static class ApiResponseExtensions
{
    extension(ApiResponse<object?> response)
    {
        public string ExtractUserIdFromLocation()
        {
            var location = response.Headers["Location"].FirstOrDefault();
            return location?.Split('/').LastOrDefault()
                   ?? throw new InvalidOperationException("Failed to get user id");
        }
    }
}
