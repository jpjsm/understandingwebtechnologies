$DebugPreference = "Continue"

kubectl delete secret secrets-store-creds -n azurekeyvault-ns

$deployments = @(".\azurekeyvault-secrets-map.yaml", ".\azurekeyvault-deployment.yaml", ".\azurekeyvault-services.yaml", ".\azurekeyvault-ns.yaml")

foreach($deployment in $deployments){
    Write-Debug "kubectl delete -f ${deployment}"
    kubectl delete -f ${deployment}
}
