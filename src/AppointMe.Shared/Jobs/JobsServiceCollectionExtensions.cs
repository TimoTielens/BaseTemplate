using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Jobs;

public static class JobsServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRecurringJobs(Assembly assembly)
        {
            return services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IRecurringJobRegistrar>(), false)
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        }
    }
}
