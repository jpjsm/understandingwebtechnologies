$rg = "fcm-react2api-demo-westus2-rg"
$acr = "fcmreact2apidemowestus2acr"
$aks = "fcmreact2apidemowestus2aks"

$images = @("pronostico", "pronosticobackend")
$imageversion = "V_14"

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$acrlist = az acr list --resource-group $rg --query "[].{acrLoginServer:loginServer}" --output table
$acrloginserver = $acrlist[-1]
write-host "acrloginserver: ${acrloginserver}"

# Log into the ACR
write-host "az acr login --name ${acr}"
az acr login --name $acr

# Tag and push the images
foreach($image in $images)
{
    Push-Location ..\${image}
    $currentlocation = Get-Location
    Write-Host "Current location: ${currentlocation}"
    write-host "docker build -t ${image}:latest ."
    docker build -t ${image}:latest .
    Pop-Location

    
    write-host "docker tag ${image}:latest ${acrloginserver}/${image}:${imageversion}"
    docker tag ${image}:latest ${acrloginserver}/${image}:${imageversion}

    write-host "docker push ${acrloginserver}/${image}:${imageversion}"
    docker push ${acrloginserver}/${image}:${imageversion}
}

# Get Azure Kubernetes Service (AKS) cluster credentials
az aks get-credentials --resource-group $rg --name $aks

# => Change your working directory to the location of the deploy-<app>.yml files

# Deploy Kubernetes infrastructure
kubectl apply -f deploy-changemanager.yml

# Visual inspaction of deployment
kubectl get all