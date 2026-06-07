using AppointMe.Organizations.Companies.RegisterCompany;
using AppointMe.Organizations.Employees;
using AppointMe.Organizations.Employees.RegisterEmployee;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Companies.Onboarding;

public static class Onboarding
{
    extension(Company)
    {
        public static (Company company, Employee owner) Onboard(
            CompanyName name,
            TimeZoneInfo timeZone,
            PersonName ownerName,
            Email ownerEmail,
            UserId registeredBy,
            DateTimeOffset registrationDate)
        {
            var company = Company.Register(name, timeZone, registeredBy, registrationDate);
            var owner = Employee.Register(
                companyId: company.Id,
                name: ownerName,
                email: ownerEmail,
                roles: [Role.Owner, Role.Staff],
                userId: registeredBy,
                registrationDate: registrationDate);
            return (company, owner);
        }
    }

    extension(Company company)
    {
        public void AssignPrimaryOwner(EmployeeId primaryOwnerEmployeeId)
        {
            if (company.PrimaryOwnerEmployeeId is not null)
            {
                throw new ValidationException("Primary owner is already assigned.");
            }

            company.PrimaryOwnerEmployeeId = primaryOwnerEmployeeId;
        }
    }
}
