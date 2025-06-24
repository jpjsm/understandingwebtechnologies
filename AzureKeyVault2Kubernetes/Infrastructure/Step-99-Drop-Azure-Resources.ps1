$DebugPreference = "Continue"

$region = "westus2"
$rg = "fcm-changemanagerwestus2rg"
$acr = "fcmchangemanagerwestus2acr"
$aks = "fcmchangemanagerwestus2aks"

# uninstall helm releases
Write-Debug "helm uninstall mtls-change-manager --namespace azurekeyvault-ns"
helm uninstall mtls-change-manager --namespace azurekeyvault-ns

# Delete AKS Cluster
Write-Debug "az aks delete --resource-group ${rg} --name ${aks}"
az aks delete --resource-group ${rg} --name ${aks} --yes

# Delete Azure Container Registry (ACR)
Write-Debug "az acr delete --name ${acr} --resource-group ${rg}"
az acr delete --name ${acr} --resource-group ${rg} --yes

# Create Azure Resource Group
Write-Debug "az group delete --name ${rg}"
az group delete --name ${rg} --yes
