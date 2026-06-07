namespace AppointMe.Shared.Authorization.Roles;

public record Role(string Name)
{
    public const int MaxLength = 50;

    public static readonly SystemRole Owner = new("Owner");
    public static readonly Role Manager = new("Manager");
    public static readonly Role Staff = new("Staff");
    public static readonly Role Receptionist = new("Receptionist");

    public static IReadOnlyCollection<Role> BuiltIn { get; } = [Owner, Manager, Staff, Receptionist];
    public static IReadOnlyCollection<Role> Configurable { get; } = BuiltIn.Where(role => role is not SystemRole).ToArray();
}

public static class RoleFactory
{
    extension(Role)
    {
        public static Role Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Role name cannot be empty.", code: "role_name_empty");
            }

            if (name.Length > Role.MaxLength)
            {
                throw new ValidationException($"Role name cannot exceed {Role.MaxLength} characters.",
                    code: "role_name_too_long");
            }

            return Role.BuiltIn.FirstOrDefault(role => role.Name == name) ?? new Role(name);
        }
    }
}
