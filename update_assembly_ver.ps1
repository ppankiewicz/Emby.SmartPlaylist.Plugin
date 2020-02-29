function getVersion()
{
    $tag = iex "git describe --tags --always"
    $a = [regex]"v\d+\.\d+\.\d+\.\d+"
    $b = $a.Match($tag)
    $b = $b.Captures[0].value
    $b = $b -replace '-', '.'
    $b = $b -replace 'v', ''
    Write-Host "Version found: $b"
    return $b
}


function SetVersion ($file, $version)
{
	$splitNumber = $version.Split(".")
    $majorNumber = $splitNumber[0]
	$minorNumber = $splitNumber[1]
	$revisionNumber = $splitNumber[3]
	
	# I need to keep my build number under the 65K int limit, hence this hack of a method
	$myBuildNumber = (Get-Date).Year + ((Get-Date).Month * 31) + (Get-Date).Day
	$myBuildNumber = $majorNumber + "." + $minorNumber + "." + $myBuildNumber + "." + $revisionNumber

	$xml=New-Object XML
	$xml.PreserveWhitespace = $true
	$xml.Load($file)
	$xml.Project.PropertyGroup.AssemblyVersion = $version
	$xml.Project.PropertyGroup.FileVersion = $version
	$xml.Save($file)

	Write-Host "Updated csproj "$file" and set to version "$version
}

function setVersionInDir($dir, $version) {

    if ($version -eq "") {
        Write-Host "version not found"
        exit 1
    }

    # Set the Assembly version
    $info_files = Get-ChildItem $dir -Recurse -Include "SmartPlaylist.csproj"
    foreach($file in $info_files)
    {
        Setversion $file $version
    }
}

# First get tag from Git
$version = getVersion
$dir = "./"
setVersionInDir $dir $version

return $version