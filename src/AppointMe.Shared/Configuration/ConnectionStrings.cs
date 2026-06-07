using Microsoft.Extensions.Configuration;

namespace AppointMe.Shared.Configuration;

public sealed class ConnectionStrings(IConfiguration configuration)
{
    public string AppointMeSql { get; } = configuration.GetConnectionString("AppointMeSql")
                                          ?? throw new InvalidOperationException(
                                              "Connection string 'AppointMeSql' not found");
}
