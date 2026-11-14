<#
.SYNOPSIS
  Builds the release zips: the UVT bundle (launcher + tools\<Name>\) and a standalone zip per tool, for x64 and x86.

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

# Bundled tools: folder name under tools\ in the bundle, project file, zip name of the standalone build.
$tools = @(
    @{ Name = 'SourceSDK.ENV.Editor'; Project = 'Tools\SourceSDK.ENV.Editor\src\SourceSDK.ENV.Editor.csproj' },
    @{ Name = 'RetimePhonemeUtil';    Project = 'Tools\RetimePhonemeUtil\src\kru.gui\kru.gui.csproj' }
)
$launcher = @{ Name = 'UniversalValveToolbox'; Project = 'UniversalValveToolbox\UniversalValveToolbox.csproj' }

function Get-Version($project) {
    $xml = [xml](Get-Content (Join-Path $src $project) -Raw)
    $v = ($xml.Project.PropertyGroup | ForEach-Object { $_.Version } | Where-Object { $_ } | Select-Object -First 1)
    if (-not $v) { throw "No <Version> in $project" }
    return $v
}

function Get-Output($project, $platform) {
    return Join-Path (Split-Path (Join-Path $src $project)) "bin\$platform\$Configuration"
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

$uvtVersion = Get-Version $launcher.Project
foreach ($platform in $Platforms) {
    if (-not $NoBuild) {
        Write-Host "Building $Configuration $platform"
        & dotnet build (Join-Path $src 'UniversalValveToolbox.sln') -c $Configuration -p:Platform=$platform -v:minimal -nologo
        if ($LASTEXITCODE -ne 0) { throw "build failed ($platform)" }
    }
    Write-Host "Packaging $platform"

    # bundle: launcher + every tool under tools\<Name>\
    $bundle = Join-Path $stage "$($launcher.Name)-$uvtVersion-$platform"
    Copy-Output (Get-Output $launcher.Project $platform) $bundle
    foreach ($tool in $tools) {
        Copy-Output (Get-Output $tool.Project $platform) (Join-Path $bundle "tools\$($tool.Name)")
    }
    Add-Docs $bundle
    New-Zip $bundle (Join-Path $OutDir "$($launcher.Name)-$uvtVersion-$platform.zip")

    # standalone zip per tool (e.g. for its own Chocolatey package)
    foreach ($tool in $tools) {
        $version = Get-Version $tool.Project
        $dir = Join-Path $stage "$($tool.Name)-$version-$platform"
        Copy-Output (Get-Output $tool.Project $platform) $dir
        Add-Docs $dir
        New-Zip $dir (Join-Path $OutDir "$($tool.Name)-$version-$platform.zip")
    }
}
Remove-Item $stage -Recurse -Force
