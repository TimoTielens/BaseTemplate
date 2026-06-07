
namespace AppointMe.Crm.Customers.Database;

internal static class CustomerQueries
{
    extension(IQueryable<Customer> customers)
    {
        public async Task<Customer> LoadAsync(CustomerId id, CancellationToken cancellationToken)
        {
            var customer = await customers
                .SingleOrDefaultAsync(customer => customer.Id == id, cancellationToken);

            return customer ?? throw new NotFoundException($"Customer with id='{id.Value}' not found");
        }
    }
}
