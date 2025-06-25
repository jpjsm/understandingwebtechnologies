$DebugPreference = "Continue"

$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

$namespace = "ingress-mtls"

# Deploy demo applications
Write-Debug "kubectl apply -f aks-helloworld-one.yaml --namespace $namespace -v=0"
kubectl apply -f aks-helloworld-one.yaml --namespace $namespace -v=0
Write-Debug "kubectl apply -f aks-helloworld-two.yaml --namespace $namespace -v=0"
kubectl apply -f aks-helloworld-two.yaml --namespace $namespace -v=0

Write-Debug "kubectl apply -f hello-world-ingress.yaml --namespace $namespace -v=0"
kubectl apply -f hello-world-ingress.yaml --namespace $namespace -v=0