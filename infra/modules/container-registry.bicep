@description('Azure region for all resources.')
param location string

@description('Globally-unique ACR name (alphanumeric, 5-50 chars).')
@minLength(5)
@maxLength(50)
param name string

@description('Tags applied to the registry.')
param tags object = {}

@description('ACR SKU. Devtest default: Basic.')
param sku string = 'Basic'

resource registry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: name
  location: location
  tags: tags
  sku: {
    name: sku
  }
  properties: {
    adminUserEnabled: false
    publicNetworkAccess: 'Enabled'
    anonymousPullEnabled: false
  }
}

output id string = registry.id
output name string = registry.name
output loginServer string = registry.properties.loginServer
