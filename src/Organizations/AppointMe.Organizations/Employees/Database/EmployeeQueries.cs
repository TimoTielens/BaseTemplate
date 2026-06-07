namespace AppointMe.Organizations.Employees.Database;

internal static class EmployeeQueries
{
    extension(IQueryable<Employee> employees)
    {
        public async Task<Employee> LoadAsync(EmployeeId id, CompanyId companyId, CancellationToken cancellationToken)
        {
            var employee = await employees.SingleOrDefaultAsync(employee =>
                employee.Id == id && employee.CompanyId == companyId, cancellationToken);

            return employee ?? throw new NotFoundException($"Employee with id='{id.Value}' not found");
        }
    }
}
