<#
.SYNOPSIS
  Builds the release zips for x64 and x86:
    UniversalValveToolbox-<version>-<platform>.zip  - the bundle: launcher + tools\<Name>\ (the launcher's build output)
    <Tool>-<version>-<platform>.zip                  - every bundled tool on its own (e.g. for its Chocolatey package)
  The tool list comes from the <BundledTool> items of UniversalValveToolbox.csproj (one place for build, bundle and zips).

.EXAMPLE
  pwsh build/package.ps1                  # Release, x64 + x86, into dist\
  pwsh build/package.ps1 -Platforms x64 -NoBuild
#>
param(
    [string[]]$Platforms = @('x64', 'x86'),
    [string]$Configuration = 'Release',
    [string]$OutDir = (Join-Path $PSScriptRoot '..\dist'),
    [switch]$NoBuild
)
$ErrorActionPreference = 'Stop'
$root = Resolve-Path (Join-Path $PSScriptRoot '..')
$src = Join-Path $root 'src'
$launcherProject = Join-Path $src 'UniversalValveToolbox\UniversalValveToolbox.csproj'

function Get-Version($project) {
    $xml = [xml](Get-Content $project -Raw)
    $v = ($xml.Project.PropertyGroup | ForEach-Object { $_.Version } | Where-Object { $_ } | Select-Object -First 1)
    if (-not $v) { throw "No <Version> in $project" }
    return $v
}

# <BundledTool Include="Name" ProjectPath="..\Tools\...\x.csproj" /> in the launcher project
$tools = ([xml](Get-Content $launcherProject -Raw)).Project.ItemGroup.BundledTool | Where-Object { $_ } | ForEach-Object {
    $project = [IO.Path]::GetFullPath((Join-Path (Split-Path $launcherProject) $_.ProjectPath))
    @{ Name = $_.Include; Project = $project; Version = (Get-Version $project) }
}
if (-not $tools) { throw "No <BundledTool> items in $launcherProject" }

function Get-Output($project, $platform) {
    return Join-Path (Split-Path $project) "bin\$platform\$Configuration"
}

# Copies a build output without debug files and without user data created by test runs.
function Copy-Output($from, $to) {
    New-Item -ItemType Directory -Force $to | Out-Null
    Copy-Item (Join-Path $from '*') $to -Recurse -Force
    Get-ChildItem $to -Recurse -Include '*.pdb', '*.runtimeconfig.dev.json' -File | Remove-Item -Force
    foreach ($userData in 'json\settings.json', 'json\projects') {
        $p = Join-Path $to $userData
        if (Test-Path $p) { Remove-Item $p -Recurse -Force }
    }
}

function Add-Docs($to) {
    foreach ($doc in 'LICENSE.md', 'THIRD-PARTY-NOTICES.md') {
        $p = Join-Path $root $doc
        if (Test-Path $p) { Copy-Item $p $to -Force }
    }
}

function New-Zip($folder, $zip) {
    if (Test-Path $zip) { Remove-Item $zip -Force }
    Compress-Archive -Path (Join-Path $folder '*') -DestinationPath $zip -CompressionLevel Optimal
    Write-Host "  $([IO.Path]::GetFileName($zip))  $([math]::Round((Get-Item $zip).Length / 1MB, 1)) MB"
}

New-Item -ItemType Directory -Force $OutDir | Out-Null
$OutDir = Resolve-Path $OutDir
$stage = Join-Path $OutDir 'stage'
if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }

$uvtVersion = Get-Version $launcherProject
Write-Host "UVT $uvtVersion; bundled: $(($tools | ForEach-Object { "$($_.Name) $($_.Version)" }) -join ', ')"
foreach ($platform in $Platforms) {
    if (-not $NoBuild) {
        Write-Host "Building $Configuration $platform"
        & dotnet build (Join-Path $src 'UniversalValveToolbox.sln') -c $Configuration -p:Platform=$platform -v:minimal -nologo
        if ($LASTEXITCODE -ne 0) { throw "build failed ($platform)" }
    }
    Write-Host "Packaging $platform"

    # bundle = the launcher's output, which already has tools\<Name>\ (CopyBundledTools target)
    $bundle = Join-Path $stage "UniversalValveToolbox-$uvtVersion-$platform"
    Copy-Output (Get-Output $launcherProject $platform) $bundle
    foreach ($tool in $tools) {
        if (-not (Test-Path (Join-Path $bundle "tools\$($tool.Name)"))) { throw "bundle has no tools\$($tool.Name)" }
    }
    Add-Docs $bundle
    New-Zip $bundle (Join-Path $OutDir "UniversalValveToolbox-$uvtVersion-$platform.zip")

    # standalone zip per tool
    foreach ($tool in $tools) {
        $dir = Join-Path $stage "$($tool.Name)-$($tool.Version)-$platform"
        Copy-Output (Get-Output $tool.Project $platform) $dir
        Add-Docs $dir
        New-Zip $dir (Join-Path $OutDir "$($tool.Name)-$($tool.Version)-$platform.zip")
    }
}
Remove-Item $stage -Recurse -Force
