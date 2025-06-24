$DebugPreference = "Continue"

Write-Debug "kubectl apply -f .\azurekeyvault-ns.yaml"
kubectl apply -f .\azurekeyvault-ns.yaml 
