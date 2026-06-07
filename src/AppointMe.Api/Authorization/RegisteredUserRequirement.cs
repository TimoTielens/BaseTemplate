using Microsoft.AspNetCore.Authorization;

namespace AppointMe.Api.Authorization;

public sealed record RegisteredUserRequirement : IAuthorizationRequirement;
