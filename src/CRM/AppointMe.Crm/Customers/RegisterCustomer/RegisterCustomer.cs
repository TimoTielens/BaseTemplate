using AppointMe.Crm.Contracts.Customers.Events;

namespace AppointMe.Crm.Customers.RegisterCustomer;

public static class RegisterCustomer
{
    extension(Customer)
    {
        public static Customer Register(CompanyId companyId, PersonName name, DateOfBirth? dateOfBirth,
            Gender? gender, Email? email, DateTimeOffset registrationDate)
        {
            var customer = new Customer
            {
                Id = new CustomerId(NewId()),
                CompanyId = companyId,
                Name = name,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Email = email,
                RegistrationDate = registrationDate,
                IsDeleted = false
            };

            customer.Raise(new CustomerRegisteredEvent(
                customer.Id.Value, companyId.Value,
                name.FirstName, name.LastName, dateOfBirth?.Value, email?.Value));

            return customer;
        }
    }
}
