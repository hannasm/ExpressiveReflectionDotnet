param ($installPath, $toolsPath, $package, $project);

Write-host 'Install path is ' $installPath;

[System.Reflection.Assembly]::LoadWithPartialName('System.IO.Compression');

$versionString = Split-Path $installPath -Leaf;
$files = (Get-ChildItem  (Join-Path $installPath 'content') -Recurse);

$zip = get-childitem (Join-Path $installPath '*.nupkg') | select -First 1;
$fs = New-Object System.IO.FileStream ($zip.FullName, [System.IO.FileMode]::Open);
$zip = new-object System.IO.Compression.ZipArchive ($fs, [System.IO.Compression.ZipArchiveMode]::Update);

foreach ($file in $files) {
	if (([System.IO.Path]::GetExtension($file.Name)) -eq '.cs') {
		
		# annotate source files with a comment indicating package version, as well as wrapping all code inside the namespace
		$text = [System.IO.File]::ReadAllText($file.FullName);
		$text = '// Nuget source distribution of ' + $versionString + [Environment]::NewLine +
			    'namespace ' + ($project.Properties | where {$_.Name -eq 'RootNamespace'}).Value + '.App_Packages {' + [Environment]::NewLine + '  ' +
				$text.Replace(([Environment]::NewLine), ([Environment]::Newline + '  ')) +
				'}';
		[System.IO.File]::WriteAllText($file.FullName, $text);

		# get path to location of the files in project
		$destPath = Resolve-Path (join-path $installPath '../../');
		$destPath = (Join-Path $destPath ((split-path $file.FullName -parent).Substring((Join-Path $installPath 'content').Length + 1)));
		$destPath = Join-Path $destPath $file.Name;

		# copy modified file to project install location
		Copy-Item $file.FullName $destPath -Force;
		
		#update the file in the zip archive too...
		$entryName = $file.FullName.Substring(($installPath.Length + 1)).Replace('\', '/');
		Write-Host 'searching for entry name ' + $entryName;

		$zip.GetEntry($entryName).Delete();
		$e = $zip.CreateEntry($entryName);
		$writer = New-Object System.IO.StreamWriter ($e.Open());
		$writer.Write($text);
		$writer.Dispose();
	}
}
$zip.Dispose();
$fs.Dispose();