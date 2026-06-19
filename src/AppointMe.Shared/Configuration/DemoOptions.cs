namespace AppointMe.Shared.Configuration;

public class DemoOptions
{
    public bool Enabled { get; init; }

    public DemoUserOptions? User { get; init; }
}

public class DemoUserOptions
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public string? Name { get; init; }
}
