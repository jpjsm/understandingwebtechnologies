$DebugPreference = "Continue"

$region = "westus2"
$rg = "fcm-mtlsdemo-westus2"
$acr = "fcmmtlsdemoacr"
$aks = "fcmmtlsdemoaks"

# Create Azure Resource Group
Write-Debug "az group create --name $rg --location $region"
az group create --name $rg --location $region

# Create Azure Container Registry (ACR)
Write-Debug "az acr create --name $acr --resource-group $rg --sku basic --admin-enabled"
az acr create --name $acr --resource-group $rg --sku basic --admin-enabled

# Create AKS Cluster
Write-Debug "az aks create --resource-group $rg --name $aks --node-count 2 --generate-ssh-keys --attach-acr $acr"
az aks create --resource-group $rg --name $aks --node-count 2 --generate-ssh-keys --attach-acr $acr

# Login to cluster to generate context
Write-Debug "az aks get-credentials --name $aks --resource-group $rg"
az aks get-credentials --name $aks --resource-group $rg

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$acrlist = az acr list --resource-group $rg --query "[].{acrLoginServer:loginServer}" --output table
$acrloginserver = $acrlist[-1]
Write-Debug "acrloginserver: ${acrloginserver}"

# Log into the ACR
Write-Debug "az acr login --name ${acr}"
az acr login --name $acr
