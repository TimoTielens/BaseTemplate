using Microsoft.AspNetCore.DataProtection;

namespace AppointMe.Api.DataProtection;

internal static class DataProtectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Configures data protection with a stable application name, persisting keys to Azure Blob
        /// Storage when a <c>DataProtectionStorage</c> connection string is configured (otherwise the
        /// default local key ring is used).
        /// </summary>
        public IServiceCollection AddAppointMeDataProtection(IConfiguration configuration)
        {
            var dataProtection = services.AddDataProtection().SetApplicationName("appointme");

            var connectionString = configuration.GetConnectionString("DataProtectionStorage");
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                dataProtection.PersistKeysToAzureBlobStorage(
                    connectionString,
                    containerName: "data-protection-keys",
                    blobName: "keys.xml");
            }

            return services;
        }
    }
}
