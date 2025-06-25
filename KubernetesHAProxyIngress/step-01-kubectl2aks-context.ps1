$region = "westus2"
$rg = "fcm-haproxytest-westus2-rg"
$acr = "fcmhaproxytestwestus2acr"
$aks = "fcmhaproxytestwestus2aks"

Write-Debug "kubectl config use-context $aks"
kubectl config use-context $aks