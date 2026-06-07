using AppointMe.Organizations.Employees;

namespace AppointMe.Organizations.Companies;

public static class CompanyOwnership
{
    extension(Company company)
    {
        public bool IsPrimaryOwner(EmployeeId employeeId) =>
            company.PrimaryOwnerEmployeeId == employeeId;

        public IReadOnlyList<Role> LockedRolesFor(EmployeeId employeeId) =>
            company.IsPrimaryOwner(employeeId) ? [Role.Owner] : [];
    }
}
