$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

$SP_Info = az ad sp create-for-rbac --skip-assignment -n $global:AKS_SP_Name -o tsv --query "[appId,password]"
$global:AKS_SP_AppId = $SP_Info[0]
$global:AKS_SP_Pwd = $SP_Info[1]

az keyvault secret set --vault-name $global:KeyVault_Name --name "AKS_SP_AppId" --value $global:AKS_SP_AppId
az keyvault secret set --vault-name $global:KeyVault_Name --name "AKS_SP_Pwd" --value $global:AKS_SP_Pwd

az keyvault set-policy -n $global:KeyVault_Name --spn $global:AKS_SP_AppId --secret-permissions get list --certificate-permissions get list --key-permissions get list --storage-permissions get list
