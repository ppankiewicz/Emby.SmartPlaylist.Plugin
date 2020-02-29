
$buildConfig = $args[0]

if(!$buildConfig){
	$buildConfig = "Debug"
}


$EmbyDir = "$env:APPDATA\Emby-Server"
$EmbyExe = "$EmbyDir\system\EmbyServer.exe"
$EmbyPluginDir = "$EmbyDir\programdata\plugins"

taskkill /IM "EmbyServer.exe" /F
taskkill /IM "embytray.exe" /F

gci -Recurse -Filter "SmartPlaylist.dll" -File -ErrorAction SilentlyContinue -Path "backend" `
    | Where-Object  {$_.Directory -match "bin\\$buildConfig"} `
    | Copy-Item -Destination "$EmbyPluginDir" -Force


iex "$EmbyExe"