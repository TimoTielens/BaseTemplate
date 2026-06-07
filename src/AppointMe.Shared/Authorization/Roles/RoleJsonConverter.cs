using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppointMe.Shared.Authorization.Roles;

public sealed class RoleJsonConverter : JsonConverter<Role>
{
    public override Role Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Role.Create(reader.GetString() ?? throw new JsonException("Role name expected."));

    public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);

    public override Role ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Role.Create(reader.GetString() ?? throw new JsonException("Role name expected."));

    public override void WriteAsPropertyName(Utf8JsonWriter writer, Role value, JsonSerializerOptions options) =>
        writer.WritePropertyName(value.Name);
}
