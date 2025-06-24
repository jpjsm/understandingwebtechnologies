$DebugPreference = "Continue"

# get definitions
. ".\step-00-global.ps1"

# Create Azure Resource Group
Write-Debug "az group create --name $RG_Name --location $LOCATION"
az group create --name $RG_Name --location $LOCATION

