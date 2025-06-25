$region = "westus2"
$rg = "fcm-react2api-demo-westus2-rg"
$acr = "fcmreact2apidemowestus2acr"
$aks = "fcmreact2apidemowestus2aks"

# Create Azure Resource Group
az group create --name $rg --location $region


# Create Azure Container Registry (ACR)
az acr create --name $acr --resource-group $rg --sku basic --admin-enabled

# Create AKS Cluster
az aks create --resource-group $rg --name $aks --node-count 2 --generate-ssh-keys --attach-acr $acr
