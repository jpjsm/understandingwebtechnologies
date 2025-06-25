$DebugPreference = "Continue"

$namespace = "ingress-mtls"
$secret = "ingress-mtls-secret"

$tlscert = "..\mtls-certs\ingress-mtls.example.pem"
$tlskey = "..\mtls-certs\ingress-mtls.example.key.pem"
$cacrt = "..\mtls-certs\ca-root.crt"

# Delete secret before creating it
Write-Debug "kubectl delete secret aks-ingress-tls --namespace $namespace"
kubectl delete secret $secret --namespace $namespace

# Create secret
Write-Debug "kubectl create secret generic $secret --namespace $namespace --from-file=tls.crt=$tlscert --from-file=tls.key=$tlskey --from-file=ca.crt=$cacrt"
kubectl create secret generic $secret --namespace $namespace --from-file=tls.crt=$tlscert --from-file=tls.key=$tlskey --from-file=ca.crt=$cacrt
