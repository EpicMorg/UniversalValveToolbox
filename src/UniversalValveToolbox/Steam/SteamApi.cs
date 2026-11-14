using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace UniversalValveToolbox.Steam {
    /// <summary>
    /// The launcher's Steam client: our own thin wrapper over the native steam_api(64).dll flat API.
    /// Runs as the configured app id (ToolsAppId: 480 by default, or the app id of a modder's own SDK/tool,
    /// so Steam shows that app as running). Never throws to callers: if Steam is not available,
    /// queries return "not installed" / null.
    /// </summary>
    internal static class SteamApi {
        private static IntPtr apps, user, friends, utils;
        private static readonly ConcurrentDictionary<int, string> installDirs = new ConcurrentDictionary<int, string>();

        public static bool IsInitialized { get; private set; }

        /// <summary>App id the API runs as (0 when not initialized).</summary>
        public static uint AppId => IsInitialized ? SteamNative.SteamAPI_ISteamUtils_GetAppID(utils) : 0;

        /// <summary>
        /// Starts the Steam API as <paramref name="appId"/>. The app id is passed through the process environment
        /// (SteamAppId / SteamGameId), not steam_appid.txt and not the user environment.
        /// </summary>
        public static bool Init(uint appId, out string error) {
            error = null;
            if (IsInitialized)
                return true;

            try {
                SteamNative.EnsureResolver();
                Environment.SetEnvironmentVariable("SteamAppId", appId.ToString());
                Environment.SetEnvironmentVariable("SteamGameId", appId.ToString());

                var msg = new byte[SteamNative.ErrMsgSize];
                var result = SteamNative.SteamAPI_InitFlat(msg);
                if (result != SteamNative.InitOk) {
                    var text = ReadUtf8(msg);
                    error = result switch {
                        SteamNative.InitNoSteamClient => "Steam is not running or the user is not logged in.",
                        SteamNative.InitVersionMismatch => "The Steam client is older than the bundled steam_api; update Steam.",
                        _ => "Steam API init failed."
                    } + (string.IsNullOrEmpty(text) ? string.Empty : Environment.NewLine + text);
                    return false;
                }

                apps = SteamNative.SteamAPI_SteamApps_v009();
                user = SteamNative.SteamAPI_SteamUser_v023();
                friends = SteamNative.SteamAPI_SteamFriends_v018();
                utils = SteamNative.SteamAPI_SteamUtils_v011();
                IsInitialized = apps != IntPtr.Zero && user != IntPtr.Zero && friends != IntPtr.Zero && utils != IntPtr.Zero;
                if (!IsInitialized) {
                    SteamNative.SteamAPI_Shutdown();
                    error = "Steam API interfaces are not available (steam_api version mismatch?).";
                }
                return IsInitialized;
            }
            catch (DllNotFoundException ex) {
                error = $"{(Environment.Is64BitProcess ? "steam_api64.dll" : "steam_api.dll")} not found next to the launcher.{Environment.NewLine}{ex.Message}";
                return false;
            }
            catch (EntryPointNotFoundException ex) {
                error = $"steam_api does not have the expected exports (wrong version?).{Environment.NewLine}{ex.Message}";
                return false;
            }
        }

        /// <summary>Retries <see cref="Init"/> until it succeeds or the timeout passes (used right after starting Steam).</summary>
        public static bool WaitAndInit(uint appId, TimeSpan timeout, out string error) {
            var sw = Stopwatch.StartNew();
            while (true) {
                if (Init(appId, out error))
                    return true;
                if (sw.Elapsed > timeout)
                    return false;
                Thread.Sleep(1000);
            }
        }

        public static void Shutdown() {
            if (!IsInitialized)
                return;
            IsInitialized = false;
            apps = user = friends = utils = IntPtr.Zero;
            installDirs.Clear();
            SteamNative.SteamAPI_Shutdown();
        }

        /// <summary>Steam process (if running), independent of the API state.</summary>
        public static Process SteamProcess => Process.GetProcessesByName("steam").FirstOrDefault();

        public static bool IsLoggedOn => IsInitialized && SteamNative.SteamAPI_ISteamUser_BLoggedOn(user);

        public static ulong SteamId => IsInitialized ? SteamNative.SteamAPI_ISteamUser_GetSteamID(user) : 0;

        /// <summary>Persona (display) name of the logged-in user, or null.</summary>
        public static string UserName => IsInitialized ? ReadUtf8(SteamNative.SteamAPI_ISteamFriends_GetPersonaName(friends)) : null;

        public static bool IsAppInstalled(int appId) =>
            IsInitialized && appId > 0 && SteamNative.SteamAPI_ISteamApps_BIsAppInstalled(apps, (uint)appId);

        /// <summary>Install folder of an installed app, or null. Cached until <see cref="ClearCache"/>.</summary>
        public static string AppInstallDir(int appId) {
            if (!IsInitialized || appId <= 0)
                return null;
            if (installDirs.TryGetValue(appId, out var cached))
                return cached;

            string dir = null;
            if (SteamNative.SteamAPI_ISteamApps_BIsAppInstalled(apps, (uint)appId)) {
                var buffer = new byte[1024];
                var len = SteamNative.SteamAPI_ISteamApps_GetAppInstallDir(apps, (uint)appId, buffer, (uint)buffer.Length);
                if (len > 0) {
                    dir = ReadUtf8(buffer);
                    if (!Directory.Exists(dir))
                        dir = null;
                }
            }
            // cache only found folders: a game installed while the launcher is open shows up on the next query
            if (dir != null)
                installDirs[appId] = dir;
            return dir;
        }

        public static string AppInstallDir(uint appId) => appId > int.MaxValue ? null : AppInstallDir((int)appId);

        public static bool IsAppInstalled(uint appId) => appId <= int.MaxValue && IsAppInstalled((int)appId);

        /// <summary>Forget cached install folders (after the user installs/moves games).</summary>
        public static void ClearCache() => installDirs.Clear();

        /// <summary>Version of the loaded steam_api(64).dll, for the About box.</summary>
        public static string NativeLibraryVersion {
            get {
                var file = Environment.Is64BitProcess ? "steam_api64.dll" : "steam_api.dll";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                return File.Exists(path) ? $"{file} {FileVersionInfo.GetVersionInfo(path).FileVersion}" : $"{file} (missing)";
            }
        }

        private static string ReadUtf8(byte[] buffer) {
            var len = Array.IndexOf(buffer, (byte)0);
            return Encoding.UTF8.GetString(buffer, 0, len < 0 ? buffer.Length : len);
        }

        private static string ReadUtf8(IntPtr p) => p == IntPtr.Zero ? null : System.Runtime.InteropServices.Marshal.PtrToStringUTF8(p);
    }
}
