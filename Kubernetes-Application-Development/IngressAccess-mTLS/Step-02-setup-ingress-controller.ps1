$DebugPreference = "Continue"

$region = "westus2"
$rg = "fcm-mtlsdemo-westus2"
$acr = "fcmmtlsdemoacr"
$aks = "fcmmtlsdemoaks"

$namespace = "ingress-mtls"

# making sure context is the right one
Write-Debug "kubectl config use-context $aks"
kubectl config use-context $aks

# Create a namespace for your ingress resources
Write-Debug "kubectl create namespace ingress-mtls"
kubectl create namespace $namespace

# Clear the repo
Write-Debug "helm repo remove mtls-ingress-nginx"
helm repo remove mtls-ingress-nginx

# Add the ingress-nginx repository
Write-Debug "helm repo add mtls-ingress-nginx https://kubernetes.github.io/ingress-nginx"
helm repo add mtls-ingress-nginx https://kubernetes.github.io/ingress-nginx

# Use Helm to deploy an NGINX ingress controller
Write-Debug 'helm install mtls-ingress-nginx ingress-nginx/ingress-nginx --namespace $namespace --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux'
helm install mtls-ingress-nginx ingress-nginx/ingress-nginx --namespace $namespace --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux

# verify load balancer
Write-Debug "kubectl --namespace ingress-mtls get services -o wide -w mtls-ingress-nginx-controller"
Write-Debug "Press CTRL-C to resume ..."
kubectl --namespace $namespace get services -o wide -w mtls-ingress-nginx-controller
