@description('Azure region for all resources.')
param location string

@description('Globally-unique storage account name (lowercase, 3-24 chars).')
@minLength(3)
@maxLength(24)
param name string

@description('Tags applied to the account.')
param tags object = {}

@description('Name of the blob container holding the Data Protection key ring.')
param dataProtectionContainerName string = 'data-protection-keys'

resource storage 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: name
  location: location
  tags: tags
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
    allowBlobPublicAccess: false
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
    publicNetworkAccess: 'Enabled'
  }
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2023-05-01' = {
  parent: storage
  name: 'default'
  properties: {}
}

resource dataProtectionContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-05-01' = {
  parent: blobService
  name: dataProtectionContainerName
  properties: {
    publicAccess: 'None'
  }
}

// The storage connection string is NOT exposed as an output — pull it via
// `az storage account show-connection-string ...` when seeding Key Vault.

output accountName string = storage.name
output accountId string = storage.id
output dataProtectionContainerName string = dataProtectionContainer.name
