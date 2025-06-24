# get definitions
. ".\step-00-global.ps1"

Write-Debug "kubectl config use-context $AKS_Name"
kubectl config use-context $AKS_Name 