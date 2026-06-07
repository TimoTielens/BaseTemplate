using AppointMe.Crm.Contracts.Customers.Events;
using AppointMe.Crm.Customers.DeleteCustomer;

namespace AppointMe.Crm.Tests.Customers.DeleteCustomer;

public class DeleteCustomerTests
{
    [Fact]
    public void should_mark_customer_as_deleted_and_raise_event()
    {
        var customer = Create.Customer.Build();
        customer.Delete();

        Assert.True(customer.IsDeleted);

        var @event = customer.Events.OfType<CustomerDeletedEvent>().Single();
        Assert.Equal(customer.Id.Value, @event.CustomerId);
        Assert.Equal(customer.CompanyId.Value, @event.CompanyId);
    }
}
