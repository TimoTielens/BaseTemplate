using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Api.OpenApi;

internal static class OpenApiExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAppointMeOpenApi()
        {
            services.AddOpenApi(options =>
            {
                options.AddSchemaTransformer<PermissionSchemaTransformer>();
                options.AddSchemaTransformer<RoleSchemaTransformer>();
                options.AddDocumentTransformer(async (document, context, cancellationToken) =>
                {
                    var permissionSchema =
                        await context.GetOrCreateSchemaAsync(typeof(Permission), null, cancellationToken);
                    document.AddComponent(nameof(Permission), permissionSchema);

                    var roleSchema = await context.GetOrCreateSchemaAsync(typeof(Role), null, cancellationToken);
                    document.AddComponent(nameof(Role), roleSchema);
                });
            });

            return services;
        }
    }
}
