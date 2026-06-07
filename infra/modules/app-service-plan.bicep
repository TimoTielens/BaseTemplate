@description('Azure region for all resources.')
param location string

@description('App Service Plan name.')
param name string

@description('Tags applied to the plan.')
param tags object = {}

@description('Plan SKU. Devtest default: B1. Use S1+ for deployment slots.')
param sku object = {
  name: 'B1'
  tier: 'Basic'
}

resource plan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: name
  location: location
  tags: tags
  sku: sku
  kind: 'linux'
  properties: {
    reserved: true
  }
}

output id string = plan.id
output name string = plan.name
