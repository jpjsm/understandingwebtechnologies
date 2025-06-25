$region = "westus2"
$rg = "fcm-ingressdemo-westus2"
$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

# Create Azure Resource Group
az group create --name $rg --location $region

# Create Azure Container Registry (ACR)
az acr create --name $acr --resource-group $rg --sku basic --admin-enabled

# Create AKS Cluster
az aks create --resource-group $rg --name $aks --node-count 2 --generate-ssh-keys --attach-acr $acr

# Login to cluster to generate context
az aks get-credentials --name $aks --resource-group $rg

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$acrlist = az acr list --resource-group $rg --query "[].{acrLoginServer:loginServer}" --output table
$acrloginserver = $acrlist[-1]
write-host "acrloginserver: ${acrloginserver}"

# Log into the ACR
write-host "az acr login --name ${acr}"
az acr login --name $acr
