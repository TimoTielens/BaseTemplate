using System.Text.Json.Nodes;
using AppointMe.Shared.Authorization.Roles;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AppointMe.Api.OpenApi;

public class RoleSchemaTransformer : IOpenApiSchemaTransformer
{
    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;

        if (typeof(Role).IsAssignableFrom(type))
        {
            schema.Type = JsonSchemaType.String;
            schema.Enum = Role.BuiltIn
                .Where(type.IsInstanceOfType)
                .Select(JsonNode (role) => JsonValue.Create(role.Name))
                .ToList();
            return;
        }

        var elementType = GetEnumerableElementType(type);
        if (elementType is not null && typeof(Role).IsAssignableFrom(elementType))
        {
            schema.Items = await context.GetOrCreateSchemaAsync(elementType, null, cancellationToken);
        }
    }

    private static Type? GetEnumerableElementType(Type type)
    {
        if (type.IsArray)
        {
            return type.GetElementType();
        }

        return type.GetInterfaces()
            .Append(type)
            .FirstOrDefault(candidate => candidate.IsGenericType
                                         && candidate.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            ?.GenericTypeArguments[0];
    }
}
