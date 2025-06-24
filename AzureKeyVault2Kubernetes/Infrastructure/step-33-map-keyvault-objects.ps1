$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

# Get SP Id and pwd from keyvault

$AKS_SP_APPId = az keyvault secret show --vault-name $global:KeyVault_Name --name "AKS_SP_AppId"
$AKS_SP_Pwd = az keyvault secret set --vault-name $global:KeyVault_Name --name "AKS_SP_Pwd"

# Create Kubernetes secret to enable access to Keyvault
Write-Debug "kubectl create secret generic secrets-store-creds --from-literal clientid=$AKS_SP_APPId --from-literal clientsecret=********************** -n azurekeyvault-ns"
kubectl create secret generic secrets-store-creds --from-literal clientid=$AKS_SP_APPId --from-literal clientsecret=$AKS_SP_Pwd -n azurekeyvault-ns

# Install provider
Write-Debug "helm install azurekeyvault-csi-secret-provider csi-secrets-store-provider-azure/csi-secrets-store-provider-azure -n azurekeyvault-ns"
helm install azurekeyvault-csi-secret-provider csi-secrets-store-provider-azure/csi-secrets-store-provider-azure -n azurekeyvault-ns 


# Deployment
Write-Debug "kubectl apply -f .\azurekeyvault-secrets-map.yaml"
kubectl apply -f .\azurekeyvault-secrets-map.yaml --namespace azurekeyvault-ns
