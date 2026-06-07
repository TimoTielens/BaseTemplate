namespace AppointMe.Organizations.Companies.Database;

internal static class CompanyQueries
{
    extension(IQueryable<Company> companies)
    {
        public async Task<Company> LoadAsync(CompanyId id, CancellationToken cancellationToken)
        {
            var company = await companies.SingleOrDefaultAsync(company => company.Id == id, cancellationToken);

            return company ?? throw new NotFoundException($"Company with id='{id.Value}' not found");
        }
    }
}
