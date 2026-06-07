using './main.bicep'

param environmentName = 'devtest'

// Populated at deploy-time via az CLI --parameters sqlAdminPassword=... or via Key Vault reference.
// Don't commit a real password here.
param sqlAdminLogin = 'appointme_admin'
param sqlAdminPassword = readEnvironmentVariable('SQL_ADMIN_PASSWORD', '')

// Entra External ID values, Wolverine transport, and demo flag now live in
// src/AppointMe.Api/appsettings.<Env>.json — Bicep no longer pushes them as App Service
// settings. Only secrets (KV refs), App Service plumbing, and ASPNETCORE_ENVIRONMENT remain
// in Bicep's appSettings.
//
// On the very first deployment use the public hello-world container so App Service can stand
// up before any image has been pushed. CI overwrites the image on each deploy via
// `az webapp config container set --container-image-name <acr>/appointme-api:<sha>`.
param initialContainerImage = readEnvironmentVariable('INITIAL_CONTAINER_IMAGE', 'mcr.microsoft.com/azuredocs/aci-helloworld:latest')

// Custom hostname is optional. Set CUSTOM_HOSTNAME=app.appointme.dev to bind a subdomain
// with a free App Service Managed Certificate. DNS records must already exist at the
// registrar (see infra/modules/custom-domain.bicep header).
param customHostname = readEnvironmentVariable('CUSTOM_HOSTNAME', '')
