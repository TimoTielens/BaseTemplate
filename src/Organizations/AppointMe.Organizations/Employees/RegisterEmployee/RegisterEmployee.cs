using AppointMe.Organizations.Contracts.Employees.Events;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Employees.RegisterEmployee;

public static class RegisterEmployee
{
    extension(Employee)
    {
        public static Employee Register(CompanyId companyId, PersonName name, Email email, IEnumerable<Role> roles,
            UserId userId, DateTimeOffset registrationDate)
        {
            var distinctRoles = roles.Distinct().ToList();
            if (distinctRoles.Count == 0)
            {
                throw new ValidationException("At least one role is required.");
            }

            var employee = new Employee
            {
                Id = new EmployeeId(NewId()),
                CompanyId = companyId,
                Name = name,
                Email = email,
                Roles = distinctRoles,
                UserId = userId,
                RegistrationDate = registrationDate,
                IsDeleted = false,
            };
            employee.Raise(new EmployeeRegistered(
                employee.Id.Value, employee.CompanyId.Value,
                name.FirstName, name.LastName, [.. distinctRoles]));
            return employee;
        }
    }
}
