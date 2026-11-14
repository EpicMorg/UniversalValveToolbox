# Universal Valve Toolbox  [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

# [![GitHub Build Status](https://img.shields.io/github/actions/workflow/status/EpicMorg/UniversalValveToolbox/master.yml?branch=master&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/actions) [![Translation status](https://translate.epicm.org/widgets/universalvalvetoolbox/-/svg-badge.svg)](https://translate.epicm.org/engage/universalvalvetoolbox/?utm_source=widget) [![Size](https://img.shields.io/github/repo-size/EpicMorg/UniversalValveToolbox?label=size&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md) [![Activity](https://img.shields.io/github/commit-activity/w/EpicMorg/UniversalValveToolbox?&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers) [![GitHub uses](https://img.shields.io/sourcegraph/rrc/github.com/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/pulse) [![CodeScene Code Health](https://codescene.io/projects/6852/status-badges/code-health)](https://codescene.io/projects/6852) [![CodeScene System Mastery](https://codescene.io/projects/6852/status-badges/system-mastery)](https://codescene.io/projects/6852)

---------------------------

`Universal Valve Toolbox is not affiliated with Valve Corporation.`

---------------------------

# Settings
All data lives in the `json` folder next to `UniversalValveToolbox.exe`:

| Path | What |
| --- | --- |
| `json\settings.json` | language, mode, Steam app ids (created on first start) |
| `json\engines\<appid>.json` | supported games and their tools (shipped with UVT) |
| `json\projects\*.json` | your projects (edited in *Edit Projects*) |
| `json\addons\*.json` | addons and bundled tools (edited in *Edit Addons*) |

If `settings.json` cannot be read (e.g. a typo), UVT says so, runs with defaults and does not overwrite the file.

## settings.json
```json
{
  "Language": "en-US",
  "ToolboxMod": "retail",
  "ToolsAppId": 480,
  "BundleAppID": 480,
  "DevEnginePath": "C:\\MyGame",
  "DevEngineName": "My Future Game (engine)",
  "LastSelectedProject": null
}
```

- `ToolsAppId` — the Steam app id UVT runs as. Steam shows this app as running while UVT is open. `480` (Spacewar) is
  Valve's test app; if you ship UVT with your game or SDK, put the app id of your SDK/tools app here.
- `ToolboxMod` — one of the modes below.

## Modes
### retail (default)
Every game from `json\engines` that is installed in the user's Steam library, with the tools that exist in that install.

### bundle
For shipping UVT with one game: only the engine `BundleAppID` is shown, its tools are taken from that game's install folder.
Set `ToolsAppId` to your SDK/tools app and `BundleAppID` to your game, and make sure `json\engines\<BundleAppID>.json` exists.

### dev
Like `bundle`, but the game is a local folder (`DevEnginePath`, shown as `DevEngineName`), e.g. a build that is not on Steam.
The list of tools comes from `json\engines\<BundleAppID>.json`; without that file the list is empty.

## Projects
```json
{ "engine": 243730, "path": "C:\\MyMod", "name": "My Mod", "args": "-dev" }
```
`Run Project` starts the game with `-steam -game "<path>" <args>`. Tools get `-game "<path>"` unless their own arguments
already have `-game`; the launched tool also gets `VProject=<path>` (and `VMod`, `VContent`, `VGame`, `VTools` for
Source Filmmaker). The user environment is updated only when the value changes.

## Addons
```json
{ "Engines": [], "Name": "Notepad++", "Category": "", "Bin": "%ProgramFiles%\\Notepad++\\notepad++.exe", "Args": "" }
```
- `Engines` — app ids the addon is shown for; empty = every engine.
- `Bin` — full path, environment variables (`%ProgramFiles%`) or a path relative to the UVT folder (`tools\\...`).
  Addons whose exe is missing are hidden.
- `Category` — `catBundled` puts the addon into the *Bundled tools* group (used by the tools shipped in `tools\`);
  other categories are listed in [docs/categories.md](docs/categories.md).
 