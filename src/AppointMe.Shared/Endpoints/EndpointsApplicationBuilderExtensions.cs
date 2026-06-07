using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Endpoints;

public static class EndpointsApplicationBuilderExtensions
{
    extension(WebApplication app)
    {
        public WebApplication MapEndpoints()
        {
            var apiVersionSet = app
                .NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

            var versionedGroup = app
                .MapGroup("api/v1")
                .WithApiVersionSet(apiVersionSet);

            var endpoints = app.Services
                .GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoint(versionedGroup);
            }

            return app;
        }
    }
}
