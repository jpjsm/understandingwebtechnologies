$DebugPreference = "Continue"


# Deployment
Write-Debug "kubectl apply -f .\azurekeyvault-deployment.yaml"
kubectl apply -f .\azurekeyvault-deployment.yaml --namespace azurekeyvault-ns

# Services
Write-Debug "kubectl apply -f .\azurekeyvault-services.yaml"
kubectl apply -f .\azurekeyvault-services.yaml --namespace azurekeyvault-ns
 