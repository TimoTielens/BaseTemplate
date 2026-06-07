// Provisions a custom domain + free App Service Managed Certificate.
//
// PRECONDITIONS (manual, at your DNS registrar):
//   1. A TXT record at `asuid.<subdomain>` set to the App Service's customDomainVerificationId
//      (read it with: az webapp show --query customDomainVerificationId -o tsv)
//   2. A CNAME record at `<subdomain>` pointing to `<appServiceName>.azurewebsites.net`
//   3. DNS has propagated (verify with `dig <customHostname> CNAME` and
//      `dig asuid.<customHostname> TXT`)
//
// The module does the rest:
//   - Hostname binding (initially with sslState=Disabled — Azure needs this to verify DNS
//     and issue the cert).
//   - App Service Managed Certificate (free, auto-renews every ~6 months via DigiCert).
//   - SNI binding update via nested module (uses the cert's thumbprint output).

@description('Web app name to bind the custom hostname to.')
param siteName string

@description('App Service Plan resource id — the cert lives at plan scope.')
param appServicePlanId string

@description('Custom hostname to bind (e.g. app.appointme.dev). Must be a subdomain; apex requires extra DNS setup not handled here.')
param customHostname string

@description('Azure region.')
param location string

@description('Tags applied to the certificate resource.')
param tags object = {}

resource webApp 'Microsoft.Web/sites@2023-12-01' existing = {
  name: siteName
}

// Phase 1: bind the hostname without SSL so Azure can verify DNS + issue the cert.
resource hostBindingDnsOnly 'Microsoft.Web/sites/hostNameBindings@2023-12-01' = {
  parent: webApp
  name: customHostname
  properties: {
    siteName: webApp.name
    hostNameType: 'Verified'
    sslState: 'Disabled'
  }
}

// Phase 2: issue the managed cert. Azure verifies the CNAME at <customHostname> still points
// at the App Service, then ACME-issues a cert through DigiCert. Auto-renews every ~6 months.
resource managedCert 'Microsoft.Web/certificates@2023-12-01' = {
  name: 'mc-${replace(customHostname, '.', '-')}'
  location: location
  tags: tags
  properties: {
    canonicalName: customHostname
    serverFarmId: appServicePlanId
  }
  dependsOn: [
    hostBindingDnsOnly
  ]
}

// Phase 3: flip the binding to SNI mode with the cert thumbprint. Nested module so ARM treats
// it as an update of the existing binding rather than a duplicate-resource declaration.
module sslBinding 'custom-domain-ssl-binding.bicep' = {
  name: 'ssl-binding-${replace(customHostname, '.', '-')}'
  params: {
    siteName: webApp.name
    customHostname: customHostname
    certificateThumbprint: managedCert.properties.thumbprint
  }
  dependsOn: [
    managedCert
  ]
}

output hostnameBindingId string = hostBindingDnsOnly.id
output certificateThumbprint string = managedCert.properties.thumbprint
output hostname string = customHostname
