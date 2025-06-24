$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

$namespace = "ingress-basic"

# Deploy demo applications
kubectl apply -f aks-helloworld-one.yaml --namespace ingress-basic -v=0
kubectl apply -f aks-helloworld-two.yaml --namespace ingress-basic -v=0

kubectl apply -f hello-world-ingress.yaml --namespace ingress-basic -v=0