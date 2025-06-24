$DebugPreference = "Continue" 
if (-not (Test-Path variable:Definitions)) {
    #shared definitions
    $global:SUBSCRIPTION_ID = "e1573d42-0032-4eb9-a4e3-2f7a429afb81"          ## MSG Starburst - CHM INT
    $global:SUBSCRIPTION_TenantId = az account show -s $global:SUBSCRIPTION_ID --query "tenantId" --output tsv

    $global:RG_Name = "fcm-test03-westus2rg"
    $global:LOCATION = "westus2"
    $global:ACR_Name = "fcmtest03westus2acr"
    $global:AKS_Name = "fcmtest03westus2aks"
    $global:Identity_Name = "fcm-test03-aks-useridentity"
    $global:KeyVault_Name = "fcm-test03-kv"
    $global:KeyVault_DBA_cnx_secret_name = "fcm-test03-sqldb-admin-cnx"
    $global:KeyVault_appsettings_name= "appsettingssecretsjson"
    $global:PublicIP_Name = "fcm-test03-public-ip"
    $global:SqlServer_DbName = "fcm-test03-sqldb"
    $global:SqlServer_AdminName = "fcm-test03-sqladmin"
    $global:SqlServer_UserName = "fcm-test03-sqluser"
    $global:AKS_azure_kvmap = "azure-kvmap"
    $global:AKS_SP_Name = "fcmtest02-aks-sp"
    
    # Connect to azure
    az login 
    
    # set subcription to use
    az account set --subscription $SUBSCRIPTION_ID  ## MSG Starburst - CHM INT
    
    # this steps sets up your azure subscription to enable AKS
    
    az provider register -n Microsoft.Network
    az provider register -n Microsoft.Storage
    az provider register -n Microsoft.Compute
    az provider register -n Microsoft.ContainerService
      
    $global:Definitions = $true
}

