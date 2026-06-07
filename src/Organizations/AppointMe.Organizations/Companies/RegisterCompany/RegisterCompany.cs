using AppointMe.Organizations.Contracts.Companies.Events;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Companies.RegisterCompany;

public static class RegisterCompany
{
    extension(Company)
    {
        public static Company Register(CompanyName name, TimeZoneInfo timeZone, UserId registeredBy,
            DateTimeOffset registrationDate)
        {
            var company = new Company
            {
                Id = new CompanyId(NewId()),
                Name = name,
                TimeZone = timeZone,
                RegisteredBy = registeredBy,
                RegistrationDate = registrationDate,
                PrimaryOwnerEmployeeId = null,
            };

            company.Raise(new CompanyRegistered(
                company.Id.Value,
                company.Name.Value,
                company.TimeZone.Id,
                company.RegisteredBy.Value
            ));
            return company;
        }
    }
}
