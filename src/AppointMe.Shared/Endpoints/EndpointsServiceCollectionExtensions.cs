using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Endpoints;

public static class EndpointsServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEndpoints(Assembly assembly)
        {
            return services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IEndpoint>(), false)
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
