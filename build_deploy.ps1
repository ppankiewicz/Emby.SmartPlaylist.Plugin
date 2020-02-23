
$buildConfig = $args[0]

if(!$buildConfig){
	$buildConfig = "Debug"
}

.\build.ps1 $buildConfig
.\deploy.ps1 $buildConfig