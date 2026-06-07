using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppointMe.Shared.Authorization.Roles;

public sealed class RoleValueConverter() : ValueConverter<Role, string>(role => role.Name, name => Role.Create(name));
