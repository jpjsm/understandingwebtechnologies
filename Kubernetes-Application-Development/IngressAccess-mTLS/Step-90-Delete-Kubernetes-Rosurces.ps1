$DebugPreference = "Continue"

$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

$namespace = "ingress-mtls"
$secret = "aks-ingress-mtls"

# Delete demo applications
Write-Debug "kubectl delete -f aks-helloworld-one.yaml --namespace $namespace -v=0"
kubectl delete -f aks-helloworld-one.yaml --namespace $namespace -v=0

Write-Debug "kubectl delete -f aks-helloworld-two.yaml --namespace $namespace -v=0"
kubectl delete -f aks-helloworld-two.yaml --namespace $namespace -v=0

Write-Debug "kubectl delete -f hello-world-ingress.yaml --namespace $namespace -v=0"
kubectl delete -f hello-world-ingress.yaml --namespace $namespace -v=0

Write-Debug "kubectl delete secret $secret --namespace $namespace"
kubectl delete secret $secret --namespace $namespace

Write-Debug "helm uninstall mtls-ingress-nginx"
helm uninstall mtls-ingress-nginx

Write-Debug "helm repo remove mtls-ingress-nginx"
helm repo remove mtls-ingress-nginx --namespace $namespace

Write-Debug "kubectl delete namespace $namespace"
kubectl delete namespace $namespace
