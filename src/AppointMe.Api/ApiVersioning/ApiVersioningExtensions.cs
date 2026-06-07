using Asp.Versioning;

namespace AppointMe.Api.ApiVersioning;

public static class ApiVersioningExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAppointMeApiVersioning()
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            return services;
        }
    }
}
