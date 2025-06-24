$DebugPreference = "Continue"

# Secrets
Write-Debug "kubectl delete secret haproxytest-secret-tls --namespace haproxytest-ns"
kubectl delete secret haproxytest-secret-tls --namespace haproxytest-ns

Write-Debug "kubectl create secret tls haproxytest-secret-tls --namespace haproxytest-ns --cert=.\changemanager.fcm.msftcloudes.com.pem --key=.\changemanager.fcm.msftcloudes.com.key"
kubectl create secret tls haproxytest-secret-tls --namespace haproxytest-ns --cert=.\changemanager.fcm.msftcloudes.com.pem --key=.\changemanager.fcm.msftcloudes.com.key

#Write-Debug "kubectl delete secret changemanager-secret-mtls --namespace changemanager-ns"
#kubectl delete secret changemanager-secret-mtls --namespace changemanager-ns

# Deployment
Write-Debug "kubectl apply -f .\haproxytest-deployment.yaml"
kubectl apply -f .\haproxytest-deployment.yaml --namespace haproxytest-ns

# Services
Write-Debug "kubectl apply -f .\changemanager-services.yaml"
kubectl apply -f .\haproxytest-services.yaml --namespace haproxytest-ns

# Create a ConfigMap named haproxytest-map
Write-Debug ""
kubectl --namespace=haproxytest-ns create configmap haproxytest-map

# Entrypoints
Write-Debug "kubectl apply -f .\haproxytest-ingress-api-aadtls.yaml --namespace haproxytest-ns"
kubectl apply -f .\haproxytest-ingress-api-aadtls.yaml --namespace haproxytest-ns
 
# Write-Debug "kubectl apply -f .\changemanager-ingress-ui-aadtls.yaml --namespace changemanager-ns"
# kubectl apply -f .\changemanager-ingress-ui-aadtls.yaml --namespace changemanager-ns
 
# Write-Debug "kubectl apply -f .\changemanager-ingress-mtls.yaml --namespace changemanager-ns"
# kubectl apply -f .\changemanager-ingress-mtls.yaml --namespace changemanager-ns
 
# Write-Debug "kubectl apply -f .\changemanager-ingress-api-swagger.yaml --namespace changemanager-ns"
# kubectl apply -f .\changemanager-ingress-api-swagger.yaml --namespace changemanager-ns
 