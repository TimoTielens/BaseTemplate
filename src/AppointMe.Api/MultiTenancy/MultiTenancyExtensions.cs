using AppointMe.Shared.Companies.Detection;

namespace AppointMe.Api.MultiTenancy;

public static class MultiTenancyExtensions
{
    public static IServiceCollection AddAppointMeMultiTenancy(this IServiceCollection services,
        Action<CompanyDetectionBuilder> configure)
    {
        services.AddSingleton<ICurrentCompany, CurrentCompany>();

        var builder = new CompanyDetectionBuilder(services);
        configure(builder);

        return services;
    }

    public static IApplicationBuilder UseAppointMeMultiTenancy(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CompanyResolutionMiddleware>();
    }
}
