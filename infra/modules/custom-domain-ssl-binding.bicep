// Nested module — exists solely to break Bicep's "same-resource declared twice" limitation.
// The custom-domain module first creates the hostname binding with sslState=Disabled
// (so Azure can verify DNS and issue the managed cert). This module then updates the same
// binding with sslState=SniEnabled + the cert thumbprint. Because it's a separate deployment,
// ARM accepts the update instead of complaining about duplicate resources.

@description('Web app name (parent for the binding).')
param siteName string

@description('Custom hostname (e.g. app.appointme.dev).')
param customHostname string

@description('Managed certificate thumbprint, read from the cert resource in the parent module.')
param certificateThumbprint string

resource sniBinding 'Microsoft.Web/sites/hostNameBindings@2023-12-01' = {
  name: '${siteName}/${customHostname}'
  properties: {
    siteName: siteName
    hostNameType: 'Verified'
    sslState: 'SniEnabled'
    thumbprint: certificateThumbprint
  }
}
