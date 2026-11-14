# FAQ

### UVT says it cannot init the Steam API
Steam must be running and logged in. UVT offers to start Steam and waits up to 90 seconds. If it still fails, the
message tells why: Steam not running, Steam client older than the bundled `steam_api`, or a missing
`steam_api64.dll` / `steam_api.dll` next to the exe (x64 / x86 builds).

### Steam shows me as playing "Spacewar"
That is app `480`, the default `ToolsAppId`. Put another app id into `json\settings.json` (see [SETTINGS.md](../SETTINGS.md)).

### A game is installed but not in the list
Only games from [SUPPORTED.md](../SUPPORTED.md) are shown, and only in `retail` mode. Press the refresh button after
installing a game while UVT is open. To add a game, add `json\engines\<appid>.json` (copy a similar one) or open an issue.

### A tool is missing for a game
UVT shows a tool only if its exe exists in the game folder (e.g. `bin\hammer.exe`, `bin\x64\hammerplusplus.exe`).
Many newer games ship without `hammer.exe`; for the Portal 2 / Left 4 Dead / CS:GO family use
*Valve Hammer Editor (in-engine, -edit)*, which runs Hammer inside the game.

### x64 or x86?
x64 on 64-bit Windows. The x86 build is for 32-bit systems; it needs the x86 .NET 10 Desktop Runtime. Both start the same
tools (the tools' own bitness does not depend on UVT).

### Where are the bundled tools?
In `tools\<Name>\` next to `UniversalValveToolbox.exe`, listed under *Bundled tools*. Each one is also released as its own
zip on the [releases page](https://github.com/EpicMorg/UniversalValveToolbox/releases).

### My `settings.json` was ignored
It could not be parsed (UVT shows a warning with the reason). Fix the file; UVT never overwrites a broken one.

### Is UVT made by Valve?
No. `Universal Valve Toolbox is not affiliated with Valve Corporation.`
