using AppointMe.Crm.Contracts.Customers.Events;

namespace AppointMe.Crm.Customers.DeleteCustomer;

public static class DeleteCustomer
{
    extension(Customer customer)
    {
        public void Delete()
        {
            customer.IsDeleted = true;
            customer.Raise(new CustomerDeletedEvent(customer.Id.Value, customer.CompanyId.Value));
        }
    }
}
