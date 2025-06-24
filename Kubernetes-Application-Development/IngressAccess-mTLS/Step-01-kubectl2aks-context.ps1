$DebugPreference = "Continue"

$region = "westus2"
$rg = "fcm-mtlsdemo-westus2"
$acr = "fcmmtlsdemoacr"
$aks = "fcmmtlsdemoaks"

Write-Debug "kubectl config use-context $aks"
kubectl config use-context $aks