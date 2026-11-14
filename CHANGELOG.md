# Universal Valve Toolbox  [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

# [![GitHub Build Status](https://img.shields.io/github/actions/workflow/status/EpicMorg/UniversalValveToolbox/master.yml?branch=master&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/actions) [![Translation status](https://translate.epicm.org/widgets/universalvalvetoolbox/-/svg-badge.svg)](https://translate.epicm.org/engage/universalvalvetoolbox/?utm_source=widget) [![Size](https://img.shields.io/github/repo-size/EpicMorg/UniversalValveToolbox?label=size&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md) [![Activity](https://img.shields.io/github/commit-activity/w/EpicMorg/UniversalValveToolbox?&style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/UniversalValveToolbox.svg?style=popout-square)](https://github.com/EpicMorg/UniversalValveToolbox/stargazers) [![GitHub uses](https://img.shields.io/sourcegraph/rrc/github.com/EpicMorg/UniversalValveToolbox?style=flat-square)](https://github.com/EpicMorg/UniversalValveToolbox/pulse) [![CodeScene Code Health](https://codescene.io/projects/6852/status-badges/code-health)](https://codescene.io/projects/6852) [![CodeScene System Mastery](https://codescene.io/projects/6852/status-badges/system-mastery)](https://codescene.io/projects/6852)

---------------------------

`Universal Valve Toolbox is not affiliated with Valve Corporation.`

---------------------------

# Changelog

## 2.0.0.0
#### Whats new
* `Steam` ready release!1!11! 
* Moved to `.NET 10` (`net10.0-windows`, SDK-style project), two builds: `x64` and `x86`; needs the [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0) of the same bitness; builds with `dotnet build` or Visual Studio 2026
* Links, `steam://` and tools are opened through the shell as before (`.NET` no longer does it by default); the app no longer opens the main window after the user declined to start Steam
* CI: `actions/checkout@v4` + `actions/setup-dotnet@v4`, no `ACTIONS_ALLOW_UNSECURE_COMMANDS`; fixed the build status badge
* Fixed naming of [supported engines](SUPPORTED.md), also all filesn in `engine` folder were renamed! Please empty this folder before manual update to avoid errors.
* Replaced `Counter-Strike: Global Offensive` to `Counter-Strike 2` engine, due of deprecation of `csgo` by `Valve`.
* Own thin wrapper over the native Steam API (flat API of `steam_api.dll` / `steam_api64.dll`) instead of @Facepunch [Steamworks](https://github.com/Facepunch/Facepunch.Steamworks); `steam_api` updated to Steamworks SDK `1.65`
* The launcher runs as `ToolsAppId` (default `480`): put your own app id there when you bundle it with your game/SDK, and Steam shows it as running
* If Steam is not running, the launcher starts it and waits until it is logged in; no crash when the `steam` process is missing; install folders are cached
* Own two-way WinForms binding helper instead of the `kasthack.binding.wf` package (it only targets .NET Framework); About shows the .NET runtime and bitness
* `VProject` (and `VMod`/`VContent`/`VGame`/`VTools` for Source Filmmaker only) are passed to the launched tool; the user environment is updated only when a value changes (it was rewritten on start and on every click, which was slow and broadcast to all windows); tools start in the game folder
* Tools no longer get `-game ""` when no project is selected
* Saving addons/projects keeps other files and subfolders (`json\addons\template` was deleted) and no longer merges items with the same name into one file
* A broken `settings.json` is reported and left as is (it was silently replaced by defaults)
* Faster main window: one refresh pass instead of two or three, settings read once, tool icons extracted once; projects with the same name in different engines no longer get mixed up
* Bundled tools: [SourceSDK.ENV.Editor](src/Tools/SourceSDK.ENV.Editor) and [Retime Phoneme Util GUI](src/Tools/RetimePhonemeUtil) moved into this repository (history kept), built on `.NET 10` x64/x86 with the launcher; the release has the UVT bundle (`tools\<Name>\`) and a standalone zip of every tool
* Addons: a path relative to the launcher folder (`tools\...`) and environment variables (`%ProgramFiles%`) work; addons without engines are shown for every engine; addons whose exe is missing are hidden (like tools)
* `build/package.ps1` builds the release zips; pushing a `v*` tag creates a draft GitHub release with them; [third-party notices](THIRD-PARTY-NOTICES.md)
* Bundled tools have their own group in the launcher (`Bundled tools`, addon category `catBundled`) and are built together with it into `tools\<Name>\` of its output folder (also when started from Visual Studio); the list lives in `<BundledTool>` of the launcher project and drives the bundle and the standalone zips
* The category chosen in the addon editor is saved now (it was never written to the addon)
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
* Added `Dystopia`, `Portal 2 Sixense Perceptual Pack`, `Portal Reloaded`, `Portal: Forbidden Testing Tracks`, `Half-Life 2: VR Mod`, `Portal: Lost in Aperture` (mod folders from installed `gameinfo.txt`); Portal 2-based ones get the in-engine Hammer (`-edit`)
* Added `Eternal Silence` (17550, `hl2.exe -game esmod`, installs into the shared `Source SDK Base` folder)
* `KAYF-LIFE`: the game exe is `KAYF-LIFE.exe` now — added next to the old `kl.exe` entries; `Insurgency`: `Run Game (x64)` (`insurgency_x64.exe`); `Portal: Forbidden Testing Tracks`: HLMV++
* Fixed names with `:` in the supported list (`Portal Stories: Mel`, `Portal: Prelude RTX`, `Half-Life 2: DownFall` and others)

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