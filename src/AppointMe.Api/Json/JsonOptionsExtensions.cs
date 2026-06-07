using System.Text.Json.Serialization;
using AppointMe.Shared.Authorization.Roles;
using Microsoft.AspNetCore.Http.Json;

namespace AppointMe.Api.Json;

internal static class JsonOptionsExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAppointMeJsonOptions()
        {
            return services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.Converters.Add(new RoleJsonConverter());
            });
        }
    }
}
