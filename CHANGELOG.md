# Universal Valve Toolbox  [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

# [![GitHub Build Status](https://img.shields.io/github/workflow/status/EpicMorg/UniversalValveToolbox/Universal%20Valve%20Toolbox%20-%20master?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/actions) [![Translation status](https://translate.epicm.org/widgets/universalvalvetoolbox/-/svg-badge.svg)](https://translate.epicm.org/engage/universalvalvetoolbox/?utm_source=widget) [![Size](https://img.shields.io/github/repo-size/EpicMorg/UniversalValveToolbox?label=size&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md) [![Activity](https://img.shields.io/github/commit-activity/w/EpicMorg/UniversalValveToolbox?&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers) [![GitHub uses](https://img.shields.io/sourcegraph/rrc/github.com/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/pulse) [![CodeScene Code Health](https://codescene.io/projects/6852/status-badges/code-health)](https://codescene.io/projects/6852) [![CodeScene System Mastery](https://codescene.io/projects/6852/status-badges/system-mastery)](https://codescene.io/projects/6852)

---------------------------

`Universal Valve Toolbox is not affiliated with Valve Corporation.`

---------------------------

# Changelog

## 2.0.0.0
#### Whats new
* `Steam` ready release!1!11! 
* Fixed naming of [supported engines](SUPPORTED.md), also all filesn in `engine` folder were renamed! Please empty this folder before manual update to avoid errors.
* Replaced `Counter-Strike: Global Offensive` to `Counter-Strike 2` engine, due of deprecation of `csgo` by `Valve`.
* Updated @Facepunch [Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) to latest stable version.
* Updated `steam_api` to `1.57`.
* Added new translations:
* Added `url` support of links in launcher.
* Added `plugins` support of future bundled `dlc` from `steam`-version.
* `Team Fortress 2`, `Counter-Strike: Source`, `Day of Defeat: Source`, `Half-Life 2: Deathmatch`, `Half-Life Deathmatch: Source`, `Garry's Mod`: games now start from their own executables (`tf_win64.exe`, `cstrike_win64.exe`, `dod_win64.exe`, `hl2mp_win64.exe`, `hl1mp_win64.exe`, `gmod_win64.exe`) instead of `hl2.exe`; 32-bit `Run Game` kept
* Old builds keep working: entries for games that moved to their own executables keep the old `hl2.exe` ones next to the new (`Engine Tools (hl2.exe)`, `Run Game (hl2.exe)`, Half-Life 2 `(hl2 only)`), only the ones that exist in the install are shown; engines got an optional `altBins` list, and `Run Project` uses the first existing of `bin` + `altBins`
* Added in-engine Hammer (`<game>.exe -edit`, loads `bin\hammer_dll.dll` into the game process) for the engines that support it: Left 4 Dead 1/2, Portal 2, Alien Swarm, Reactive Drop, Aperture Tag, Thinking with Time Machine, CS:GO (Legacy), Insurgency, Portal Stories: Mel — useful where only `hammer_dll.dll` is shipped
* Added `Portal Stories: Mel`, `Insurgency`, `Nuclear Dawn`, `Zombie Panic! Source`, `Portal: Prelude RTX`, `Portal with RTX`, `Half-Life 2 RTX`, `Half-Life 2: DownFall`, `Half-Life 2: Year Long Alarm`, `Half-Life 2: Genry's Great Escape From City 13` (launch data from Steam)
* Fixed app ids of `No More Room in Hell` (224260) and `Thinking with Time Machine` (286080) — they carried the ids of Day of Infamy and Reactive Drop; `Reactive Drop` runs `-game reactivedrop` (its mod folder) instead of `swarm`
* Added 64-bit tools (`bin\x64`, Garry's Mod `bin\win64`): Hammer, Hammer++, HLMV, FacePoser, QC Eyes — shown only when installed; also for `Source SDK 2013 MultiPlayer`
* Added `Counter-Strike: Global Offensive (Legacy)` — the old CS:GO is a separate app `4465480` now (`730` is Counter-Strike 2)
* Added `Portal: Revolution` (Strata Source, `bin\win64`) and `Team Fortress 2 Classified` (`tf2classified_win64.exe`, tools launcher, Hammer++ / HLMV++ x64)
* Fixed `Counter-Strike` typos (incl. Cyrillic `С`) in the supported list
* Added `Entropy : Zero`, `Entropy : Zero 2` (+ Wilson : Zero, Progenitors, Axon Pariah) and `Pirates, Vikings and Knights II` (x64) — launch configs as published by Steam
* `Half-Life 2`: runs `-game hl2_complete` (20th anniversary update), `Run Game (hl2 only)` kept

## 1.0.0.4
#### Whats new
* Fixed `SteamInit` on program initialization and added tries to launch `Steam`
* Fixed `Run project` option
* Added [support for new engines](SUPPORTED.md)
* Added some default addons, but not linked with any engines

## 1.0.0.3
#### Whats new
* Migrated to @Facepunch [Steamworks](https://github.com/Facepunch/Facepunch.Steamworks)
* Added support for new engines
* Code cleanup
* Added new modes - `retail`, `bundeled`, `dev`

## 1.0.0.2
#### Whats new
* SteamPathLib version bump
* Fixed UI
* Fixed logic

## 1.0.0.1
#### Whats new
* lang hotfix

## 1.0.0.0
#### Whats new
* Release Candidate