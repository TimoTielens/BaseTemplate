using System.Reflection;
using AppointMe.Shared.Authorization.Permissions.DefaultGrants;
using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Authorization.Permissions;

public static class PermissionsServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPermissions(Assembly assembly)
        {
            foreach (var permission in ScanPermissions(assembly))
            {
                services.AddSingleton(permission);
            }

            return services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IDefaultGrantPolicy>(), publicOnly: false)
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        }
    }

    private static IEnumerable<Permission> ScanPermissions(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(IsPermissionContainer)
            .SelectMany(GetPermissionsFromType);
    }

    private static bool IsPermissionContainer(Type type)
    {
        return type is { IsAbstract: true, IsSealed: true }
               && type.Name.EndsWith("Permissions", StringComparison.Ordinal);
    }

    private static IEnumerable<Permission> GetPermissionsFromType(Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(fieldInfo => typeof(Permission).IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (Permission)fieldInfo.GetValue(null)!);
    }
}
