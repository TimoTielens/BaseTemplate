using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Companies.Detection;

public class CompanyDetectionBuilder(IServiceCollection services)
{
    public CompanyDetectionBuilder FromHeader(string headerName)
    {
        services.AddSingleton<ICompanyDetection>(new HeaderCompanyDetection(headerName));
        return this;
    }
}
