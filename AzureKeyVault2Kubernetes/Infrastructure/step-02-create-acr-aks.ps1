$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

# Create Azure Container Registry (ACR)
Write-Debug "az acr create --name $ACR_Name --resource-group $RG_Name --sku standard --admin-enabled"
az acr create --name $ACR_Name --resource-group $RG_Name --sku standard --admin-enabled

# Create AKS Cluster
Write-Debug "az aks create --resource-group $RG_Name --name $AKS_Name --node-count 3 --generate-ssh-keys --attach-acr $ACR_Name --dns-name-prefix fcm-changemanager --load-balancer-sku standard"
az aks create --resource-group $RG_Name --name $AKS_Name --node-count 3 --generate-ssh-keys --attach-acr $ACR_Name --dns-name-prefix fcm-changemanager --load-balancer-sku standard --enable-managed-identity

# Get the Service Principal ID for AKS Cluster
$AKS_ServicePrincipal = az aks show -g $RG_Name -n $AKS_Name --query "identity.principalId" -o tsv

#  ensure the service principal used by the AKS cluster has delegated permissions to the resource group
az role assignment create --assignee $AKS_ServicePrincipal --role "Network Contributor" --scope /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RG_Name

# Login to cluster to generate context
Write-Debug "az aks get-credentials --name $AKS_Name --resource-group $RG_Name --overwrite-existing"
az aks get-credentials --name $AKS_Name --resource-group $RG_Name --overwrite-existing

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$ACR_LoginServer = az acr list --resource-group $RG_Name --query "[].{acrLoginServer:loginServer}" --output tsv
Write-Debug "'acrLoginServer:loginServer': ${ACR_LoginServer}"

# Log into the ACR
Write-Debug "az acr login --name $ACR_Name"
az acr login --name $ACR_Name
