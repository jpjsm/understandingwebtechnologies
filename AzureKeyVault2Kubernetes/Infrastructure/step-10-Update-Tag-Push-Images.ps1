$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

$images = @("webapi", "ingress")
$imageversion = "V_0.05"

# Get ACR Login Server to include in Docker tag
# - the image needs to be tagged with the login server address of your registry
$global:ACR_LoginServer = az acr list --resource-group $global:RG_Name --query "[].{acrLoginServer:loginServer}" --output tsv
Write-Debug "global:ACR_LoginServer: ${global:ACR_LoginServer}"

# Log into the ACR
Write-Debug "az acr login --name ${global:ACR_Name}"
az acr login --name ${global:ACR_Name}

# Tag and push the images
foreach($image in $images)
{
    Push-Location ..\${image}
    $currentlocation = Get-Location
    Write-Debug "Current location: ${currentlocation}"

    Write-Debug "docker build -t ${image}:latest ."
    docker build -t ${image}:latest .
    Pop-Location

    
    Write-Debug "docker tag ${image}:latest ${global:ACR_LoginServer}/${image}:${imageversion}"
    docker tag ${image}:latest ${global:ACR_LoginServer}/${image}:${imageversion}

    Write-Debug "docker push ${global:ACR_LoginServer}/${image}:${imageversion}"
    docker push ${global:ACR_LoginServer}/${image}:${imageversion}
}
