@description('Azure region for all resources.')
param location string

@description('Globally-unique Service Bus namespace name.')
param namespaceName string

@description('Tags applied to the namespace.')
param tags object = {}

@description('Service Bus SKU. Standard or higher is required for topics/subscriptions; devtest default: Standard.')
param sku string = 'Standard'

resource namespace 'Microsoft.ServiceBus/namespaces@2022-10-01-preview' = {
  name: namespaceName
  location: location
  tags: tags
  sku: {
    name: sku
    tier: sku
  }
  properties: {
    publicNetworkAccess: 'Enabled'
    minimumTlsVersion: '1.2'
  }
}

// The Service Bus connection string is NOT exposed as an output — pull it via
// `az servicebus namespace authorization-rule keys list ...` when seeding Key Vault.

output id string = namespace.id
output name string = namespace.name
output endpoint string = namespace.properties.serviceBusEndpoint
