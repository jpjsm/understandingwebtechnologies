$region = "westus2"
$rg = "fcm-ingressdemo-westus2"
$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

# Delete AKS Cluster
write-host "az aks delete --resource-group $rg --name $aks"
az aks delete --resource-group $rg --name $aks

# Delete Azure Container Registry (ACR)
write-host "az acr delete --name $acr --resource-group $rg"
az acr delete --name $acr --resource-group $rg

# Create Azure Resource Group
write-host "az group delete --name $rg"
az group delete --name $rg
