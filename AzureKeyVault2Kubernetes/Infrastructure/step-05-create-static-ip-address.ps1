$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

# Create a static IP address 
$global:PublicIP_Address = az network public-ip create --resource-group $RG_Name --name fcm-changemanager-public-ip --sku Standard --allocation-method static --query ipAddress --output tsv

Write-Debug $global:PublicIP_Address
