# Universal Valve Toolbox  [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

# [![GitHub Build Status](https://img.shields.io/github/actions/workflow/status/EpicMorg/UniversalValveToolbox/master.yml?branch=master&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/actions) [![Translation status](https://translate.epicm.org/widgets/universalvalvetoolbox/-/svg-badge.svg)](https://translate.epicm.org/engage/universalvalvetoolbox/?utm_source=widget) [![Size](https://img.shields.io/github/repo-size/EpicMorg/UniversalValveToolbox?label=size&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md) [![Activity](https://img.shields.io/github/commit-activity/w/EpicMorg/UniversalValveToolbox?&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers) [![GitHub uses](https://img.shields.io/sourcegraph/rrc/github.com/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/pulse) [![CodeScene Code Health](https://codescene.io/projects/6852/status-badges/code-health)](https://codescene.io/projects/6852) [![CodeScene System Mastery](https://codescene.io/projects/6852/status-badges/system-mastery)](https://codescene.io/projects/6852)

---------------------------

`Universal Valve Toolbox is not affiliated with Valve Corporation.`

---------------------------

# Description
`Universal Valve Toolbox` (UVT) is a launcher for the modding tools of Source engine games: Hammer, Hammer++, Model Viewer,
FacePoser, Engine Tools, in-engine Hammer (`-edit`), your mod projects and your own helper programs — for every
supported game you have installed in Steam. Full list of supported games: [SUPPORTED.md](SUPPORTED.md).

- Shows only the games you have installed and only the tools that exist in each install (old and new builds of a game).
- Projects: your mod folders with their engine and launch arguments; `VProject` is set for the tools you start.
- Addons: any program (7-Zip, Notepad++, Git, …) in the same list, per engine or for all.
- Bundled tools: [SourceSDK.ENV.Editor](src/Tools/SourceSDK.ENV.Editor) and
  [Retime Phoneme Util GUI](src/Tools/RetimePhonemeUtil) come with UVT and are also released on their own.
- Modders can ship UVT with their game (`bundle` mode, own Steam app id) — see [SETTINGS.md](SETTINGS.md).

# Download
| Where | What |
| --- | --- |
| [GitHub Releases](https://github.com/EpicMorg/UniversalValveToolbox/releases) | `UniversalValveToolbox-<version>-x64.zip` / `-x86.zip` (UVT with the bundled tools), standalone zips of every bundled tool |
| [Gamer-Lab](http://gamer-lab.com/rus/files_source/Universal_Valve_Toolbox) | mirror |

Requirements: Windows 10/11, [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0) of the same
bitness as the zip (x64 or x86), Steam installed and logged in.

Unpack anywhere and run `UniversalValveToolbox.exe`. If Steam is not running, UVT offers to start it and waits for it.

# Documentation
- [Settings and modes](SETTINGS.md): `retail` / `bundle` / `dev`, Steam app id, projects, addons
- [FAQ](docs/FAQ.md)
- [Supported games](SUPPORTED.md)
- [Changelog](CHANGELOG.md)
- [Third-party notices](THIRD-PARTY-NOTICES.md)

# Building
Visual Studio 2026 or the .NET 10 SDK:

```
dotnet build src/UniversalValveToolbox.sln -c Release -p:Platform=x64     # or x86
pwsh build/package.ps1                                                      # release zips into dist\
```

Output: `src/UniversalValveToolbox/bin/<x64|x86>/<Debug|Release>/`, bundled tools in its `tools\` subfolder.
Pushing a `v*` tag builds a draft GitHub release with all zips.

---------------------------

# Stargazers
[![Stargazers repo roster for @EpicMorg/UniversalValveToolbox](https://reporoster.com/stars/dark/EpicMorg/UniversalValveToolbox)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers)

# Forkers
[![Forkers repo roster for @EpicMorg/UniversalValveToolboxr](https://reporoster.com/forks/dark/EpicMorg/UniversalValveToolbox)](https://github.com/EpicMorg/UniversalValveToolbox/network/members)

---------------------------

[![](https://codescene.io/projects/6852/status.svg)](https://codescene.io/projects/6852/jobs/latest-successful/results)
