using System.Text.Json.Nodes;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AppointMe.Api.OpenApi;

public class PermissionSchemaTransformer(IEnumerable<Permission> permissions) : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!typeof(Permission).IsAssignableFrom(context.JsonTypeInfo.Type))
        {
            return Task.CompletedTask;
        }

        schema.Type = JsonSchemaType.String;
        schema.Enum = permissions
            .Where(permission => context.JsonTypeInfo.Type.IsInstanceOfType(permission))
            .Select(JsonNode (permission) => JsonValue.Create(permission.Code))
            .ToList();

        return Task.CompletedTask;
    }
}
