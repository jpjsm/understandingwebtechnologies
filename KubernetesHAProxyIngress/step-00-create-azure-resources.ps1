$DebugPreference = "Continue"

# Create Azure Resource Group
Write-Debug "az group create --name fcm-haproxytestwestus2rg --location westus2"
az group create --name fcm-haproxytestwestus2rg --location westus2

# Create Azure Container Registry (ACR)
Write-Debug "az acr create --name fcmhaproxytestwestus2acr --resource-group fcm-haproxytestwestus2rg --sku basic --admin-enabled"
az acr create --name fcmhaproxytestwestus2acr --resource-group fcm-haproxytestwestus2rg --sku standard --admin-enabled

# Create AKS Cluster
Write-Debug "az aks create --resource-group fcm-haproxytestwestus2rg --name fcmhaproxytestwestus2aks --node-count 3 --generate-ssh-keys --attach-acr fcmhaproxytestwestus2acr --dns-name-prefix fcm-haproxytest --load-balancer-sku standard"
az aks create --resource-group fcm-haproxytestwestus2rg --name fcmhaproxytestwestus2aks --node-count 3 --generate-ssh-keys --attach-acr fcmhaproxytestwestus2acr --dns-name-prefix fcm-haproxytest --load-balancer-sku standard

# Login to cluster to generate context
Write-Debug "az aks get-credentials --name fcmhaproxytestwestus2aks --resource-group fcm-haproxytestwestus2rg --overwrite-existing"
az aks get-credentials --name fcmhaproxytestwestus2aks --resource-group fcm-haproxytestwestus2rg --overwrite-existing

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$acrlist = az acr list --resource-group fcm-haproxytestwestus2rg --query "[].{acrLoginServer:loginServer}" --output table
$acrloginserver = $acrlist[-1]
Write-Debug "acrloginserver: ${acrloginserver}"

# Log into the ACR
Write-Debug "az acr login --name fcmhaproxytestwestus2acr"
az acr login --name fcmhaproxytestwestus2acr

# Add the haproxy-ingress repository
Write-Debug "helm repo add haproxy-ingress https://haproxy-ingress.github.io/charts"
helm repo add haproxy-ingress https://haproxy-ingress.github.io/charts
