$DebugPreference = "Continue"

$region = "westus2"
$rg = "fcm-mtlsdemo-westus2"
$acr = "fcmmtlsdemoacr"
$aks = "fcmmtlsdemoaks"

# Delete AKS Cluster
Write-Debug "az aks delete --resource-group $rg --name $aks"
az aks delete --resource-group $rg --name $aks

# Delete Azure Container Registry (ACR)
Write-Debug "az acr delete --name $acr --resource-group $rg"
az acr delete --name $acr --resource-group $rg

# Create Azure Resource Group
Write-Debug "az group delete --name $rg"
az group delete --name $rg
