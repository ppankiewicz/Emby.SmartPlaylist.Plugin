
$PackPath = ".\pack\"

New-Item -ItemType Directory -Force -Path $PackPath

$version = .\update_assembly_ver
.\build.ps1 "Release"

gci -Recurse -Filter "SmartPlaylist.dll" -File -ErrorAction SilentlyContinue -Path "backend" `
    | Where-Object  {$_.Directory -match "bin\\Release"} `
    | compress-archive -destinationpath "$PackPath\SmartPlaylist v$version.zip"
