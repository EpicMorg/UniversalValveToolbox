# Third-party notices

Universal Valve Toolbox and its bundled tools are MIT licensed (see [LICENSE.md](LICENSE.md),
`src/Tools/*/LICENSE.md`). They ship or use the following third-party components.

## Steamworks API (`steam_api.dll`, `steam_api64.dll`)
Valve Corporation, Steamworks SDK 1.65 redistributable binaries. Used by the launcher through its own
wrapper (`src/UniversalValveToolbox/Steam`). Redistributed as allowed by the Steamworks SDK terms;
no SDK headers or sources are included in this repository.

## Retime Phoneme Util (`retime_phoneme.exe`)
Author: **wowks**. Console tool that rescales the phoneme (lip-sync) data of a WAV file; announced and its
source published by the author in the topic «Исправление русской озвучки Half-Life 2 и Эпизодов»
(21–24.07.2015), https://forum.csmania.ru/viewtopic.php?t=43274 (the forum is closed; archived copy:
https://web.archive.org/web/20251104172143/https://forum.csmania.ru/viewtopic.php?t=43274).
Bundled with the author's permission given on that forum; authorship retained. Retime Phoneme Util GUI
(`src/Tools/RetimePhonemeUtil`) is a front end over it.

The tool ships MinGW-w64 runtime libraries next to it: `libgcc_s_seh-1.dll` / `libgcc_s_sjlj-1.dll`,
`libstdc++-6.dll` (GCC runtime, GPL v3 with the GCC Runtime Library Exception) and `libwinpthread-1.dll`
(MinGW-w64 winpthreads, MIT/BSD-style licence).

## NuGet packages
- [Newtonsoft.Json](https://www.newtonsoft.com/json) — MIT (launcher)
- [DarkUI.CE](https://github.com/MuzzleInteractive/DarkUI.CE) — MIT, fork of DarkUI by Robin Perris (SourceSDK.ENV.Editor)
- [Octokit](https://github.com/octokit/octokit.net) — MIT (SourceSDK.ENV.Editor, update check)
