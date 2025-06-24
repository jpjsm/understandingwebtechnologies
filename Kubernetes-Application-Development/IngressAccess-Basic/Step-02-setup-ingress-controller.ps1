$acr = "fcmingressdemoacr"
$aks = "fcmingressdemoaks"

$namespace = "ingress-basic"

# making sure context is the right one
kubectl config use-context fcmingressdemoaks

# Create a namespace for your ingress resources
kubectl create namespace ingress-basic

# Add the ingress-nginx repository
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx

# Use Helm to deploy an NGINX ingress controller
helm install nginx-ingress ingress-nginx/ingress-nginx --namespace ingress-basic --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux

# verify load balancer
write-host "kubectl --namespace ingress-basic get services -o wide -w nginx-ingress-ingress-nginx-controller"
write-host "Press CTRL-C to resume ..."
kubectl --namespace ingress-basic get services -o wide -w nginx-ingress-ingress-nginx-controller
