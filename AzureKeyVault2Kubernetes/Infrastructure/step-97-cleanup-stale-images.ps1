# get definitions
. ".\step-00-global.ps1"

$ACR_Repositories = @("webapi","ingress")

$TIMESTAMP = (Get-Date).AddDays(-1000).ToString('yyyy-MM-dd')
$i = 0.0
[int] $step = (1.0/$ACR_Repositories.Length) * 100
foreach($repo in $ACR_Repositories){
    $pct = ($i * 100.0) / $ACR_Repositories.Length
    Write-Progress -Activity 'Clean-Up stale images' -Status "Cleaning repo: ${repo}" -PercentComplete $pct
    $digests = az acr repository show-manifests --name $ACR_Name --repository $repo --orderby time_asc --query "[?timestamp < '$TIMESTAMP'].digest" -o tsv

    $j = 0.0

    foreach($digest in $digests){
        [int]$progress = $pct + ($j/$digests.Length)*$step
        Write-Progress -Activity 'Clean-Up stale images' -Status "Cleaning repo: ${repo}" -PercentComplete $progress
        az acr repository delete --name $ACR_Name --image ${repo}@${digest} --yes
        $j++
    }

    $i++
}
