namespace AppointMe.Identity.Signup;

public sealed class SignupRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}
