$data = [xml](Get-Content Package.nuspec);
$version = $data.package.metadata.version;

$assemblyPackage = 'ExpressiveReflection.' + $version + '.nupkg';
$sourcePackage = 'ExpressiveReflection.Sources.' + $version + '.nupkg';

if (!(test-path $assemblyPackage) -or !(Test-Path $sourcePackage)) {
	if (!(test-path $assemblyPackage)) {
		Write-Error 'Unable teo find assembly package ' + $assemblyPackage;
	}
	if (!(Test-Path $sourcePackage)) {
		Write-Error 'Unable to find source package ' + $sourcePackage;
	}
	return;
} else {
	nuget push $assemblyPackage;
	nuget push $sourcePackage;
}