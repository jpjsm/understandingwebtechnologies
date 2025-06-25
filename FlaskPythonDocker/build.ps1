$DebugPreference = "Continue"

Write-Debug "`$status = `$(docker build -t flask-hostname:2.0.1 .)"
$status = $(docker build -t flask-hostname:2.0.1 .)
Write-Debug "Status: ${status}"
if ($status -match "Successfully built") {
    Write-Debug "docker tag flask-hostname:2.0.1 jpjofresm/flask-hostname:2.0.1"
    docker tag flask-hostname:2.0.1 jpjofresm/flask-hostname:2.0.1
    Write-Debug "docker push jpjofresm/flask-hostname:tagname"
    docker push jpjofresm/flask-hostname:2.0.1
}