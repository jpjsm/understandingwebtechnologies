$DebugPreference = "Continue"

Write-Debug "kubectl apply -f .\haproxytest-ns.yaml"
kubectl apply -f .\haproxytest-ns.yaml 

# Use Helm to deploy an NGINX ingress controller
Write-Debug 'helm install mtls-change-manager ingress-nginx/ingress-nginx --namespace haproxytest-ns --set controller.replicaCount=3 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux'
#helm install mtls-haproxytest ingress-nginx/ingress-nginx --namespace haproxytest-ns --set controller.service.loadBalancerIP=52.158.243.110 --set controller.replicaCount=3 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux --set rbac.create=false --set rbac.createRole=false --set rbac.createClusterRole=false
helm install fcm-haproxytest haproxy-ingress/haproxy-ingress --namespace=haproxytest-ns --set controller.hostNetwork=true

# verify load balancer
Write-Debug "kubectl --namespace haproxytest-ns get services -o wide -w mtls-change-manager-ingress-ingress-nginx-controller"
Write-Debug "Press CTRL-C to resume ..."
kubectl --namespace haproxytest-ns get services -o wide
