# Universal Valve Toolbox  [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

# [![GitHub Build Status](https://img.shields.io/github/workflow/status/EpicMorg/UniversalValveToolbox/Universal%20Valve%20Toolbox%20-%20master?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/actions) [![Translation status](https://translate.epicm.org/widgets/universalvalvetoolbox/-/svg-badge.svg)](https://translate.epicm.org/engage/universalvalvetoolbox/?utm_source=widget) [![Size](https://img.shields.io/github/repo-size/EpicMorg/UniversalValveToolbox?label=size&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md) [![Activity](https://img.shields.io/github/commit-activity/w/EpicMorg/UniversalValveToolbox?&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers) [![GitHub uses](https://img.shields.io/sourcegraph/rrc/github.com/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/pulse) [![CodeScene Code Health](https://codescene.io/projects/6852/status-badges/code-health)](https://codescene.io/projects/6852) [![CodeScene System Mastery](https://codescene.io/projects/6852/status-badges/system-mastery)](https://codescene.io/projects/6852)

---------------------------

`Universal Valve Toolbox is not affiliated with Valve Corporation.`

---------------------------

# Settings
Mod could be changed at `settings.json` with `ToolboxMod` section. Supported mods: `retail`, `bundle`, `dev`.
## Retail
This is default mode. Shows all avalible users games at account. Tool launched with default steam app id `480` for requesting steam api and getting info.

## Bundle mode
If you want to distribute this tollbox with your game - edit  `settings.json` and switch `ToolboxMod` to `bundle` value. And change `ToolsAppId` and `BundleAppID` for your's `SteamApp` of you game and sdk tools.
## Dev mode
Dev mode is the same mode as `bundle` but local path of root of your game folder and angine name will be get from  `DevEnginePath` and `DevEngineName` values. Also You should set `ToolsAppId` and `BundleAppID` for your's `SteamApp` of you game and sdk tools.
 