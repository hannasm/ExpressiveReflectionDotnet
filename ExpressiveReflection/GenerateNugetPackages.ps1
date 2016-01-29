remove-item ExpressiveReflection*.nupkg

msbuild ./ExpressiveReflection.csproj /p:Configuration=Release

nuget pack 'Package.nuspec' -Symbol -Prop Configuration=Release

$data = [xml](Get-Content Package.nuspec);
$version = $data.package.metadata.version;

$data.package.metadata.description = 'This is a source only distribution of the ' + $data.package.metadata.id + ' library that can be embedded inside of other projects / libraries.';
$data.package.metadata.id = $data.package.metadata.id + '.Sources';

$project = [xml](Get-Content ExpressiveReflection.csproj)
$rootFilePath = (Join-Path (join-path 'content' 'App_Packages') ($data.package.metadata.id + '.' + $version))
$fileRef = $data.package.files.file | select -first 1;

foreach ($grp in $project.Project.ItemGroup) {
	foreach ($compile in $grp.Compile) {
		if ( (Split-Path $compile.Include -Leaf) -eq 'AssemblyInfo.cs') { continue; }

		$file = $fileRef.Clone();
		$file.src = $compile.Include;
		$file.target = [string](Join-Path $rootFilePath $compile.Include);

		$data.package.files.AppendChild($file);
	}
}

$sourcesNuspec = 'ExpressiveReflection.Sources.nuspec';

$data.OuterXml | Out-File -FilePath $sourcesNuspec -Force;

nuget pack $sourcesNuspec;


# vim: set expandtab ts=2 sw=2: 
