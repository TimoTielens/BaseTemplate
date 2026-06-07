using AppointMe.Crm.Contracts.Customers.Events;

namespace AppointMe.Crm.Customers.UpdateCustomer;

public static class UpdateCustomer
{
    extension(Customer customer)
    {
        public void Update(PersonName name, DateOfBirth? dateOfBirth, Gender? gender, Email? email)
        {
            customer.Name = name;
            customer.DateOfBirth = dateOfBirth;
            customer.Gender = gender;
            customer.Email = email;
            customer.Raise(new CustomerUpdatedEvent(
                customer.Id.Value,
                customer.CompanyId.Value,
                name.FirstName,
                name.LastName,
                dateOfBirth?.Value,
                email?.Value));
        }
    }
}
