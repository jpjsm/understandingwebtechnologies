$DebugPreference = "Continue"

Write-Debug "`$status = `$(docker build -t donothingubuntu:0.1 .)"
$status = $(docker build -t donothingubuntu:0.1 .)
Write-Debug "Status: ${status}"
if ($status -match "Successfully built") {
    Write-Debug "docker tag donothingubuntu:0.1 jpjofresm/donothingubuntu:0.1"
    docker tag donothingubuntu:0.1 jpjofresm/donothingubuntu:0.1
    Write-Debug "docker push jpjofresm/donothingubuntu:tagname"
    docker push jpjofresm/donothingubuntu:0.1
}