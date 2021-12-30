Write-Host "Setting build number"


$jan2020 = New-Object DateTime(2020,01,01);
$now = Get-Date
$timeDifference = New-TimeSpan -Start $jan2020 -End $now
$daysSinceThen = [math]::Round($timeDifference.TotalDays);

$majorNumber = 0
$minorNumber = 9
$buildNumber = $daysSinceThen
$revision    = $now.ToString("HHmm")

$version = "$majorNumber.$minorNumber.$buildNumber.$revision"

Write-Host "Using version number: $version"

# Setting env variable will not be seen by bash if this script is executed from bash
$env:VERSION = $version

# Update build props with this new version:
$filePath = "Directory.Build.props"

$xml = New-Object xml
$xml.Load($filePath)
#$versionNode = $xml.Project.PropertyGroup.Version
$xml.Project.PropertyGroup.Version = $version
$xml.Save($filePath)
