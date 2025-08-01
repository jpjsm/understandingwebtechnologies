# No command echo in PS :(

# Definitions
$RESOURCEGROUP_AKS="fcm-keyvault2kubernetes-rg"                          ## matrix-k8s-rbac-dev-we
$LOCATION = "westus2"
$AKS_NAME="fcmkeyvault2kubernetesaks"                                    ##matrix-aks-rbac-dev-we
$RESOURCEGROUP_KEYVAULT="fcm-keyvault2kubernetes-rg"                     ##matrix-keyvault-rbac-dev-we
$KEYVAULT_NAME="fcmkeyvault2kubernetes"                                  ##matrix-secrets-rbac-dev
$IDENTITY_AKSKEYVAULT_NAME="identity-fcm-keyvault2kubernetes"
$K8S_KEYVAULTACCESS_LABEL="keyvaultaccess"

# Setup infrastructure
az group create --name $RESOURCEGROUP_AKS --location $LOCATION

# Create Key Vault
az keyvault create --name $KEYVAULT_NAME --resource-group $RESOURCEGROUP_KEYVAULT --enabled-for-template-deployment --enabled-for-deployment

# Create little secret
az keyvault secret set --name magicwords --vault-name $KEYVAULT_NAME --value "Hocus Pocus"

# Create AKS Cluster
az aks create --resource-group $RESOURCEGROUP_AKS --name $AKS_NAME --node-count 3 --generate-ssh-keys --dns-name-prefix fcm-keyvault2kubernetes --load-balancer-sku standard --enable-managed-identity

# Create Identity
$IDENTITY_CLIENTID=$(az identity create -g $RESOURCEGROUP_AKS -n $IDENTITY_AKSKEYVAULT_NAME --query clientId -o tsv)
$IDENTITY_RESOURCEID=$(az identity show -g $RESOURCEGROUP_AKS -n $IDENTITY_AKSKEYVAULT_NAME --query id -o tsv)
$IDENTITY_PRINCIPALID=$(az identity show -g $RESOURCEGROUP_AKS -n $IDENTITY_AKSKEYVAULT_NAME --query principalId -o tsv)

# Login to cluster to generate context
az aks get-credentials --name $AKS_NAME --resource-group $RESOURCEGROUP_AKS --overwrite-existing

start-sleep -Seconds 30 # sometimes the Service Principal is not there yet
az keyvault set-policy -n $KEYVAULT_NAME --secret-permissions get list --spn $IDENTITY_CLIENTID

$SUBSCRIPTIONID=$(az account show --query id -o tsv)
$NODERESOURCEGROUP=$(az aks show --resource-group $RESOURCEGROUP_AKS --name $AKS_NAME --query nodeResourceGroup -o tsv)
az role assignment create --role Reader --assignee $IDENTITY_PRINCIPALID --scope /subscriptions/$SUBSCRIPTIONID/resourcegroups/$NODERESOURCEGROUP

# according to https://github.com/Azure/aad-pod-identity. But not needed at all.
$AKS_SERVICEPRINCIPAL_ID=$(az aks show -g $RESOURCEGROUP_AKS -n $AKS_NAME --query servicePrincipalProfile.clientId -o tsv)
az role assignment create --role "Managed Identity Operator" --assignee $AKS_SERVICEPRINCIPAL_ID --scope $IDENTITY_RESOURCEID

# The Helm charts gives an error. It's missing the CRD's. Install them manually to make it work.
# More info: https://github.com/Azure/aad-pod-identity/issues/454
kubectl apply -f ./aadpodidentity-tmp/crd.yaml

start-sleep -Seconds 120 # sometimes the Service Principal is not available for AKS yet

helm repo add aad-pod-identity https://raw.githubusercontent.com/Azure/aad-pod-identity/master/charts
helm upgrade "aad-pod-identity" "aad-pod-identity/aad-pod-identity" --install --set azureIdentity.enabled=true,azureIdentity.resourceID=$IDENTITY_RESOURCEID,azureIdentity.clientID=$IDENTITY_CLIENTID,azureIdentityBinding.selector=$K8S_KEYVAULTACCESS_LABEL

kubectl apply -f keyvaultreader.yaml