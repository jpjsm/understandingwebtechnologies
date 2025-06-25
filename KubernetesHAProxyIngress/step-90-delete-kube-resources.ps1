$DebugPreference = "Continue"

$secrets = @("haproxytest-secret-tls")

foreach($secret in $secrets){
    Write-Debug "kubectl delete secret ${secret} -n haproxytest-ns"
    kubectl delete secret ${secret} -n haproxytest-ns
}

kubectl delete configmap haproxytest-map -n haproxytest-ns

$deployments = @(".\haproxytest-ingress-api-aadtls.yaml", ".\haproxytest-deployment.yaml", ".\haproxytest-services.yaml")

foreach($deployment in $deployments){
    Write-Debug "kubectl delete -f ${deployment}"
    kubectl delete -f ${deployment}
}
